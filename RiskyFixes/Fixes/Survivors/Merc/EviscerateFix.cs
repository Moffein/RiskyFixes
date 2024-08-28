using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace RiskyFixes.Fixes.Survivors.Merc
{
    public class EviscerateFix : FixBase<EviscerateFix>
    {
        public override string ConfigCategoryString => "Survivors - Mercenary";

        public override string ConfigOptionName => "(Client-Side) Eviscerate Targeting";

        public override string ConfigDescriptionString => "Fixes Eviscerate from getting caught on allies.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.EntityStates.Merc.Evis.FixedUpdate += Evis_FixedUpdate;
        }

        //The goal is to get Eviscerate to ignore allies. The way this is achieved is hacky here.
        private void Evis_FixedUpdate(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
             x => x.MatchLdfld(typeof(RoR2.HurtBox), "healthComponent")
            ))
            {
                c.Emit(OpCodes.Ldarg_0);//self
                c.EmitDelegate<Func<HealthComponent, EntityStates.Merc.EvisDash, HealthComponent>>((healthComponent, self) =>
                {
                    if (FriendlyFireManager.friendlyFireMode == FriendlyFireManager.FriendlyFireMode.Off && healthComponent != self.healthComponent)
                    {
                        if (healthComponent && healthComponent.body && healthComponent.body.teamComponent)
                        {
                            //trick it into thinking it's itself
                            if (!FriendlyFireManager.ShouldDirectHitProceed(healthComponent, self.GetTeam())) return self.healthComponent;
                        }
                    }
                    return healthComponent;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: Merc EviscerateFix EvisDash IL Hook failed");
            }
        }
    }
}
