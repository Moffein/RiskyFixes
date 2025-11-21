using BepInEx;
using System.Reflection;
using System;
using System.Linq;
using RiskyFixes.Fixes;
using R2API.Utils;

namespace RiskyFixes
{
    [BepInDependency("com.Moffein.AI_Blacklist", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.Moffein.EnigmaBlacklist", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("dev.wildbook.multitudes", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.TPDespair.ZetArtifacts", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("local.difficulty.multitudes", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Gorakh.CherfInheritCrit", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("Gorakh.VagrantOrbFix", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("LordVGames.LunarRuinDamageNerf", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.RiskyLives.LinearDamage", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.RiskyLives.SneedHooks")]
    [BepInDependency(R2API.R2API.PluginGUID)]

    [NetworkCompatibility(CompatibilityLevel.NoNeedForSync, VersionStrictness.DifferentModVersionsAreOk)]
    [BepInPlugin("com.Moffein.RiskyFixes", "RiskyFixes", "1.9.1")]
    public class RiskyFixesPlugin : BaseUnityPlugin
    {
        private void Awake()
        {
            ModCompat.Init();
            AddToAssembly();
        }

        private void AddToAssembly()
        {
            var fixTypes = Assembly.GetExecutingAssembly().GetTypes().Where(type => !type.IsAbstract && type.IsSubclassOf(typeof(FixBase)));

            foreach (var fixType in fixTypes)
            {
                FixBase fix = (FixBase)Activator.CreateInstance(fixType);
                fix.Init(Config);
            }
        }
    }
}
