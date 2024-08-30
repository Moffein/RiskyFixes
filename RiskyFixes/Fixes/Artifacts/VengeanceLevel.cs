using RoR2;
using UnityEngine;
using UnityEngine.Networking;


namespace RiskyFixes.Fixes.Artifacts
{
    public class VengeanceLevel : FixBase<VengeanceLevel>
    {
        public override string ConfigCategoryString => "Artifacts";

        public override string ConfigOptionName => "(Server-Side) Vengeance Umbra Level";

        public override string ConfigDescriptionString => "Vengeance Umbras copy player levels.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            if (ModCompat.AIBlacklist.pluginLoaded)
            {
                Debug.LogWarning("RiskyFixes: Disabling VengeanceLevel due to AI Blacklist being loaded.");
                return;
            }
            RoR2.CharacterMaster.onStartGlobal += RunVengeanceChanges;
        }

        private void RunVengeanceChanges(CharacterMaster self)
        {
            if (NetworkServer.active && self.inventory && self.inventory.GetItemCount(RoR2Content.Items.InvadingDoppelganger) > 0)
            {
                int alCount = self.inventory.GetItemCount(RoR2Content.Items.UseAmbientLevel);
                if (alCount > 0) self.inventory.RemoveItem(RoR2Content.Items.UseAmbientLevel, alCount);

                int lbCount = self.inventory.GetItemCount(RoR2Content.Items.LevelBonus);
                if (lbCount > 0) self.inventory.RemoveItem(RoR2Content.Items.LevelBonus, lbCount);

                self.inventory.GiveItem(RoR2Content.Items.LevelBonus, (int)TeamManager.instance.GetTeamLevel(TeamIndex.Player) - 1);
            }
        }
    }
}
