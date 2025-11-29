using RoR2;

namespace RiskyFixes.Fixes.Enemies.ArtifactReliquary
{
    public class PreventArtifactHeal : FixBase<PreventArtifactHeal>
    {
        public override string ConfigCategoryString => "Enemies - Artifact Reliquary";

        public override string ConfigOptionName => "(Server-Side) Prevent Heal";

        public override string ConfigDescriptionString => "Prevents Artifact Reliquary from healing and softlocking runs.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            On.RoR2.HealthComponent.Heal += HealthComponent_Heal;
        }

        private float HealthComponent_Heal(On.RoR2.HealthComponent.orig_Heal orig, RoR2.HealthComponent self, float amount, RoR2.ProcChainMask procChainMask, bool nonRegen)
        {
            if (self.body && self.body.bodyIndex == RoR2Content.BodyPrefabs.ArtifactShellBody.bodyIndex)
            {
                return 0f;
            }

            return orig(self, amount, procChainMask, nonRegen);
        }
    }
}
