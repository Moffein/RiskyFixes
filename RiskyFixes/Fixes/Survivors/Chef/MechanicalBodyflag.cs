using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.Survivors.Chef
{
    public class MechanicalBodyflag : FixBase<MechanicalBodyflag>
    {
        public override string ConfigCategoryString => "Survivors - CHEF";

        public override string ConfigOptionName => "(Server-Side) Mechanical Bodyflag";

        public override string ConfigDescriptionString => "Fix CHEF not being marked with the Mechanical bodyflag.";

        protected override void ApplyChanges()
        {
            GameObject gameObject = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/Chef/ChefBody.prefab").WaitForCompletion();

            CharacterBody cb = gameObject.GetComponent<CharacterBody>();
            if (cb) cb.bodyFlags |= CharacterBody.BodyFlags.Mechanical;
        }
    }
}
