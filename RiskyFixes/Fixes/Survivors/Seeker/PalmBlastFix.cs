using System;
using System.Collections.Generic;
using System.Text;

namespace RiskyFixes.Fixes.Survivors.Seeker
{
    public class PalmBlastFix : FixBase<PalmBlastFix>
    {
        public override string ConfigCategoryString => "Survivors - Seeker";

        public override string ConfigOptionName => "(Server-Side) Palm Blast Fixes";
        public override string ConfigDescriptionString => "Fixes Palm Blast not rolling for crit and not scaling with level.";

        protected override void ApplyChanges()
        {
            On.PalmBlastProjectileController.Init += PalmBlastProjectileController_Init;
        }

        private void PalmBlastProjectileController_Init(On.PalmBlastProjectileController.orig_Init orig, PalmBlastProjectileController self, RoR2.CharacterBody body)
        {
            orig(self, body);
            if (self.projectileDamage && body)
            {
                self.projectileDamage.crit = body.RollCrit();
                self.projectileDamage.damage = body.damage;
            }
        }
    }
}
