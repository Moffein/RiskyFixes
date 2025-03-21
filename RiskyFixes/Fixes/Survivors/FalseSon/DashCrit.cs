using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using UnityEngine;

namespace RiskyFixes.Fixes.Survivors.FalseSon
{
    public class DashCrit : FixBase<DashCrit>
    {
        public override string ConfigCategoryString => "Survivors - False Son";

        public override string ConfigOptionName => "(Client-Side) Step of the Brothers - Fix Crit";

        public override string ConfigDescriptionString => "Fixes Step of the Brothers not rolling for crits.";

        protected override void ApplyChanges()
        {
            On.EntityStates.FalseSon.StepBrothers.OnEnter += StepBrothers_OnEnter;
            IL.EntityStates.FalseSon.StepBrothers.FixedUpdate += StepBrothers_FixedUpdate;
        }

        private void StepBrothers_OnEnter(On.EntityStates.FalseSon.StepBrothers.orig_OnEnter orig, EntityStates.FalseSon.StepBrothers self)
        {
            orig(self);
            if (self.overlapAttack != null)
            {
                self.overlapAttack.isCrit = self.RollCrit();
            }
        }

        private void StepBrothers_FixedUpdate(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(x => x.MatchCallvirt<BlastAttack>("Fire")))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<BlastAttack, EntityStates.FalseSon.StepBrothers, BlastAttack>>((blastAttack, self) =>
                {
                    if (self.overlapAttack != null)
                    {
                        blastAttack.crit = self.overlapAttack.isCrit;
                    }
                    return blastAttack;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: False Son DashCrit IL Hook failed.");
            }
        }
    }
}
