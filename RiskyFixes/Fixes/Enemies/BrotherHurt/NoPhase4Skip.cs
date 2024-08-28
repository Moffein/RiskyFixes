using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;
using UnityEngine;
using RoR2;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.Enemies.BrotherHurt
{
    //Kinda jank
    public class NoPhase4Skip : FixBase<NoPhase4Skip>
    {
        public override string ConfigCategoryString => "Enemies - Mithrix (Phase 4)";

        public override string ConfigOptionName => "(Server-Side) Prevent Phase Skip";

        public override string ConfigDescriptionString => "Fixes the bug where Mithrix is instakilled if he takes damage at the start of the phase.";

        public override bool StopLoadOnConfigDisable => true;

        private BodyIndex brotherHurtIndex;

        protected override void ApplyChanges()
        {
            //Generally directly modifying vanilla prefabs should be avoided for a Vanilla-Compatible mod.
            GameObject brotherHurtPrefab = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Brother/BrotherHurtBody.prefab").WaitForCompletion();
            brotherHurtPrefab.AddComponent<Phase4ImmuneComponent>();

            RoR2Application.onLoad += OnLoad;
            On.RoR2.HealthComponent.TakeDamageProcess += HealthComponent_TakeDamageProcess;
            On.EntityStates.BrotherMonster.SpellBaseState.OnExit += SpellBaseState_OnExit;
        }

        private void SpellBaseState_OnExit(On.EntityStates.BrotherMonster.SpellBaseState.orig_OnExit orig, EntityStates.BrotherMonster.SpellBaseState self)
        {
            Phase4ImmuneComponent p4 = self.GetComponent<Phase4ImmuneComponent>();
            if (p4 && p4.spawnImmuneDuration > 0.1f) p4.spawnImmuneDuration = 0.1f; //Add a tiny delay just in case

            orig(self);
        }

        private void HealthComponent_TakeDamageProcess(On.RoR2.HealthComponent.orig_TakeDamageProcess orig, HealthComponent self, DamageInfo damageInfo)
        {
            if (NetworkServer.active && self.body.bodyIndex == brotherHurtIndex)
            {
                if (self.GetComponent<Phase4ImmuneComponent>())
                {
                    damageInfo.rejected = true;
                }
            }
            orig(self, damageInfo);
        }

        private void OnLoad()
        {
            brotherHurtIndex = BodyCatalog.FindBodyIndex("BrotherHurtBody");
        }
    }

    //Having this on BrotherHurt prevents him from taking damage
    public class Phase4ImmuneComponent : MonoBehaviour
    {
        public float spawnImmuneDuration = 10f; //Failsafe in case this doesnt get deleted for whatever reason.

        private void Awake()
        {
            if (!NetworkServer.active) Destroy(this);
        }

        private void FixedUpdate()
        {
            spawnImmuneDuration -= Time.fixedDeltaTime;
            if (spawnImmuneDuration <= 0f)
            {
                Destroy(this);
                return;
            }
        }
    }
}
