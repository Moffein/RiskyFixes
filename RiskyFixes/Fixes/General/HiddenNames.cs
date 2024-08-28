using RoR2;

namespace RiskyFixes.Fixes.General
{
    public class HiddenNames : FixBase<HiddenNames>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "(Client-Side) Hidden Names Fix";

        public override string ConfigDescriptionString => "Fixes people sometimes having hidden names by refreshing the list on stage start.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            RoR2.Stage.onStageStartGlobal += Stage_onStageStartGlobal;
        }

        private void Stage_onStageStartGlobal(RoR2.Stage obj)
        {
            foreach (NetworkUser netUser in NetworkUser.instancesList)
            {
                netUser.UpdateUserName();
            }
        }
    }
}
