using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiskyFixes.Fixes.Survivors.Commando
{
    public class PrimaryFireRate : FixBase<PrimaryFireRate>
    {
        public override string ConfigCategoryString => "Survivors - Commando";

        public override string ConfigOptionName => "(Client-Side) Double Tap Fire Rate";

        public override string ConfigDescriptionString => "Fixes Double Tap having a hidden reload state that lowers its fire rate at high attack speeds.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.EntityStates.Commando.CommandoWeapon.FirePistol2.FixedUpdate += FirePistol2_FixedUpdate;
        }

        //Removes the reload state completely since this messes with attack speed lategame.
        private void FirePistol2_FixedUpdate(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(
                 x => x.MatchLdcI4(0)
                ))
            {
                c.Index++;
                c.EmitDelegate<Func<int, int>>(zero =>
                {
                    return -1000000000;
                });
            }
            else
            {
                UnityEngine.Debug.LogError("RiskyFixes: Commando PrimaryFireRate IL Hook failed");
            }
        }
    }
}
