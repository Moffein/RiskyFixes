using MonoMod.Cil;
using System;
using RoR2;
using UnityEngine;
using Mono.Cecil.Cil;
using System.Linq;

namespace RiskyFixes.Fixes.Enemies.LynxTotem
{
    internal class GhostFriendlyFire : FixBase<GhostFriendlyFire>
    {
        public override string ConfigCategoryString => "Enemies - LynxTotem";

        public override string ConfigOptionName => "(Server-Side) Disable Ghost Friendly Fire.";

        public override string ConfigDescriptionString => "Prevents ghosts from teamkilling you.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            On.EntityStates.Totem.SacrificeSearch.FindAlliesToRaptureUnderAuthority += SacrificeSearch_FindAlliesToRaptureUnderAuthority;
        }

        private void SacrificeSearch_FindAlliesToRaptureUnderAuthority(On.EntityStates.Totem.SacrificeSearch.orig_FindAlliesToRaptureUnderAuthority orig, EntityStates.Totem.SacrificeSearch self, System.Collections.Generic.List<GameObject> alliesToRaptureAuthority)
        {
            if (self.GetTeam() == TeamIndex.Player)
            {
                //Just don't even let this thing grief.
                //Could filter out Players and Drones, but then Chirr still gets screwed over.
                return;
            }

            orig(self, alliesToRaptureAuthority);
        }
    }
}
