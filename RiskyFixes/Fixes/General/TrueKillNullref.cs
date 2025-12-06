using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RiskyFixes.Fixes.General
{
    public class TrueKillNullref : FixBase<TrueKillNullref>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "(Server-Side) TrueKill Nullref";

        public override string ConfigDescriptionString => "Fixes this nullref.";

        protected override void ApplyChanges()
        {
            IL.RoR2.CharacterMaster.TrueKill_GameObject_GameObject_DamageTypeCombo += CharacterMaster_TrueKill_GameObject_GameObject_DamageTypeCombo;
        }

        private void CharacterMaster_TrueKill_GameObject_GameObject_DamageTypeCombo(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            int bodyLoc = -1;
            if (c.TryGotoNext(x => x.MatchCall<CharacterMaster>("GetBody"), x => x.MatchStloc(out bodyLoc)))
            {
                if (c.TryGotoNext(x => x.MatchLdloc(bodyLoc), x => x.MatchCall<UnityEngine.Object>("op_Implicit"))
                    && c.TryGotoNext(MoveType.After, x => x.MatchLdloc(bodyLoc), x => x.MatchCall<UnityEngine.Object>("op_Implicit")))
                {
                    c.Index--;
                    c.EmitDelegate<Func<CharacterBody, CharacterBody>>(body =>
                    {
                        return (body && body.healthComponent) ? body : null;
                    });
                }
            }
        }
    }
}
