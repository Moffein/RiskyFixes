using RoR2;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.Enemies
{
    public class SolusSpawnrates : FixBase<SolusSpawnrates>
    {
        public override string ConfigCategoryString => "Enenemies - DLC3";

        public override string ConfigOptionName => "Fix spawncosts.";

        public override string ConfigDescriptionString => "Fix spawncosts for most DLC3 enemies being set too low.";

        protected override void ApplyChanges()
        {
            SetSpawnCost(Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC3/WorkerUnit/cscWorkerUnit.asset").WaitForCompletion(), 16);  //Prospector 11->16, 14.5->10
            SetSpawnCost(Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC3/Tanker/cscTanker.asset").WaitForCompletion(), 35);  //Scorcher 18->35, 9.72 -> 5
            SetSpawnCost(Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC3/MinePod/cscMinePod.asset").WaitForCompletion(), 52);  //Distributor 20->52, 13 -> 5
            SetSpawnCost(Addressables.LoadAssetAsync<CharacterSpawnCard>("RoR2/DLC3/DefectiveUnit/cscDefectiveUnit.asset").WaitForCompletion(), 80);  //Invalidator 40->80, 10 -> 5
        }

        private void SetSpawnCost(CharacterSpawnCard csc, int cost)
        {
            if (csc.directorCreditCost < cost) csc.directorCreditCost = cost;
        }
    }
}
