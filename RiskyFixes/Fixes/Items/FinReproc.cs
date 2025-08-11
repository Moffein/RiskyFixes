using MonoMod.Cil;
using RoR2;
using SneedHooks;
using System;
using UnityEngine;

namespace RiskyFixes.Fixes.Items
{
    public class FinReproc : FixBase<FinReproc>
    {
        public static float damageBuffPerStack = 0.1f;

        public override string ConfigCategoryString => "Items";

        public override string ConfigOptionName => "(Server-Side) Breaching Fin - Fix Re-Proc";

        public override string ConfigDescriptionString => "Fixes Fin being able to boost damage multiple times in the same proc chain.";

        protected override void ApplyChanges()
        {
            On.RoR2.KnockbackFinUtil.ModifyDamageInfo += KnockbackFinUtil_ModifyDamageInfo;

            if (ModCompat.LinearDamageCompat.pluginLoaded)
            {
                SneedHooks.ModifyFinalDamage.ModifyFinalDamageActions += ModifyFinalDamage_Additive;
            }
            else
            {
                SneedHooks.ModifyFinalDamage.ModifyFinalDamageActions += ModifyFinalDamage;
            }
        }

        private void ModifyFinalDamage(ModifyFinalDamage.DamageModifierArgs damageModifierArgs, DamageInfo damageInfo, HealthComponent victim, CharacterBody victimBody)
        {
            int buffCount = victimBody.GetBuffCount(DLC2Content.Buffs.KnockUpHitEnemiesJuggleCount);
            if (buffCount > 0)
            {
                damageModifierArgs.damageMultFinal *= 1f + Mathf.Max(1, buffCount) * damageBuffPerStack;
                if (!damageInfo.crit)
                {
                    damageInfo.damageColorIndex = DamageColorIndex.KnockBackHitEnemies;
                }
            }
        }

        private void ModifyFinalDamage_Additive(ModifyFinalDamage.DamageModifierArgs damageModifierArgs, DamageInfo damageInfo, HealthComponent victim, CharacterBody victimBody)
        {
            int buffCount = victimBody.GetBuffCount(DLC2Content.Buffs.KnockUpHitEnemiesJuggleCount);
            if (buffCount > 0)
            {
                damageModifierArgs.damageMultAdd += Mathf.Max(1, buffCount) * damageBuffPerStack;
                if (!damageInfo.crit)
                {
                    damageInfo.damageColorIndex = DamageColorIndex.KnockBackHitEnemies;
                }
            }
        }

        private void KnockbackFinUtil_ModifyDamageInfo(On.RoR2.KnockbackFinUtil.orig_ModifyDamageInfo orig, ref DamageInfo damageInfo, CharacterBody attacker, CharacterBody victim)
        {
            return;
        }
    }
}
