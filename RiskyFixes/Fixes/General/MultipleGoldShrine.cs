using RoR2;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.General
{
    public class MultipleGoldShrine : FixBase<MultipleGoldShrine>
    {
        public override string ConfigCategoryString => "General";

        public override string ConfigOptionName => "(Server-Side) Shrine of Gold Spawnlimit";

        public override string ConfigDescriptionString => "Limits Shrine of Gold to 1 per stage.";

        protected override void ApplyChanges()
        {
            InteractableSpawnCard isc = Addressables.LoadAssetAsync<InteractableSpawnCard>("RoR2/Base/ShrineGoldshoresAccess/iscShrineGoldshoresAccess.asset").WaitForCompletion();
            isc.maxSpawnsPerStage = 1;
        }
    }
}
