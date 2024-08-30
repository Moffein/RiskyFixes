using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RiskyFixes.Fixes.Items
{
    public class VoidsentNewtProc : FixBase<VoidsentNewtProc>
    {
        public override string ConfigCategoryString => "Items";

        public override string ConfigOptionName => "(Server-Side) Voidsent Flame disabled in Bazaar";

        public override string ConfigDescriptionString => "Prevents Voidsent Flame from proccing against the Newt due to the risk of crashing.";

        public override bool StopLoadOnConfigDisable => true;

        private BodyIndex newtIndex;

        protected override void ApplyChanges()
        {
            RoR2Application.onLoad += OnLoad;
            IL.RoR2.HealthComponent.TakeDamageProcess += HealthComponent_TakeDamageProcess;
        }

        private void HealthComponent_TakeDamageProcess(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After, x => x.MatchLdsfld(typeof(DLC1Content.Items), "ExplodeOnDeathVoid")))
            {
                c.Emit(OpCodes.Ldarg_0);//healthcomponent
                c.EmitDelegate<Func<ItemDef, HealthComponent, ItemDef>>((item, self) =>
                {
                    if (self.body.bodyIndex == newtIndex)
                    {
                        return null;
                    }
                    return item;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: VoidsentNewtProc IL Hook Failed.");
            }
        }

        private void OnLoad()
        {
            newtIndex = BodyCatalog.FindBodyIndex("ShopkeeperBody");
        }
    }
}
