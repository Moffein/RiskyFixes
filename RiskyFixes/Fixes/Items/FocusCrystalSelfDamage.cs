using MonoMod.Cil;
using System;
using RoR2;
using Mono.Cecil.Cil;
using UnityEngine;

namespace RiskyFixes.Fixes.Items
{
    public class FocusCrystalSelfDamage : FixBase<FocusCrystalSelfDamage>
    {
        public override string ConfigCategoryString => "Items";

        public override string ConfigOptionName => "(Server-Side) Focus Crystal Self Damage";

        public override string ConfigDescriptionString => "Prevents Focus Crystal from applying to self damage.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.RoR2.HealthComponent.TakeDamageProcess += HealthComponent_TakeDamageProcess;
        }

        private void HealthComponent_TakeDamageProcess(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
                 x => x.MatchLdsfld(typeof(RoR2Content.Items), "NearbyDamageBonus")
                ))
            {
                c.Index += 1;
                c.Emit(OpCodes.Ldarg_0);//victim healthcomponent
                c.Emit(OpCodes.Ldarg_1);//damageInfo
                c.EmitDelegate<Func<int, HealthComponent, DamageInfo, int>>((itemCount, self, damageInfo) =>
                {
                    if (itemCount > 0 && self.body && damageInfo.attacker)
                    {
                        if (self.body.gameObject == damageInfo.attacker)
                        {
                            itemCount = 0;
                        }
                    }
                    return itemCount;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: Items FixFocusCrystalSelfDamage IL Hook failed");
            }
        }
    }
}
