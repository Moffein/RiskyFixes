using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RiskyFixes.Fixes.Survivors.Treebot
{
    public class UtilityCrash : FixBase<UtilityCrash>
    {
        public override string ConfigCategoryString => "Survivors - REX";

        public override string ConfigOptionName => "Utility Crash Fix";

        public override string ConfigDescriptionString => "(Server-Side) Fixes error spam and crashes related to the Utility skill.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.EntityStates.Treebot.Weapon.FireSonicBoom.OnEnter += FireSonicBoom_OnEnter;
        }

        //I'm not sure what the exact cause of the error is, so I'm targeting multiple candidates that seem likely
        private void FireSonicBoom_OnEnter(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);

            bool error = true;

            //Fix potential divide by zero
            if (c.TryGotoNext(MoveType.After, x=> x.MatchCall(typeof(UnityEngine.Vector3), "get_magnitude")))
            {
                c.EmitDelegate<Func<float, float>>(magnitude => (magnitude <= 0f) ? 1f : magnitude);

                //Fix zero mass
                if (c.TryGotoNext(MoveType.After, x => x.MatchLdfld(typeof(CharacterMotor), "mass")))
                {
                    c.EmitDelegate<Func<float, float>>(mass => (mass <= 0f) ? 1f : mass);

                    if (c.TryGotoNext(MoveType.After, x => x.MatchCallvirt(typeof(Rigidbody), "get_mass")))
                    {
                        c.EmitDelegate<Func<float, float>>(mass => (mass <= 0f) ? 1f : mass);

                        //Fix zero acceleration. Is this actually a problem?
                        if (c.TryGotoNext(MoveType.After, x=> x.MatchCallvirt(typeof(CharacterBody), "get_acceleration")))
                        {
                            c.EmitDelegate<Func<float, float>>(accel => (accel <= 0f) ? 1f : accel);
                            error = false;
                        }
                    }
                }
            }

            if (error)
            {
                Debug.LogError("RiskyFixes: Treebot UtilityCrash IL Hook failed.");
            }
        }
    }
}
