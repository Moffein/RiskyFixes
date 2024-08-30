using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.AddressableAssets;
using UnityEngine;

namespace RiskyFixes.Fixes.Survivors.Seeker
{
    //Directly taken from https://thunderstore.io/package/score/SeekerVFXFix/ as requested by the description.
    public class M2Vfx : FixBase<M2Vfx>
    {
        public override string ConfigCategoryString => "Survivors - Seeker";

        public override string ConfigOptionName => "(Client-Side) Unseen Hand VFX Fix";

        public override string ConfigDescriptionString => "Fixes missing muzzleflash effect for Unseen Hand.";

        protected override void ApplyChanges()
        {
            GameObject prefab = Addressables.LoadAssetAsync<GameObject>("RoR2/DLC2/Seeker/SoulSpiralMuzzleflashVFX.prefab").WaitForCompletion();
            SneedUtils.SetAddressableEntityStateField("RoR2/DLC2/Seeker/EntityStates.Seeker.UnseenHand.asset", "muzzleflashEffect", prefab);
        }
    }
}
