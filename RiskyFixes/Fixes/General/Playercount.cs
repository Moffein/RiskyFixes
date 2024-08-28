using MonoMod.RuntimeDetour;
using R2API.Utils;
using System;
using RoR2;

namespace RiskyFixes.Fixes.General
{
    public class Playercount : FixBase<Playercount>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "(Server-Side) Playercount Fix";

        public override string ConfigDescriptionString => "Fixes disconnected players counting towards the playercount.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            //Based on https://github.com/wildbook/R2Mods/blob/master/Multitudes/Multitudes.cs
            var getParticipatingPlayerCount = new Hook(typeof(Run).GetMethodCached("get_participatingPlayerCount"),
                typeof(Playercount).GetMethodCached(nameof(GetParticipatingPlayerCountHook)));
        }
        private static int GetParticipatingPlayerCountHook(Run self)
        {
            return GetConnectedPlayers();
        }

        public static int GetConnectedPlayers()
        {
            int players = 0;
            foreach (PlayerCharacterMasterController pc in PlayerCharacterMasterController.instances)
            {
                if (pc.isConnected)
                {
                    players++;
                }
            }

            players *= ModCompat.MultitudesCompat.GetMultiplier();
            players *= ModCompat.ZetArtifactsCompat.Multifact.GetMultiplier();

            return players;
        }
    }
}
