using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.General
{
    public class SmallHoldoutCharging : FixBase<SmallHoldoutCharging>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "(Server-Side) Small Holdout Charging";

        public override string ConfigDescriptionString => "Small holdouts charge at full speed as long as 1 player is in the radius.";

        protected override void ApplyChanges()
        {
            //I think only NullSafeWard and DropshipZone actually get changed by this.
            string[] toFix = new string[]
            {
                "RoR2/Base/moon2/MoonBatteryBlood.prefab",
                "RoR2/Base/moon2/MoonBatteryDesign.prefab",
                "RoR2/Base/moon2/MoonBatteryMass.prefab",
                "RoR2/Base/moon2/MoonBatterySoul.prefab",
                "RoR2/DLC1/DeepVoidPortalBattery/DeepVoidPortalBattery.prefab",
                "RoR2/Base/NullSafeWard/NullSafeWard.prefab",
                "RoR2/Base/moon2/Moon2DropshipZone.prefab"
            };
            foreach (string str in toFix)
            {
                FixHoldout(Addressables.LoadAssetAsync<GameObject>(str).WaitForCompletion());
            }
        }

        public static void FixHoldout(GameObject gameObject)
        {
            if (!gameObject) return;

            HoldoutZoneController hzc = gameObject.GetComponentInChildren<HoldoutZoneController>();
            if (hzc)
            {
                hzc.playerCountScaling = 0; //Power of 0 = 100% charge effectiveness at all times.
            }
        }
    }
}
