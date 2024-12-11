using MonoMod.Cil;
using UnityEngine;
using RoR2;
using System;

namespace RiskyFixes.Fixes.Items
{
    internal class LongstandingSolitudeNullref : FixBase<LongstandingSolitudeNullref>
    {
        public override string ConfigCategoryString => "Items";

        public override string ConfigOptionName => "(Client-Side) Longstanding Solitude Nullref Fix";

        public override string ConfigDescriptionString => "Fixes a nullref related to this item.";

        protected override void ApplyChanges()
        {
            On.RoR2.TeamManager.LongstandingSolitudesInParty += TeamManager_LongstandingSolitudesInParty;
        }

        private int TeamManager_LongstandingSolitudesInParty(On.RoR2.TeamManager.orig_LongstandingSolitudesInParty orig)
        {
            int num = 0;
            foreach (PlayerCharacterMasterController playerCharacterMasterController in PlayerCharacterMasterController.instances)
            {
                if (playerCharacterMasterController && playerCharacterMasterController.master)
                {
                    CharacterBody body = playerCharacterMasterController.master.GetBody();
                    if (body && body.inventory && body.inventory.GetItemCount(DLC2Content.Items.OnLevelUpFreeUnlock) > 0)
                    {
                        num += body.inventory.GetItemCount(DLC2Content.Items.OnLevelUpFreeUnlock);
                    }
                }
            }
            return num;
        }
    }
}
