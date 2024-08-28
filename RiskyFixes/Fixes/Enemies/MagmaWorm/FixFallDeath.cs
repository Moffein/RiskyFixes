using RoR2;
using UnityEngine;

namespace RiskyFixes.Fixes.Enemies.MagmaWorm
{
    public class FixFallDeath : FixBase<FixFallDeath>
    {
        public override string ConfigCategoryString => "Enemies - Magma Worm";

        public override string ConfigOptionName => "(Server-Side) Prevent Fall Death";

        public override string ConfigDescriptionString => "Prevents Magma Worms from dying to fall triggers.";

        public override bool StopLoadOnConfigDisable => true;


        private BodyIndex magmaWormIndex;
        private BodyIndex overloadingWormIndex;

        protected override void ApplyChanges()
        {
            RoR2Application.onLoad += RoR2Application_OnLoad;
            On.RoR2.CharacterBody.Start += CharacterBody_Start;
        }

        private void CharacterBody_Start(On.RoR2.CharacterBody.orig_Start orig, CharacterBody self)
        {
            orig(self);
            if (self.inventory && (self.bodyIndex == magmaWormIndex || self.bodyIndex == overloadingWormIndex))
            {
                self.inventory.GiveItem(RoR2Content.Items.TeleportWhenOob);
            }
        }

        private void RoR2Application_OnLoad()
        {
            magmaWormIndex = BodyCatalog.FindBodyIndex("MagmaWormBody");
            overloadingWormIndex = BodyCatalog.FindBodyIndex("ElectricWormBody");
        }
    }
}
