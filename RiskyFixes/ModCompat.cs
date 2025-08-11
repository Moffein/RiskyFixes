using RoR2;
using System.Runtime.CompilerServices;
using Local.Difficulty.Multitudes;
using TPDespair.ZetArtifacts;
using UnityEngine;

namespace RiskyFixes
{
    internal static class ModCompat
    {
        internal static void Init()
        {
            RiskOfOptionsCompat.pluginLoaded = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.rune580.riskofoptions");
            EnigmaBlacklistCompat.pluginLoaded = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Moffein.EnigmaBlacklist");
            MultitudesCompat.pluginLoaded = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("dev.wildbook.multitudes");
            ZetArtifactsCompat.pluginLoaded = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.TPDespair.ZetArtifacts");
            AIBlacklist.pluginLoaded = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Moffein.AI_Blacklist");
            MultitudesDifficultyCompat.pluginLoaded = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("local.difficulty.multitudes");
            LinearDamageCompat.pluginLoaded = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.Moffein.LinearDamage");
        }

        public static class LinearDamageCompat
        {
            public static bool pluginLoaded;
        }

        public static class RiskOfOptionsCompat
        {
            public static bool pluginLoaded;
        }

        public static class EnigmaBlacklistCompat
        {
            public static bool pluginLoaded;
        }

        public static class MultitudesCompat
        {
            public static bool pluginLoaded;

            public static int GetMultiplier()
            {
                if (!pluginLoaded) return 1;
                return GetMultiplierInternal();
            }

            [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
            private static int GetMultiplierInternal()
            {
                return Multitudes.Multitudes.Multiplier;
            }
        }

        public static class ZetArtifactsCompat
        {
            public static bool pluginLoaded;
            
            public static class Multifact
            {
                public static int GetMultiplier()
                {
                    if (!pluginLoaded) return 1;
                    return GetMultiplierInternal();
                }

                [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
                private static int GetMultiplierInternal()
                {
                    if (TPDespair.ZetArtifacts.ZetMultifact.Enabled && RunArtifactManager.instance.IsArtifactEnabled(ZetArtifactsContent.Artifacts.ZetMultifact))
                    {
                        return Mathf.Max(2, ZetArtifactsPlugin.MultifactMultiplier.Value); //GetMultiplier is private so I copypasted the code.
                    }
                    return 1;
                }
            }
        }

        public static class MultitudesDifficultyCompat
        {
            public static bool pluginLoaded;

            public static int GetAdditional()
            {
                if (!pluginLoaded) return 0;
                return GetAdditionalInternal();
            }

            [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
            private static int GetAdditionalInternal()
            {
                if (Setup.forceEnable || Run.instance?.selectedDifficulty == Setup.index)
                {
                    return (int) Session.additionalPlayers;
                }
                return 0;
            }
        }

        public static class AIBlacklist
        {
            public static bool pluginLoaded;
        }
    }
}
