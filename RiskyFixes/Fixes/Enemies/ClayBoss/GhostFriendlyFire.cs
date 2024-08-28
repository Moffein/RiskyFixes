using MonoMod.Cil;
using System;
using RoR2;
using Mono.Cecil.Cil;
using UnityEngine;

namespace RiskyFixes.Fixes.Enemies.ClayBoss
{
    public class GhostFriendlyFire : FixBase<GhostFriendlyFire>
    {
        public override string ConfigCategoryString => "Enemies - Clay Dunestrider";

        public override string ConfigOptionName => "(Server-Side) Disable Ghost Friendly Fire";

        public override string ConfigDescriptionString => "Prevents ghosts from teamkilling you.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.EntityStates.ClayBoss.Recover.FireTethers += Recover_FireTethers;
        }

        private void Recover_FireTethers(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(
                 x => x.MatchCallvirt<BullseyeSearch>("RefreshCandidates")
                ))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<BullseyeSearch, EntityStates.ClayBoss.Recover, BullseyeSearch>>((search, self) =>
                {
                    if (self.GetTeam() == TeamIndex.Player)
                    {
                        search.teamMaskFilter.RemoveTeam(TeamIndex.Player);
                    }
                    return search;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: ClayBoss Recover_FireTethers IL Hook failed");
            }
        }
    }
}
