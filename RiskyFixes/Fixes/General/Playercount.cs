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

        private static int maxPlayersOnStage = 0;

        protected override void ApplyChanges()
        {
            On.RoR2.Stage.Start += Stage_Start;

            //Based on https://github.com/wildbook/R2Mods/blob/master/Multitudes/Multitudes.cs
            var getParticipatingPlayerCount = new Hook(typeof(Run).GetMethodCached("get_participatingPlayerCount"),
                typeof(Playercount).GetMethodCached(nameof(GetParticipatingPlayerCountHook)));
        }

        private System.Collections.IEnumerator Stage_Start(On.RoR2.Stage.orig_Start orig, Stage self)
        {
            maxPlayersOnStage = 0;
            return orig(self);
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

            if (players > maxPlayersOnStage)
            {
                maxPlayersOnStage = players;
            }
            else
            {
                players = maxPlayersOnStage;
            }

            players *= ModCompat.MultitudesCompat.GetMultiplier();
            players *= ModCompat.ZetArtifactsCompat.Multifact.GetMultiplier();

            if (players != 0)
            {
                players += ModCompat.MultitudesDifficultyCompat.GetAdditional();
            }

            return players;
        }
    }
}
