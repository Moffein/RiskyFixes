using MonoMod.Cil;
using UnityEngine;

namespace RiskyFixes.Fixes.Survivors.Treebot
{
    public class FruitingNullref : FixBase<FruitingNullref>
    {
        public override string ConfigCategoryString => "Survivors - REX";

        public override string ConfigOptionName => "(Server-Side) Fruiting Nullref";

        public override string ConfigDescriptionString => "Fixes nullrefs related to DIRECTIVE: INJECT.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            IL.RoR2.GlobalEventManager.OnCharacterDeath += GlobalEventManager_OnCharacterDeath;
        }

        private void GlobalEventManager_OnCharacterDeath(MonoMod.Cil.ILContext il)
        {
            ILCursor c = new ILCursor(il);
            if (c.TryGotoNext(
                 x => x.MatchLdstr("Prefabs/Effects/TreebotFruitDeathEffect.prefab")
                ))
            {
                c.Next.Operand = "Prefabs/Effects/TreebotFruitDeathEffect";
            }
            else
            {
                Debug.LogError("RiskyFixes: Treebot FruitingNullref IL Hook failed");
            }
        }
    }
}
