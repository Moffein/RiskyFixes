using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using UnityEngine;

namespace RiskyFixes.Fixes.Enemies
{
    public class AffixBeadNullref : FixBase<AffixBeadNullref>
    {
        public override string ConfigCategoryString => "Enemies - Twisted Elites";

        public override string ConfigOptionName => "(Server-side) AffixBeadAttachment.Update nullref";

        public override string ConfigDescriptionString => "Fixes nullref spam from this elite.";

        protected override void ApplyChanges()
        {
            base.ApplyChanges();
            IL.RoR2.AffixBeadAttachment.Update += AffixBeadAttachment_Update;
        }

        private void AffixBeadAttachment_Update(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After, x => x.MatchCall<UnityEngine.Object>("op_Equality")))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<bool, AffixBeadAttachment, bool>>((isNull, self) =>
                {
                    return isNull || !self.modelObject;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: AffixBeadAttachment Nullref IL hook failed.");
            }
        }
    }
}
