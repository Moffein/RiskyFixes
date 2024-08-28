using System;
using MonoMod.Cil;
using RoR2;
using UnityEngine;

namespace RiskyFixes.Fixes.Enemies
{
    public class DetectionFix : FixBase<DetectionFix>
    {
        public override string ConfigCategoryString => "Enemies";

        public override string ConfigOptionName => "(Server-Side) Close Range Detection";

        public override string ConfigDescriptionString => "Fixes enemy AI breaking due to negative detection range.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.RoR2.CharacterAI.BaseAI.GameObjectPassesSkillDriverFilters += BaseAI_GameObjectPassesSkillDriverFilters;
        }

        private void BaseAI_GameObjectPassesSkillDriverFilters(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
                x => x.MatchCallvirt<RoR2.CharacterAI.AISkillDriver>("get_minDistanceSqr")))
            {
                c.EmitDelegate<Func<float, float>>(orig =>
                {
                    if (orig <= 0f) orig = float.NegativeInfinity;
                    return orig;
                });
            }
        }
    }
}
