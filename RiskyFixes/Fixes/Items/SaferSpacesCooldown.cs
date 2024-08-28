using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RiskyFixes.Fixes.Items
{
    public class SaferSpacesCooldown : FixBase<SaferSpacesCooldown>
    {
        public override string ConfigCategoryString => "Items";

        public override string ConfigOptionName => "(Server-Side) Safer Spaces Cooldown";

        public override string ConfigDescriptionString => "Fixes Safer Spaces starting with +1 stacks.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.RoR2.HealthComponent.TakeDamageProcess += HealthComponent_TakeDamageProcess;
        }

        private void HealthComponent_TakeDamageProcess(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After, x => x.MatchLdsfld(typeof(DLC1Content.Items), "BearVoid")))
            {
                c.Index++;
                c.EmitDelegate<Func<int, int>>(itemCount => itemCount - 1);
            }
            else
            {
                Debug.LogError("RiskyFixes: SaferSpacesCoodlown IL Hook failed.");
            }
        }
    }
}
