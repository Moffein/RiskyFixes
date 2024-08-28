using MonoMod.Cil;
using System;
using RoR2;
using UnityEngine;
using Mono.Cecil.Cil;

namespace RiskyFixes.Fixes.Enemies.Grandparent
{
    internal class GhostFriendlyFire : FixBase<GhostFriendlyFire>
    {
        public override string ConfigCategoryString => "Enemies - Grandparent";

        public override string ConfigOptionName => "(Server-Side) Disable Ghost Friendly Fire.";

        public override string ConfigDescriptionString => "Prevents ghosts from teamkilling you.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.RoR2.GrandParentSunController.ServerFixedUpdate += GrandParentSunController_ServerFixedUpdate;   //Actual damage logic, runs server-side
            IL.RoR2.GrandParentSunController.FixedUpdate += GrandParentSunController_FixedUpdate;   //Affects visuals, runs client-side
        }

        private void GrandParentSunController_FixedUpdate(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (c.TryGotoNext(MoveType.After,
                 x => x.MatchLdfld<HealthComponent>("body")
                ))
            {
                c.Emit(OpCodes.Ldarg_0);    //suncontroller
                c.EmitDelegate<Func<CharacterBody, GrandParentSunController, CharacterBody>>((victimBody, self) =>
                {
                    GameObject ownerObject = self.ownership.ownerObject; if (ownerObject)
                    {
                        TeamComponent tc = ownerObject.GetComponent<TeamComponent>();
                        if (tc && tc.teamIndex == TeamIndex.Player
                        && victimBody && victimBody.teamComponent && victimBody.teamComponent.teamIndex == TeamIndex.Player)
                        {
                            return null;
                        }
                    }
                    return victimBody;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: Grandparent GhostFriendlyFire FixedUpdate IL Hook failed");
            }
        }

        private void GrandParentSunController_ServerFixedUpdate(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
                 x => x.MatchLdfld<RoR2.HurtBox>("healthComponent")
                ))
            {
                c.Emit(OpCodes.Ldarg_0);    //suncontroller
                c.EmitDelegate<Func<HealthComponent, GrandParentSunController, HealthComponent>>((victimHealth, self) =>
                {
                    GameObject ownerObject = self.ownership.ownerObject;
                    if (ownerObject)
                    {
                        TeamComponent tc = ownerObject.GetComponent<TeamComponent>();
                        if (tc && tc.teamIndex == TeamIndex.Player
                        && victimHealth && victimHealth.body.teamComponent && victimHealth.body.teamComponent.teamIndex == TeamIndex.Player)
                        {
                            return null;
                        }
                    }
                    return victimHealth;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: Grandparent GhostFriendlyFire ServerFixedUpdate IL Hook failed");
            }
        }
    }
}
