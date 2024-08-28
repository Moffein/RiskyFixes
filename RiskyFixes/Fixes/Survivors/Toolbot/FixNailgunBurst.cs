using EntityStates.Toolbot;
using RoR2;
using UnityEngine;

namespace RiskyFixes.Fixes.Survivors.Toolbot
{
    public class FixNailgunBurst : FixBase<FixNailgunBurst>
    {
        public override string ConfigCategoryString => "Survivors - MUL-T";

        public override string ConfigOptionName => "(Client-Side) Nailgun Burst Always Triggers";

        public override string ConfigDescriptionString => "Nailgun ending shotgun burst always triggers when ending the skill.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            On.EntityStates.Toolbot.FireNailgun.OnExit += FireNailgun_OnExit;
        }

        //The reason this is broken is because the shotgun blast is its own entitystate, which gets cancelled by any other entitystate.
        private void FireNailgun_OnExit(On.EntityStates.Toolbot.FireNailgun.orig_OnExit orig, FireNailgun self)
        {
            orig(self);
            //Manually fire shotgun burst if skill is cancelled.
            //self.outer.nextState doesn't seem to be working
            if (self.IsKeyDownAuthority() && !(self.characterBody && self.characterBody.isSprinting))
            {
                if (self.characterBody)
                {
                    self.characterBody.SetSpreadBloom(1f, false);
                }
                Ray aimRay = self.GetAimRay();
                self.FireBullet(self.GetAimRay(), NailgunFinalBurst.finalBurstBulletCount, BaseNailgunState.spreadPitchScale, BaseNailgunState.spreadYawScale);

                Util.PlaySound(NailgunFinalBurst.burstSound, self.gameObject);
                if (self.isAuthority)
                {
                    float num = NailgunFinalBurst.selfForce * (self.characterMotor.isGrounded ? 0.5f : 1f) * self.characterMotor.mass;
                    self.characterMotor.ApplyForce(aimRay.direction * -num, false, false);
                }
                Util.PlaySound(BaseNailgunState.fireSoundString, self.gameObject);
                Util.PlaySound(BaseNailgunState.fireSoundString, self.gameObject);
                Util.PlaySound(BaseNailgunState.fireSoundString, self.gameObject);
            }
        }
    }
}
