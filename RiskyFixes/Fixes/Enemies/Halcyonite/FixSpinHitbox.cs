using RoR2;
using RoR2.Projectile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Networking;

namespace RiskyFixes.Fixes.Enemies.Halcyonite
{
    public class FixSpinHitbox : FixBase<FixSpinHitbox>
    {
        public override string ConfigCategoryString => "Enemies - Halcyonite";

        public override string ConfigOptionName => "(Server-Side) Fix Whirlwind";

        public override string ConfigDescriptionString => "Fixes Whirlwind Hitbox persisting when it shouldn't, along with the skill ignoring Stuns.";

        protected override void ApplyChanges()
        {
            GameObject projectile = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/Halcyonite/WhirlWindHalcyoniteProjectile.prefab").WaitForCompletion();
            ProjectileController pc = projectile.GetComponent<ProjectileController>();
            if (pc)
            {
                pc.cannotBeDeleted = true;
                projectile.AddComponent<RegisterHalcyoniteProjectile>();
            }

            GameObject bodyObject = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/Halcyonite/HalcyoniteBody.prefab").WaitForCompletion();
            bodyObject.AddComponent<HalcyoniteProjectileTracker>();
            On.EntityStates.Halcyonite.WhirlwindRush.OnExit += WhirlwindRush_OnExit;

            //Make sure all skills are interrupted
            var allStateMachines = bodyObject.GetComponents<EntityStateMachine>();
            SetStateOnHurt ssoh = bodyObject.GetComponent<SetStateOnHurt>();
            ssoh.idleStateMachine = allStateMachines.Where(esm => esm != ssoh.targetStateMachine).ToArray();
        }

        private void WhirlwindRush_OnExit(On.EntityStates.Halcyonite.WhirlwindRush.orig_OnExit orig, EntityStates.Halcyonite.WhirlwindRush self)
        {
            //Rerun this code because it's not compatible with interrupts in the basegame
            self.characterBody.baseMoveSpeed = self.originalMoveSpeed;
            self.characterBody.baseAcceleration = self.originalAccSpeed;
            if (self.characterMotor)
            {
                self.characterMotor.walkSpeedPenaltyCoefficient = 1f;
            }
            if (self.characterBody)
            {
                self.characterBody.isSprinting = false;
            }
            if (self.characterDirection)
            {
                self.characterDirection.moveVector = self.characterDirection.forward;
            }
            if (self.rigidbodyMotor)
            {
                self.rigidbodyMotor.moveVector = Vector3.zero;
            }
            Util.PlaySound("Stop_halcyonite_skill3_loop", self.gameObject);

            if (NetworkServer.active)
            {
                HalcyoniteProjectileTracker tracker = self.GetComponent<HalcyoniteProjectileTracker>();
                if (tracker) tracker.DestroyAllProjectilesServer();
            }

            orig(self);
        }

        public class HalcyoniteProjectileTracker : MonoBehaviour
        {
            private List<GameObject> activeProjectiles;

            private void Awake()
            {
                activeProjectiles = new List<GameObject>();
            }

            public void AddProjectileServer(GameObject projectile)
            {
                if (!NetworkServer.active) return;
                activeProjectiles = activeProjectiles.Where(go => go != null).ToList();
                activeProjectiles.Add(projectile);
            }

            public void DestroyAllProjectilesServer()
            {
                if (!NetworkServer.active) return;
                foreach (GameObject projectile in activeProjectiles)
                {
                    Destroy(projectile);
                }
                activeProjectiles = activeProjectiles.Where(go => go != null).ToList();
            }
        }

        public class RegisterHalcyoniteProjectile : MonoBehaviour
        {
            private void Start()
            {
                if (NetworkServer.active)
                {
                    ProjectileController pc = base.GetComponent<ProjectileController>();
                    if (pc && pc.owner)
                    {
                        HalcyoniteProjectileTracker tracker = pc.owner.GetComponent<HalcyoniteProjectileTracker>();
                        if (tracker) tracker.AddProjectileServer(base.gameObject);
                    }
                }

                Destroy(this);
            }
        }
    }
}
