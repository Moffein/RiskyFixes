using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RiskyFixes.Fixes.Enemies.Scorchling
{
    public class FixEnemyDetectionNullref : FixBase<FixEnemyDetectionNullref>
    {
        public override string ConfigCategoryString => "Enemies - Scorch Wurm";

        public override string ConfigOptionName => "(Client-Side) Fix Nullref Spam";

        public override string ConfigDescriptionString => "Fixes nullref spam from EnemyDetection.";

        protected override void ApplyChanges()
        {
            On.EnemyDetection.Enable += EnemyDetection_Enable;
            On.EnemyDetection.Disable += EnemyDetection_Disable;
        }

        private void EnemyDetection_Disable(On.EnemyDetection.orig_Disable orig, EnemyDetection self)
        {
            if (!self.detectionCollider)
            {
                self.Start();
            }
            orig(self);
        }

        private void EnemyDetection_Enable(On.EnemyDetection.orig_Enable orig, EnemyDetection self)
        {
            if (!self.detectionCollider)
            {
                self.Start();
            }
            orig(self);
        }
    }
}
