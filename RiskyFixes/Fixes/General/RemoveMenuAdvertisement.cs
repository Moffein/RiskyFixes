using UnityEngine;
using RoR2.UI;

namespace RiskyFixes.Fixes.General
{
    public class RemoveMenuAdvertisement : FixBase<RemoveMenuAdvertisement>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "(Client-Side) Remove Main Menu Advertisement";

        public override string ConfigDescriptionString => "Removes the DLC advertisement from the main menu.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            On.RoR2.UI.MPButton.Awake += MPButton_Awake;
        }

        private void MPButton_Awake(On.RoR2.UI.MPButton.orig_Awake orig, MPButton self)
        {
            if (self.name == "PlatformStoreButton")
            {
                UnityEngine.Object.Destroy(self.gameObject);
                return;
            }
            orig(self);
        }
    }
}
