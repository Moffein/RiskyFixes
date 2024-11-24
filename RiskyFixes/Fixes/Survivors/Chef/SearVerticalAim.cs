using Mono.Cecil.Cil;
using MonoMod.Cil;
using RoR2;
using System;
using UnityEngine;

namespace RiskyFixes.Fixes.Survivors.Chef
{
    public class SearVerticalAim : FixBase<SearVerticalAim>
    {
        public override string ConfigCategoryString => "Survivors - CHEF";

        public override string ConfigOptionName => "(Client-Side) Sear - Vertical Aiming";

        public override string ConfigDescriptionString => "Allow Sear to be aimed vertically and fixes aim being tied to your model direction.";

        protected override void ApplyChanges()
        {
            IL.EntityStates.Chef.Sear.FirePrimaryAttack += Sear_FirePrimaryAttack;
        }

        private void Sear_FirePrimaryAttack(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(x => x.MatchCallvirt(typeof(RoR2.BulletAttack), "Fire"))){
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<BulletAttack, EntityStates.Chef.Sear, BulletAttack>>((bulletAttack, self) =>
                {
                    Ray aimRay = self.GetAimRay();
                    bulletAttack.aimVector = aimRay.direction;
                    return bulletAttack;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: SearVerticalAim IL hook failed.");
            }
        }
    }
}
