using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.Networking;

namespace RiskyFixes.Fixes.Stages.Moon
{
    public class KillVoidTeam : FixBase<KillVoidTeam>
    {
        public override string ConfigCategoryString => "Stages - Commencement";

        public override string ConfigOptionName => "(Server-Side) Kill Void Team";

        public override string ConfigDescriptionString => "Kills Void team enemies when the bossfight starts.";

        protected override void ApplyChanges()
        {
            On.EntityStates.Missions.BrotherEncounter.BrotherEncounterBaseState.KillAllMonsters += BrotherEncounterBaseState_KillAllMonsters;
        }

        private void BrotherEncounterBaseState_KillAllMonsters(On.EntityStates.Missions.BrotherEncounter.BrotherEncounterBaseState.orig_KillAllMonsters orig, EntityStates.Missions.BrotherEncounter.BrotherEncounterBaseState self)
        {
            orig(self);

            if (NetworkServer.active)
            {
                foreach (TeamComponent teamComponent in new List<TeamComponent>(TeamComponent.GetTeamMembers(TeamIndex.Void)))
                {
                    if (teamComponent)
                    {
                        HealthComponent component = teamComponent.GetComponent<HealthComponent>();
                        if (component)
                        {
                            component.Suicide(null, null, DamageType.Generic);
                        }
                    }
                }
            }
        }
    }
}
