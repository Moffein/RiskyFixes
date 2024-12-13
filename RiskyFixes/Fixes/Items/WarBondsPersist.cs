using RoR2;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RiskyFixes.Fixes.Items
{
    public class WarBondsPersist : FixBase<WarBondsPersist>
    {
        public override string ConfigCategoryString => "Items";

        public override string ConfigOptionName => "(Server-Side) War Bonds - Reset Between Stages";

        public override string ConfigDescriptionString => "Fixes War Bonds persisting between stages.";

        protected override void ApplyChanges()
        {
            Stage.onStageStartGlobal += Stage_onStageStartGlobal;
        }

        private void Stage_onStageStartGlobal(Stage obj)
        {
            foreach (var master in CharacterMaster.instancesList)
            {
                master.trackedMissileCount = 0;
            }
        }
    }
}
