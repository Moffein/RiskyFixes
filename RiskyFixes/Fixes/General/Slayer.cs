using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RiskyFixes.Fixes.General
{
    public class Slayer : FixBase<Slayer>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "(Server-Side) Slayer Procs";

        public override string ConfigDescriptionString => "Fixes Slayer damagetype (bonus damage to low health) not affecting procs.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            On.RoR2.HealthComponent.TakeDamageProcess += HealthComponent_TakeDamageProcess;
        }

        private void HealthComponent_TakeDamageProcess(On.RoR2.HealthComponent.orig_TakeDamageProcess orig, RoR2.HealthComponent self, RoR2.DamageInfo damageInfo)
        {
            if ((damageInfo.damageType & DamageType.BonusToLowHealth) == DamageType.BonusToLowHealth)
            {
                damageInfo.damageType.damageType &= ~DamageType.BonusToLowHealth;
                damageInfo.damage *= Mathf.Lerp(3f, 1f, self.combinedHealthFraction);
            }
            orig(self, damageInfo);
        }
    }
}
