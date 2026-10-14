using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.Enemies.Providence
{
    public class AntiSpoilers : FixBase<AntiSpoilers>
    {
        public override string ConfigCategoryString => "Enemies - DLC4 Secret Boss";

        public override string ConfigOptionName => "(Client-Side) Prevent spoilers on ping.";

        public override string ConfigDescriptionString => "Prevents getting spoiled on the DLC4 Secret Boss when pinging.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            GameObject bodyPrefab = Addressables.LoadAssetAsync<GameObject>("4aa2cdd8f36bb944b91c9f13b6efbee2").WaitForCompletion();
            CharacterBody body = bodyPrefab.GetComponent<CharacterBody>();
            body.baseNameToken = "UNIDENTIFIED";
            body.subtitleNameToken = "UNIDENTIFIED";
        }
    }
}
