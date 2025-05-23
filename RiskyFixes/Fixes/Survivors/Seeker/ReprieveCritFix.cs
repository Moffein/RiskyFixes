using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.Projectile;
using System;
using UnityEngine;

namespace RiskyFixes.Fixes.Survivors.Seeker
{
    public class ReprieveCritFix : FixBase<ReprieveCritFix>
    {
        public override string ConfigCategoryString => "Survivors - Seeker";
        public override string ConfigOptionName => "(Server-Side) Reprieve Crit Fix";

        public override string ConfigDescriptionString => "Fixes Reprieve not rolling for crits.";

        protected override void ApplyChanges()
        {
            IL.RoR2.ReprieveVehicle.EndSojournVehicle += ReprieveVehicle_EndSojournVehicle;
        }

        private void ReprieveVehicle_EndSojournVehicle(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(x => x.MatchCallvirt<RoR2.Projectile.ProjectileManager>("FireProjectile")))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<FireProjectileInfo, ReprieveVehicle, FireProjectileInfo>>((info, self) =>
                {
                    if (self.characterBody)
                    {
                        info.crit = self.characterBody.RollCrit();
                    }
                    return info;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: Seeker ReprieveCritFix IL hook failed.");
            }
        }
    }
}
