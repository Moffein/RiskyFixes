using System;
using System.Linq;
using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.Enemies.MagmaWorm
{
    public class FixHeadshot : FixBase<FixHeadshot>
    {
        public override string ConfigCategoryString => "Enemies - Magma Worm";

        public override string ConfigOptionName => "(Client-Side) Headshot Fix";

        public override string ConfigDescriptionString => "Fixes Magma Worms not taking crit damage from headshots.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            GameObject magmaWorm = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/MagmaWorm/MagmaWormBody.prefab").WaitForCompletion();
            FixHitbox(magmaWorm);

            GameObject electricWorm = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/ElectricWorm/ElectricWormBody.prefab").WaitForCompletion();
            FixHitbox(electricWorm);
        }

        private void FixHitbox(GameObject bodyPrefab)
        {
            ModelLocator ml = bodyPrefab.GetComponent<ModelLocator>();
            if (!ml || !ml.modelTransform || !ml.modelTransform.gameObject) return;

            HurtBoxGroup hbg = ml.modelTransform.gameObject.GetComponent<HurtBoxGroup>();
            if (!hbg) return;

            var allHurtboxes = ml.modelTransform.gameObject.GetComponentsInChildren<HurtBox>();

            foreach (HurtBox hb in allHurtboxes)
            {
                if (!hbg.hurtBoxes.Contains(hb))
                {
                    var list = hbg.hurtBoxes.ToList();
                    list.Add(hb);
                    hbg.hurtBoxes = list.ToArray();
                }
            }
        }
    }
}
