using System;
using System.Collections.Generic;
using System.Text;

namespace RiskyFixes.Fixes.Survivors.CHEF
{
    public class CleaverAttackSpeed : FixBase<CleaverAttackSpeed>
    {
        public override string ConfigCategoryString => "Survivors - CHEF";

        public override string ConfigOptionName => "Dice Attack Speed";

        public override string ConfigDescriptionString => "Fixes various aspects of Dice not scaling with attack speed.";

        protected override void ApplyChanges()
        {
            On.RoR2.Projectile.CleaverProjectile.Start += CleaverProjectile_Start;
        }

        private void CleaverProjectile_Start(On.RoR2.Projectile.CleaverProjectile.orig_Start orig, RoR2.Projectile.CleaverProjectile self)
        {
            orig(self);
            if (self.chefController && self.chefController.characterBody)
            {
                self.holdChargeTime /= self.chefController.characterBody.attackSpeed;
                self.minTravelTime /= self.chefController.characterBody.attackSpeed;
            }
        }
    }
}
