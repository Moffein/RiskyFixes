using RoR2;
using RoR2.CharacterAI;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.Minions
{
    public class DontRetaliate : FixBase<DontRetaliate>
    {
        public override string ConfigCategoryString => "Minions";

        public override string ConfigOptionName => "(Server-Side) Dont Retaliate";

        public override string ConfigDescriptionString => "Fixes minions attempting to retaliate against their owner.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            //Engi
            ModifyAI(LoadMasterObject("EngiTurretMaster"));
            ModifyAI(LoadMasterObject("EngiWalkerTurretMaster"));
            ModifyAI(LoadMasterObject("EngiBeamTurretMaster"));

            //Drones
            ModifyAI(LoadMasterObject("Turret1Master"));
            ModifyAI(LoadMasterObject("Drone1Master"));
            ModifyAI(LoadMasterObject("MegaDroneMaster"));
            ModifyAI(LoadMasterObject("DroneMissileMaster"));
            ModifyAI(LoadMasterObject("FlameDroneMaster"));

            //Item Allies
            ModifyAI(LoadMasterObject("DroneBackupMaster"));
            ModifyAI(LoadMasterObject("SquidTurretMaster"));
            ModifyAI(LoadMasterObject("RoboBallGreenBuddyMaster"));
            ModifyAI(LoadMasterObject("RoboBallRedBuddyMaster"));
            ModifyAI(LoadMasterObject("BeetleGuardAllyMaster"));
            ModifyAI(Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Nullifier/NullifierAllyMaster.prefab").WaitForCompletion());
            ModifyAI(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidBarnacle/VoidBarnacleAllyMaster.prefab").WaitForCompletion());
            ModifyAI(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidJailer/VoidJailerAllyMaster.prefab").WaitForCompletion());
            ModifyAI(Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/VoidMegaCrab/VoidMegaCrabAllyMaster.prefab").WaitForCompletion());
        }

        private void ModifyAI(GameObject masterObject)
        {
            AimAtEnemy(masterObject.GetComponents<AISkillDriver>());
            SetDontRetaliate(masterObject.GetComponents<BaseAI>());
        }

        private void AimAtEnemy(AISkillDriver[] skillDrivers)
        {
            foreach (var skillDriver in skillDrivers) skillDriver.aimType = AISkillDriver.AimType.AtCurrentEnemy;
        }

        private void SetDontRetaliate(BaseAI[] baseAIs)
        {
            foreach (var baseAI in baseAIs) baseAI.neverRetaliateFriendlies = true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private GameObject LoadMasterObject(string mastername)
        {
            return LegacyResourcesAPI.Load<GameObject>("prefabs/charactermasters/" + mastername);
        }
    }
}
