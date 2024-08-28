using RoR2;
using RoR2.Artifacts;
using System;
using System.Collections.Generic;

namespace RiskyFixes.Fixes.Artifacts
{
    public class EnigmaBlacklistCheck : FixBase<EnigmaBlacklistCheck>
    {
        public override string ConfigCategoryString => "Artifacts";

        public override string ConfigOptionName => "(Server-Side) Enigma Blacklist Fix";

        public override string ConfigDescriptionString => "Fixes blacklisted equipment being able to spawn during Enigma runs.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            On.RoR2.Artifacts.EnigmaArtifactManager.OnRunStartGlobal += EnigmaArtifactManager_OnRunStartGlobal;
        }

        private void EnigmaArtifactManager_OnRunStartGlobal(On.RoR2.Artifacts.EnigmaArtifactManager.orig_OnRunStartGlobal orig, Run run)
        {
            orig(run);

            //EnigmaArtifactManager.validEquipment
            List<EquipmentIndex> toRemove = new List<EquipmentIndex>();
            foreach (EquipmentIndex ei in EnigmaArtifactManager.validEquipment)
            {
                if (!Run.instance.availableEquipment.Contains(ei))
                {
                    toRemove.Add(ei);
                }
            }

            foreach (EquipmentIndex ei in toRemove)
            {
                EnigmaArtifactManager.validEquipment.Remove(ei);
            }
        }
    }
}
