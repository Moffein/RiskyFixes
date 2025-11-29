using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using UnityEngine;

namespace RiskyFixes.Fixes.Enemies.DefectiveUnit
{
    public class DefectiveDeathNullref : FixBase<DefectiveDeathNullref>
    {
        public override string ConfigCategoryString => "Enemies - Solus Invalidator";

        public override string ConfigOptionName => "(Client-Side) Death Nullref Fix";

        public override string ConfigDescriptionString => "Fixes this nullref";

        protected override void ApplyChanges()
        {
            IL.EntityStates.DefectiveUnit.DeathState.AttemptDeathBehavior += DeathState_AttemptDeathBehavior;
        }

        private void DeathState_AttemptDeathBehavior(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After, x => x.MatchLdsfld<EntityStates.DefectiveUnit.DeathState>("deathEffect")))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<GameObject, EntityStates.DefectiveUnit.DeathState, GameObject>>((effect, self) =>
                {
                    return self.cachedModelTransform ? effect : null;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: DefectiveDeathNullref IL hook failed.");
            }
        }
    }
}
