using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;

namespace RiskyFixes.Fixes.Survivors.Railgunner
{
    public class FixBungus : FixBase<FixBungus>
    {
        public override string ConfigCategoryString => "Survivors - Railgunner";
        
        public override string ConfigOptionName => "(Client-Side) Fix Bungus";

        public override string ConfigDescriptionString => "Disables self-knockback while standing still.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.EntityStates.Railgunner.Weapon.BaseFireSnipe.OnFireBulletAuthority += BaseFireSnipe_OnFireBulletAuthority;
            IL.EntityStates.Railgunner.Weapon.FirePistol.FireBullet += FirePistol_FireBullet;
        }

        private void FirePistol_FireBullet(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
                 x => x.MatchLdfld(typeof(EntityStates.Railgunner.Weapon.FirePistol), "selfKnockbackForce")
                ))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<float, EntityStates.Railgunner.Weapon.FirePistol, float>>((force, self) =>
                {
                    if (self.characterMotor && self.characterMotor.isGrounded)
                    {
                        return 0;
                    }
                    return force;
                });
            }
            else
            {
                UnityEngine.Debug.LogError("RiskyFixes: Railgunner FixBungus FirePistol IL Hook failed");
            }
        }

        private void BaseFireSnipe_OnFireBulletAuthority(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
                 x => x.MatchLdfld(typeof(EntityStates.Railgunner.Weapon.BaseFireSnipe), "selfKnockbackForce")
                ))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<float, EntityStates.Railgunner.Weapon.BaseFireSnipe, float>>((force, self) =>
                {
                    if (self.characterMotor && self.characterMotor.velocity == UnityEngine.Vector3.zero)
                    {
                        return 0;
                    }
                    return force;
                });
            }
            else
            {
                UnityEngine.Debug.LogError("RiskyFixes: Railgunner FixBungus Snipe IL Hook failed");
            }
        }
    }
}
