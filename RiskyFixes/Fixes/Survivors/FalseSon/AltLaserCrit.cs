using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using UnityEngine;

namespace RiskyFixes.Fixes.Survivors.FalseSon
{
    public class AltLaserCrit : FixBase<AltLaserCrit>
    {
        public override string ConfigCategoryString => "Survivors - False Son";

        public override string ConfigOptionName => "(Client-Side) Laser Burst - Fix Crit";

        public override string ConfigDescriptionString => "Fixes Laser Burst not rolling for crits.";

        protected override void ApplyChanges()
        {
            IL.EntityStates.FalseSon.LaserFatherBurst.FireBurstLaser += LaserFatherBurst_FireBurstLaser;
        }

        private void LaserFatherBurst_FireBurstLaser(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(x => x.MatchCallvirt<BlastAttack>("Fire")))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<BlastAttack, EntityStates.FalseSon.LaserFatherBurst, BlastAttack>>((blastAttack, self) =>
                {
                    blastAttack.crit = self.RollCrit();
                    return blastAttack;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: False Son AltLaserCrit IL Hook failed.");
            }
        }
    }
}
