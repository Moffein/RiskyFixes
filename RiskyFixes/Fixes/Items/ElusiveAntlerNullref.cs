using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace RiskyFixes.Fixes.Items
{
    public class ElusiveAntlerNullref : FixBase<ElusiveAntlerNullref>
    {
        public override string ConfigCategoryString => "Items";

        public override string ConfigOptionName => "(Server-Side) Elusive Antler - Fix Nullref";

        public override string ConfigDescriptionString => "Fixes nullref on orb despawn.";

        protected override void ApplyChanges()
        {
            On.RoR2.ElusiveAntlersPickup.OnShardDestroyed += ElusiveAntlersPickup_OnShardDestroyed;
        }

        private void ElusiveAntlersPickup_OnShardDestroyed(On.RoR2.ElusiveAntlersPickup.orig_OnShardDestroyed orig, RoR2.ElusiveAntlersPickup self, RoR2.ElusiveAntlersPickup.ElusiveAntlersDestroyCondition destroyCondition)
        {
            if (!NetworkServer.active)
            {
                Debug.LogWarning("[Server] function 'System.Void RoR2.ElusiveAntlersPickup::OnShardDestroyed(RoR2.ElusiveAntlersPickup/ElusiveAntlersDestroyCondition)' called on client");
                return;
            }
            if (self.ownerBody != null)
            {
                self.ownerBody.OnShardDestroyed();
                if (destroyCondition != ElusiveAntlersPickup.ElusiveAntlersDestroyCondition.Despawn)
                {
                    ElusiveAntlersBehavior elusiveAntlersBehavior = self.ownerBody.GetComponent<ElusiveAntlersBehavior>();
                    if (elusiveAntlersBehavior) elusiveAntlersBehavior.ShardDestroyed(destroyCondition);
                }
            }
        }
    }
}
