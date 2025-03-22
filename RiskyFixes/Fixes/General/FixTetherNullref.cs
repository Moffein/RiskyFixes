using System;
using System.Collections.Generic;
using System.Text;

namespace RiskyFixes.Fixes.General
{
    public class FixTetherNullref : FixBase<FixTetherNullref>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "(Client-Side) TetherVFX Nullref Fix";

        public override string ConfigDescriptionString => "Fixes TetherVFX nullref spam.";

        protected override void ApplyChanges()
        {
            On.RoR2.TetherVfxOrigin.RemoveTetherAt += TetherVfxOrigin_RemoveTetherAt;
        }

        private void TetherVfxOrigin_RemoveTetherAt(On.RoR2.TetherVfxOrigin.orig_RemoveTetherAt orig, RoR2.TetherVfxOrigin self, int i)
        {
            if (i >= self.tetheredTransforms.Count || i >= self.tetherVfxs.Count) return;
            orig(self, i);
        }
    }
}
