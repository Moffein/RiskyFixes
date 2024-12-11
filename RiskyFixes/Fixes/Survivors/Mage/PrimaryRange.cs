using RoR2.Projectile;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RiskyFixes.Fixes.Survivors.Mage
{
    public class PrimaryRange : FixBase<PrimaryRange>
    {
        public override string ConfigCategoryString => "Survivors - Artificer";

        public override string ConfigOptionName => "(Server-Side) Primary Range";

        public override string ConfigDescriptionString => "Fix projectiles disappearing at midrange.";

        public override bool StopLoadOnConfigDisable => true;

        protected override void ApplyChanges()
        {
            GameObject fireBolt = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MageFireboltBasic.prefab").WaitForCompletion();
            IncreaseProjectileLifetime(fireBolt);

            GameObject plasmaBolt = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Mage/MageLightningboltBasic.prefab").WaitForCompletion();
            IncreaseProjectileLifetime(plasmaBolt);
        }

        private void IncreaseProjectileLifetime(GameObject projectile)
        {
            ProjectileSimple ps = projectile.GetComponent<ProjectileSimple>();
            ps.lifetime = 10f;

            ProjectileImpactExplosion pie = projectile.GetComponent<ProjectileImpactExplosion>();
            if (pie) pie.lifetime = 10f;
        }
    }
}
