using MonoMod.Cil;
using UnityEngine;
using RoR2;
using System;
using System.Linq;

namespace RiskyFixes.Fixes.Items
{
    public class DynamiteNullref : FixBase<DynamiteNullref>
    {
        public override string ConfigCategoryString => "Items";

        public override string ConfigOptionName => "(Client-Side) Box of Dynamite Nullref";

        public override string ConfigDescriptionString => "Fixes this nullref.";

        protected override void ApplyChanges()
        {
            IL.RoR2.Items.DroneDynamiteBehaviour.IsEnemyNearby += DroneDynamiteBehaviour_IsEnemyNearby;
        }

        private void DroneDynamiteBehaviour_IsEnemyNearby(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(x => x.MatchCall<UnityEngine.Physics>("CapsuleCastAll")) && c.TryGotoNext(MoveType.After, x => x.MatchCallvirt(typeof(UnityEngine.Component), "GetComponent")))
            {
                c.EmitDelegate<Func<HurtBox, HurtBox>>(orig =>
                {
                    if (orig != null && orig.healthComponent == null) return null;

                    return orig;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: Box of Dynamite Nullref IL hook failed.");
            }
        }
    }
}
