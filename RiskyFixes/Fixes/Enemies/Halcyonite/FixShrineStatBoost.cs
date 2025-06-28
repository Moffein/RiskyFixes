using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.Enemies.Halcyonite
{
    public class FixShrineStatBoost : FixBase<FixShrineStatBoost>
    {
        public override string ConfigCategoryString => "Enemies - Halcyonite";

        public override string ConfigOptionName => "(Server-Side) Shrine Scaling Fix";

        public override string ConfigDescriptionString => "Fixes Halcyonite Shrines giving too many stat boosts due to not factoring in gold scaling.";

        private static int origGoldDrainValue = 1;
        protected override void ApplyChanges()
        {
            RoR2Application.onLoad += GetOrigGoldDrain;
            IL.RoR2.HalcyoniteShrineInteractable.DrainConditionMet += HalcyoniteShrineInteractable_DrainConditionMet;
        }

        private void GetOrigGoldDrain()
        {
            GameObject prefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/ShrineHalcyonite.prefab").WaitForCompletion();
            HalcyoniteShrineInteractable hsi = prefab.GetComponent<HalcyoniteShrineInteractable>();
            origGoldDrainValue = hsi.goldDrainValue;
        }

        private void HalcyoniteShrineInteractable_DrainConditionMet(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After, x => x.MatchLdcR4(100f)))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<float, HalcyoniteShrineInteractable, float>>((divisor, self) =>
                {
                    if (self.automaticallyScaleCostWithDifficulty && self.goldDrainValue > 0)
                    {
                        return divisor * ((float)self.goldDrainValue / (float)FixShrineStatBoost.origGoldDrainValue);
                    }
                    return divisor;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: Halcyonite FixShrineStatBoost IL hook failed.");
            }
        }
    }
}
