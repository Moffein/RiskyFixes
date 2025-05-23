using System;
using System.Collections.Generic;
using System.Text;

namespace RiskyFixes.Fixes.Survivors.Treebot
{
    public class HarvestAnim : FixBase<HarvestAnim>
    {
        public override string ConfigCategoryString => "Survivors - REX";

        public override string ConfigOptionName => "(Client-Side) DIRECTIVE: Harvest Animation";

        public override string ConfigDescriptionString => "Restores the missing prep animation for DIRECTIVE: Harvest.";

        protected override void ApplyChanges()
        {
            SneedUtils.SetAddressableEntityStateField("RoR2/Base/Treebot/EntityStates.Treebot.TreebotPrepFruitSeed.asset", "baseDuration", "0.1");
        }
    }
}
