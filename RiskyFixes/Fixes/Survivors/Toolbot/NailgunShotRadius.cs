using MonoMod.Cil;
using System;
using RoR2;

namespace RiskyFixes.Fixes.Survivors.Toolbot
{
    public class NailgunShotRadius : FixBase<NailgunShotRadius>
    {
        public override string ConfigCategoryString => "Survivors - MUL-T";

        public override string ConfigOptionName => "(Client-Side) Nailgun Shot Radius";

        public override string ConfigDescriptionString => "Gives Nailgun shots a small radius like most other bullet attacks in the game.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.EntityStates.Toolbot.BaseNailgunState.FireBullet += BaseNailgunState_FireBullet;
        }

        private void BaseNailgunState_FireBullet(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(
                 x => x.MatchCallvirt<BulletAttack>("Fire")
                 ))
            {
                c.EmitDelegate<Func<BulletAttack, BulletAttack>>(bulletAttack =>
                {
                    bulletAttack.radius = 0.2f;
                    bulletAttack.smartCollision = true;
                    return bulletAttack;
                });
            }
            else
            {
                UnityEngine.Debug.LogError("RiskyFixes: Toolbot NailgunShotRadius IL Hook failed");
            }
        }
    }
}
