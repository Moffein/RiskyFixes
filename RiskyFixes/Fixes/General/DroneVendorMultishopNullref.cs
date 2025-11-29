using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RiskyFixes.Fixes.General
{
    public class DroneVendorMultishopNullref : FixBase<DroneVendorMultishopNullref>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "(Client-Side) DroneVendorMultiShopController.OnDestroy Nullref";

        public override string ConfigDescriptionString => "Fixes this nullref.";

        protected override void ApplyChanges()
        {
            On.RoR2.DroneVendorMultiShopController.OnDestroy += DroneVendorMultiShopController_OnDestroy;
        }

        private void DroneVendorMultiShopController_OnDestroy(On.RoR2.DroneVendorMultiShopController.orig_OnDestroy orig, RoR2.DroneVendorMultiShopController self)
        {
            if (self._terminals != null)
            {
                self._terminals = self._terminals.Where(t => t != null).ToArray();
            }
            orig(self);
        }
    }
}
