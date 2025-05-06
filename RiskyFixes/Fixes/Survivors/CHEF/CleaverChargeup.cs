using System;
using System.Collections.Generic;
using System.Text;

namespace RiskyFixes.Fixes.Survivors.CHEF
{
    public class CleaverChargeup : FixBase<CleaverChargeup>
    {
        public override string ConfigCategoryString => "Survivors - CHEF";

        public override string ConfigOptionName => "Dice Chargeup Attack Speed";

        public override string ConfigDescriptionString => "Fixes Dice's double damage hold duration not scaling with attack speed.";

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
            }
        }
    }
}
