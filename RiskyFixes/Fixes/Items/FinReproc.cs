using MonoMod.Cil;
using RiskyFixes.Fixes.SharedHooks;
using RoR2;
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
            ModifyFinalDamage.ModifyFinalDamageActions += ModifyDamage;
        }

        private void KnockbackFinUtil_ModifyDamageInfo(On.RoR2.KnockbackFinUtil.orig_ModifyDamageInfo orig, ref DamageInfo damageInfo, CharacterBody attacker, CharacterBody victim)
        {
            return;
        }

        private void ModifyDamage(ModifyFinalDamage.DamageMult damageMult, DamageInfo damageInfo, HealthComponent victim, CharacterBody victimBody)
        {
            int buffCount = victimBody.GetBuffCount(DLC2Content.Buffs.KnockUpHitEnemiesJuggleCount);
            if (buffCount > 0)
            {
                float mult = Mathf.Max(1f, (float)buffCount) * damageBuffPerStack;
                damageMult.value += mult;

                if (!damageInfo.crit)
                {
                    damageInfo.damageColorIndex = DamageColorIndex.KnockBackHitEnemies;
                }
            }
        }
    }
}
