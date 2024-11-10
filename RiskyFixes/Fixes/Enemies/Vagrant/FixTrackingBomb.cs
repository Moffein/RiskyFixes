using RiskOfOptions.Resources;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.Enemies.Vagrant
{
    public class FixTrackingBomb : FixBase<FixTrackingBomb>
    {
        public override string ConfigCategoryString => "Enemies - Wandering Vagrant";

        public override string ConfigOptionName => "Fix Tracking Bomb";

        public override string ConfigDescriptionString => "Fixes Tracking Bomb being invincible.";

        protected override void ApplyChanges()
        {
            if (BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("Gorakh.VagrantOrbFix"))
            {
                Debug.LogWarning("RiskyFixes: Skipping FixTrackingBomb because standalone plugin is loaded.");
                return;
            }

            GameObject prefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/Scorchling/ScorchlingBombProjectile.prefab").WaitForCompletion();
            Transform model = prefab.transform.Find("Model");
            ModelLocator modelLocator = prefab.GetComponent<ModelLocator>();
            modelLocator._modelTransform = model;
        }
    }
}
