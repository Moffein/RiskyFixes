using UnityEngine;
using RoR2;

namespace RiskyFixes.Fixes.Survivors.Bandit2
{
    public class PrimaryShotRadius : FixBase<PrimaryShotRadius>
    {
        public override string ConfigCategoryString => "Survivors - Bandit";

        public override string ConfigOptionName => "(Client-Side) Primary Shot Radius";

        public override string ConfigDescriptionString => "Gives Bandit primary bullets a small radius like most other bullet attacks in the game.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            SneedUtils.SetAddressableEntityStateField("RoR2/Base/Bandit2/EntityStates.Bandit2.Weapon.Bandit2FireRifle.asset", "radius", "0.3");
            SneedUtils.SetAddressableEntityStateField("RoR2/Base/Bandit2/EntityStates.Bandit2.Weapon.FireShotgun2.asset", "radius", "0.5");
        }
    }
}
