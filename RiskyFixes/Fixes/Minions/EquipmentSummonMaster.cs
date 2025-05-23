using MonoMod.Cil;
using RoR2;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace RiskyFixes.Fixes.Minions
{
    public class EquipmentSummonMaster : FixBase<EquipmentSummonMaster>
    {
        public override string ConfigCategoryString => "Minions";

        public override string ConfigOptionName => "(Server-Side) Equipment-Spawned Minions use Ambient Level";

        public override string ConfigDescriptionString => "Fixes Back-Up drones not scaling with Ambient Level.";

        protected override void ApplyChanges()
        {
            IL.RoR2.EquipmentSlot.SummonMaster += EquipmentSlot_SummonMaster;
        }

        private void EquipmentSlot_SummonMaster(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(x => x.MatchCallvirt<MasterSummon>("Perform")))
            {
                c.EmitDelegate<Func<MasterSummon, MasterSummon>>(summon =>
                {
                    summon.useAmbientLevel = true;
                    return summon;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: EquipmentSummonMaster IL hook failed.");
            }
        }
    }
}
