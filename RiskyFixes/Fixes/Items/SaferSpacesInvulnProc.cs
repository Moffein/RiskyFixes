﻿using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RiskyFixes.Fixes.Items
{
    public class SaferSpacesInvulnProc : FixBase<SaferSpacesInvulnProc>
    {
        public override string ConfigCategoryString => "Items";

        public override string ConfigOptionName => "(Server-Side) Safer Spaces Invuln Fix";

        public override string ConfigDescriptionString => "Fixes Safer Spaces being wasted when hit while invincible.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.RoR2.HealthComponent.TakeDamageProcess += HealthComponent_TakeDamageProcess;
        }

        private void HealthComponent_TakeDamageProcess(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After, x => x.MatchLdsfld(typeof(DLC1Content.Buffs), "BearVoidReady")))
            {
                c.Index++;
                c.Emit(OpCodes.Ldarg_0);    //healthComponent
                c.Emit(OpCodes.Ldarg_1);    //damageInfo
                c.EmitDelegate<Func<bool, HealthComponent, DamageInfo, bool>>((hasBuff, self, damageInfo) =>
                {
                    if (!hasBuff) return false;

                    //Goal is to stop the proc if another invuln condition is active
                    bool shouldProcBear = !damageInfo.rejected && !self.godMode;
                    if (self.body)
                    {
                        bool isImmune = self.body.HasBuff(RoR2Content.Buffs.Immune) && !self.body.HasBuff(JunkContent.Buffs.GoldEmpowered);
                        bool isIframe = self.body.HasBuff(RoR2Content.Buffs.HiddenInvincibility) && !damageInfo.damageType.damageType.HasFlag(DamageType.BypassArmor);
                        bool isSojourn = (damageInfo.damageType & DamageTypeExtended.SojournVehicleDamage) <= 0UL & self.body.HasBuff(DLC2Content.Buffs.SojournVehicle);

                        shouldProcBear = shouldProcBear && !isIframe && !isImmune && !isSojourn;
                    }
                    return shouldProcBear;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: SaferSpacesInvulnProc IL Hook failed.");
            }
        }
    }
}
