using RoR2;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.Enemies
{
    public class CollectiveEliteIcon : FixBase<CollectiveEliteIcon>
    {
        public override string ConfigCategoryString => "Enenemies - DLC3";

        public override string ConfigOptionName => "Collective Elite Icon";

        public override string ConfigDescriptionString => "Fixes missing Collective Elite Icon.";

        protected override void ApplyChanges()
        {
            BuffDef bdCollective = Addressables.LoadAssetAsync<BuffDef>("RoR2/DLC3/Collective/bdEliteCollective.asset").WaitForCompletion();
            if (bdCollective.iconSprite == null)
            {
                bdCollective.iconSprite = Addressables.LoadAssetAsync<Sprite>("RoR2/DLC3/Collective/texEliteCollectiveIcon.png").WaitForCompletion();
            }
        }
    }
}
