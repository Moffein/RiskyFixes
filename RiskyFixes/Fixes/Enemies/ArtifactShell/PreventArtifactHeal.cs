using RoR2;

namespace RiskyFixes.Fixes.Enemies.ArtifactReliquary
{
    public class PreventArtifactHeal : FixBase<PreventArtifactHeal>
    {
        public override string ConfigCategoryString => "Enemies - Artifact Reliquary";

        public override string ConfigOptionName => "(Server-Side) Prevent Heal";

        public override string ConfigDescriptionString => "Prevents Artifact Reliquary from healing and softlocking runs.";

        public override bool StopLoadOnConfigDisable => true;

        private BodyIndex bodyIndex;

        protected override void ApplyChanges()
        {
            RoR2Application.onLoad += OnLoad;
            On.RoR2.HealthComponent.Heal += HealthComponent_Heal;
        }

        private void OnLoad()
        {
            bodyIndex = BodyCatalog.FindBodyIndex("ArtifactShellBody");
        }

        private float HealthComponent_Heal(On.RoR2.HealthComponent.orig_Heal orig, RoR2.HealthComponent self, float amount, RoR2.ProcChainMask procChainMask, bool nonRegen)
        {
            if (self.body && self.body.bodyIndex == bodyIndex)
            {
                return 0f;
            }

            return orig(self, amount, procChainMask, nonRegen);
        }
    }
}
