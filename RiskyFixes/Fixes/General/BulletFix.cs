using RoR2;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskyFixes.Fixes.General
{
    //Credits to Nuxlar for this fix
    public class BulletFix : FixBase<BulletFix>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "(Client-Side) Bullet Self-Hit Fix";

        public override string ConfigDescriptionString => "Fixes bulletattacks sometimes being able to hit yourself.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            On.RoR2.BulletAttack.ProcessHitList += BulletAttack_ProcessHitList;
        }

        private UnityEngine.GameObject BulletAttack_ProcessHitList(On.RoR2.BulletAttack.orig_ProcessHitList orig, BulletAttack self, List<BulletAttack.BulletHit> hits, ref UnityEngine.Vector3 endPosition, List<UnityEngine.GameObject> ignoreList)
        {
            if (self.owner) ignoreList.Add(self.owner);
            return orig(self, hits, ref endPosition, ignoreList);
        }
    }
}
