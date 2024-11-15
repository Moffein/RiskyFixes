using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using UnityEngine;

namespace RiskyFixes.Fixes.General
{
    public class SotSDoTFix : FixBase<SotSDoTFix>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "(Server-Side) DoT Fix";

        public override string ConfigDescriptionString => "Fix the SotS bug where DoT stacks disappear randomly.";

        protected override void ApplyChanges()
        {
            IL.RoR2.BurnEffectController.HandleDestroy += BurnEffectController_HandleDestroy;
        }

        private void BurnEffectController_HandleDestroy(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (c.TryGotoNext(x => x.MatchCall(typeof(UnityEngine.Object), "Destroy")))
            {
                c.Emit(OpCodes.Ldarg_0);//BurnEffectController
                c.EmitDelegate<Func<UnityEngine.Object, BurnEffectController, UnityEngine.Object>>((orig, bec) =>
                {
                    return bec;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: SotSDoTFix BurnEffectController_HandleDestroy IL Hook failed.");
            }
        }
    }
}
