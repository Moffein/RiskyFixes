using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskyFixes.Fixes.Items
{
    public class ScorpionSelfProc : FixBase<ScorpionSelfProc>
    {
        public override string ConfigCategoryString => "Items";

        public override string ConfigOptionName => "Symbiotic Scorpion Self Proc";

        public override string ConfigDescriptionString => "Fixes cases where Symbiotic Scorpion can proc on self damage.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.RoR2.HealthComponent.TakeDamageProcess += HealthComponent_TakeDamageProcess;
        }

        private void HealthComponent_TakeDamageProcess(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
                 x => x.MatchLdsfld(typeof(DLC1Content.Items), "PermanentDebuffOnHit")
                ))
            {
                c.Emit(OpCodes.Ldarg_0);    //HealthComponent
                c.Emit(OpCodes.Ldarg_1);    //DamageInfo
                c.EmitDelegate<Func<BuffDef, HealthComponent, DamageInfo, BuffDef>>((buff, self, damageInfo) =>
                {
                    return (self.gameObject != damageInfo.attacker ? buff : null);
                });
            }
            else
            {
                UnityEngine.Debug.LogError("RiskyFixes: ScorpionSelfProc IL Hook failed");
            }
        }
    }
}
