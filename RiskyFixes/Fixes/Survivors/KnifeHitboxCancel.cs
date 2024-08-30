using Mono.Cecil.Cil;
using MonoMod.Cil;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace RiskyFixes.Fixes.Survivors
{
    public class KnifeHitboxCancel : FixBase<KnifeHitboxCancel>
    {
        public override string ConfigCategoryString => "Survivors - Bandit";

        public override string ConfigOptionName => "(Client-Side) Knife Hitbox Cancel Fix";

        public override string ConfigDescriptionString => "Knife hitbox no longer gets cancelled by other animations.";

        protected override void ApplyChanges()
        {
            IL.EntityStates.Bandit2.ThrowSmokebomb.OnEnter += ThrowSmokebomb_OnEnter;
            IL.EntityStates.Bandit2.Weapon.Bandit2FirePrimaryBase.OnEnter += Bandit2FirePrimaryBase_OnEnter;
            IL.EntityStates.Bandit2.Weapon.Reload.OnEnter += Reload_OnEnter;
            IL.EntityStates.Bandit2.Weapon.EnterReload.OnEnter += EnterReload_OnEnter;
        }

        private void ThrowSmokebomb_OnEnter(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
                x => x.MatchLdstr("Gesture, Additive")))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<string, EntityStates.Bandit2.ThrowSmokebomb, string>>((animLayer, self) =>
                {
                    Animator modelAnimator = self.GetModelAnimator();
                    if (modelAnimator)
                    {
                        int layerIndex = modelAnimator.GetLayerIndex("Gesture, Additive");
                        if (layerIndex >= 0)
                        {
                            AnimatorStateInfo animStateInfo = modelAnimator.GetCurrentAnimatorStateInfo(layerIndex);
                            if (animStateInfo.IsName("SlashBlade"))
                            {
                                return "BanditTweaksInvalidLayer";
                            }
                        }
                    }
                    return animLayer;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: Bandit KnifeHitboxCancel ThrowSmokebomb IL Hook failed.");
            }
        }

        private void Bandit2FirePrimaryBase_OnEnter(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
                x => x.MatchLdstr("Gesture, Additive")))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<string, EntityStates.Bandit2.ThrowSmokebomb, string>>((animLayer, self) =>
                {
                    Animator modelAnimator = self.GetModelAnimator();
                    if (modelAnimator)
                    {
                        int layerIndex = modelAnimator.GetLayerIndex("Gesture, Additive");
                        if (layerIndex >= 0)
                        {
                            AnimatorStateInfo animStateInfo = modelAnimator.GetCurrentAnimatorStateInfo(layerIndex);
                            if (animStateInfo.IsName("SlashBlade"))
                            {
                                return "BanditTweaksInvalidLayer";
                            }
                        }
                    }
                    return animLayer;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: Bandit KnifeHitboxCancel Bandit2FirePrimaryBase IL Hook failed.");
            }
        }

        private void Reload_OnEnter(ILContext il)
        {
            ILCursor c = new ILCursor(il);

            if (c.TryGotoNext(MoveType.After,
                x => x.MatchLdstr("Gesture, Additive")))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<string, EntityStates.Bandit2.ThrowSmokebomb, string>>((animLayer, self) =>
                {
                    Animator modelAnimator = self.GetModelAnimator();
                    if (modelAnimator)
                    {
                        int layerIndex = modelAnimator.GetLayerIndex("Gesture, Additive");
                        if (layerIndex >= 0)
                        {
                            AnimatorStateInfo animStateInfo = modelAnimator.GetCurrentAnimatorStateInfo(layerIndex);
                            if (animStateInfo.IsName("SlashBlade"))
                            {
                                return "BanditTweaksInvalidLayer";
                            }
                        }
                    }
                    return animLayer;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: Bandit KnifeHitboxCancel Reload IL Hook failed.");
            }
        }

        private void EnterReload_OnEnter(ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(MoveType.After,
                x => x.MatchLdstr("Gesture, Additive")))
            {
                c.Emit(OpCodes.Ldarg_0);
                c.EmitDelegate<Func<string, EntityStates.Bandit2.ThrowSmokebomb, string>>((animLayer, self) =>
                {
                    Animator modelAnimator = self.GetModelAnimator();
                    if (modelAnimator)
                    {
                        int layerIndex = modelAnimator.GetLayerIndex("Gesture, Additive");
                        if (layerIndex >= 0)
                        {
                            AnimatorStateInfo animStateInfo = modelAnimator.GetCurrentAnimatorStateInfo(layerIndex);
                            if (animStateInfo.IsName("SlashBlade"))
                            {
                                return "BanditTweaksInvalidLayer";
                            }
                        }
                    }
                    return animLayer;
                });
            }
            else
            {
                Debug.LogError("RiskyFixes: Bandit KnifeHitboxCancel EnterReload IL Hook failed.");
            }
        }
    }
}
