using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace RiskyFixes.Fixes.Survivors.Huntress
{
    public class FixFlurry : FixBase<FixFlurry>
    {
        public override string ConfigCategoryString => "Survivors - Huntress";

        public override string ConfigOptionName => "(Server-Side) Flurry Attack Speed Fix";
        public override string ConfigDescriptionString => "Fixes Flurry losing arrows at high attack speeds.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            On.EntityStates.Huntress.HuntressWeapon.FireSeekingArrow.OnExit += FireSeekingArrow_OnExit;
        }

        private void FireSeekingArrow_OnExit(On.EntityStates.Huntress.HuntressWeapon.FireSeekingArrow.orig_OnExit orig, EntityStates.Huntress.HuntressWeapon.FireSeekingArrow self)
        {
            orig(self);
            if (NetworkServer.active)
            {
                int remainingArrows = self.maxArrowCount - self.firedArrowCount;
                for (int i = 0; i < remainingArrows; i++)
                {
                    self.arrowReloadTimer = 0f;
                    self.FireOrbArrow();
                }
            }
        }
    }
}
