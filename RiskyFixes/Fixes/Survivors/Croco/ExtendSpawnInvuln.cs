using RoR2;
using UnityEngine.Networking;

namespace RiskyFixes.Fixes.Survivors.Croco
{
    public class ExtendSpawnInvuln : FixBase<ExtendSpawnInvuln>
    {
        public override string ConfigCategoryString => "Survivors - Acrid";

        public override string ConfigOptionName => "(Server-Side) Spawn Protection";

        public override string ConfigDescriptionString => "Spawn protection lasts until after waking up.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            On.EntityStates.Croco.Spawn.OnEnter += (orig, self) =>
            {
                orig(self);
                if (NetworkServer.active && self.characterBody)
                {
                    self.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
                }
            };

            On.EntityStates.Croco.Spawn.OnExit += (orig, self) =>
            {
                if (NetworkServer.active && self.characterBody && self.characterBody.HasBuff(RoR2Content.Buffs.HiddenInvincibility))
                {
                    self.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
                }
                orig(self);
            };

            On.EntityStates.Croco.WakeUp.OnEnter += (orig, self) =>
            {
                orig(self);
                if (NetworkServer.active && self.characterBody)
                {
                    self.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
                }
            };

            On.EntityStates.Croco.WakeUp.OnExit += (orig, self) =>
            {
                if (NetworkServer.active && self.characterBody && self.characterBody.HasBuff(RoR2Content.Buffs.HiddenInvincibility))
                {
                    self.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
                }
                orig(self);
            };
        }
    }
}
