﻿using BepInEx;
using System.Reflection;
using System;
using System.Linq;
using RiskyFixes.Fixes;

namespace RiskyFixes
{

    [BepInDependency("com.Moffein.EnigmaBlacklist", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.rune580.riskofoptions", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("dev.wildbook.multitudes", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency("com.TPDespair.ZetArtifacts", BepInDependency.DependencyFlags.SoftDependency)]

    [BepInPlugin("com.Moffein.RiskyFixes", "RiskyFixes", "1.0.0")]
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

            foreach (var artifactType in fixTypes)
            {
                FixBase fix = (FixBase)Activator.CreateInstance(artifactType);
                fix.Init(Config);
            }
        }
    }
}