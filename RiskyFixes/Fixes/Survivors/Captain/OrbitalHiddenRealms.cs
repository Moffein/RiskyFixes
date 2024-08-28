using R2API.Utils;
using MonoMod.RuntimeDetour;
using RoR2.Skills;

namespace RiskyFixes.Fixes.Survivors.Captain
{
    internal class OrbitalHiddenRealms : FixBase<OrbitalHiddenRealms>
    {
        public override string ConfigCategoryString => "Survivors - Captain";

        public override string ConfigOptionName => "(Client-Side) Orbital Skills in Hidden Realms";

        public override string ConfigDescriptionString => "Allows Orbital Skills to be used in Hidden Realms.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            var getIsAvailable = new Hook(typeof(CaptainOrbitalSkillDef).GetMethodCached("get_isAvailable"),
                typeof(OrbitalHiddenRealms).GetMethodCached(nameof(IsAvailable)));
        }

        private static bool IsAvailable(CaptainOrbitalSkillDef self)
        {
            return true;
        }
    }
}
