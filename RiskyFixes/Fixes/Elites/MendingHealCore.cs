using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.Elites
{
    public class MendingHealCore : FixBase<MendingHealCore>
    {
        public override string ConfigCategoryString => "Elites";

        public override string ConfigOptionName => "(Server-Side) Mending Healing Core Level Scaling";

        public override string ConfigDescriptionString => "Fixes Healing Cores not scaling with level.";

        protected override void ApplyChanges()
        {
            GameObject healCore = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC1/EliteEarth/AffixEarthHealerBody.prefab").WaitForCompletion();
            CharacterBody body = healCore.GetComponent<CharacterBody>();
            body.levelMaxHealth = body.baseMaxHealth * 0.3f;
            body.levelDamage = body.baseDamage * 0.2f;
        }
    }
}
