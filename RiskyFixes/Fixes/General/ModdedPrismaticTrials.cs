using System;
using System.Collections.Generic;
using System.Text;

namespace RiskyFixes.Fixes.General
{
    public class ModdedPrismaticTrials : FixBase<ModdedPrismaticTrials>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "Prismatic Trials";

        public override string ConfigDescriptionString => "Enables Prismatic Trials while modded. Runs will not count towards the Leaderboard.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            //Disable leaderboard.
            On.RoR2.WeeklyRun.ClientSubmitLeaderboardScore += (orig, self, runReport) =>
            {
                return;
            };

            //Prevent button from being hidden.
            On.RoR2.DisableIfGameModded.OnEnable += (orig, self) =>
            {
                return;
            };
        }
    }
}
