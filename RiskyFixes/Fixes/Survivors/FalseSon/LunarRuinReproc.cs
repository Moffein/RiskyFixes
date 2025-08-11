using UnityEngine;
using RoR2;
using MonoMod.Cil;
using System;
using SneedHooks;

namespace RiskyFixes.Fixes.Survivors.FalseSon
{
    public class LunarRuinReproc : FixBase<LunarRuinReproc>
    {
        public override string ConfigCategoryString => "Survivors - False Son";

        public override string ConfigOptionName => "(Server-Side) Lunar Ruin Exponential Damage Removal";

        public override string ConfigDescriptionString => "Prevent Lunar Ruin from exponentially increasing damage in a proc chain.";

        public static float damageMultPerBuff = 0.1f;

        protected override void ApplyChanges()
        {
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("LordVGames.LunarRuinDamageNerf"))
            {
                Debug.LogWarning("RiskyFixes: Skipping LunarRuinReproc because standalone plugin is loaded.");
                return;
            }

            IL.RoR2.HealthComponent.TakeDamageProcess += HealthComponent_TakeDamageProcess;

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
            int buffCount = victimBody.GetBuffCount(DLC2Content.Buffs.lunarruin);
            if (buffCount > 0)
            {
                damageModifierArgs.damageMultFinal *= 1f + (damageMultPerBuff * buffCount);
                damageInfo.damageColorIndex = DamageColorIndex.Void;
            }
        }

        private void ModifyFinalDamage_Additive(ModifyFinalDamage.DamageModifierArgs damageModifierArgs, DamageInfo damageInfo, HealthComponent victim, CharacterBody victimBody)
        {
            int buffCount = victimBody.GetBuffCount(DLC2Content.Buffs.lunarruin);
            if (buffCount > 0)
            {
                damageModifierArgs.damageMultAdd += damageMultPerBuff * buffCount;
                damageInfo.damageColorIndex = DamageColorIndex.Void;
            }
        }

        private void HealthComponent_TakeDamageProcess(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After, x => x.MatchLdsfld(typeof(DLC2Content.Buffs), "lunarruin")))
            {
                c.EmitDelegate<Func<BuffDef, BuffDef>>(buff => null);
            }
            else
            {
                Debug.LogError("RiskyFixes: LunarRuinReproc IL hook failed.");
            }
        }
    }
}
