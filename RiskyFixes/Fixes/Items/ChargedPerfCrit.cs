using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using RoR2.Orbs;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RiskyFixes.Fixes.Items
{
    public class ChargedPerfCrit : FixBase<ChargedPerfCrit>
    {
        public override string ConfigCategoryString => "Items";

        public override string ConfigOptionName => "(Server-Side) Charged Perforator Crit Fix";

        public override string ConfigDescriptionString => "Charged Perforator inherits crit instead of rerolling.";

        protected override void ApplyChanges()
        {
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Gorakh.CherfInheritCrit"))
            {
                Debug.LogWarning("RiskyFixes: Skipping ChargedPerfCrit because standalone plugin is loaded.");
                return;
            }
            IL.RoR2.GlobalEventManager.ProcessHitEnemy += FixChargedPerforator;
        }

        private void FixChargedPerforator(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(
                 x => x.MatchLdsfld(typeof(RoR2Content.Items), "LightningStrikeOnHit")
                )
            &&
            c.TryGotoNext(
                x => x.MatchStfld(typeof(GenericDamageOrb), "isCrit")
                ))
            {
                c.Emit(OpCodes.Ldarg_1);    //DamageInfo
                c.EmitDelegate<Func<bool, DamageInfo, bool>>((isCrit, damageInfo) =>
                {
                    return damageInfo.crit;
                });
            }
            else
            {
                UnityEngine.Debug.LogError("RiskyFixes: ChargedPerfCrit IL Hook failed");
            }
        }
    }
}
