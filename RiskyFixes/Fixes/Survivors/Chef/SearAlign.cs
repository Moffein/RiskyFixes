using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RiskyFixes.Fixes.Survivors.Chef
{
    public class SearAlign : FixBase<SearAlign>
    {
        public override string ConfigCategoryString => "Survivors - CHEF";

        public override string ConfigOptionName => "(Client-Side) Sear - Force Align";

        public override string ConfigDescriptionString => "Makes CHEF always point in your aim direction when using Sear.";

        protected override void ApplyChanges()
        {
            On.EntityStates.Chef.Sear.Update += Sear_Update;
        }

        private void Sear_Update(On.EntityStates.Chef.Sear.orig_Update orig, EntityStates.Chef.Sear self)
        {
            Ray aimRay = self.GetAimRay();
            Vector3 forwardDirection = aimRay.direction;
            forwardDirection.y = 0f;
            forwardDirection.Normalize();

            if (self.characterDirection) self.characterDirection.forward = forwardDirection;

            orig(self);
        }
    }
}
