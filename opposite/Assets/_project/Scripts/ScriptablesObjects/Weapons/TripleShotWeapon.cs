using OppositeGame._project.Scripts.mechanics.Magnetism;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    [CreateAssetMenu(fileName = "TripleShotWeapon", menuName = "OppositeGame/Weapons/TripleShotWeapon", order = 1)]
    public class TripleShotWeapon : WeaponStrategy
    {
        [SerializeField] private float horizontalSpacing = 2f;
        [SerializeField] private float verticalSpacing = 2f;
        public override void Fire(Transform startTransform, int layer, PolarityType polarity = PolarityType.Blue )
        {
            if(startTransform == null) return;
            
            var rightBullet = GetBullet();
            var middleBullet = GetBullet();
            var leftBullet = GetBullet();
            
            if(rightBullet && leftBullet && middleBullet)
            {
                var rightPosition = startTransform.position + startTransform.right * horizontalSpacing;
                var leftPosition = startTransform.position - startTransform.right * horizontalSpacing;
                var middlePosition = startTransform.position + startTransform.up * verticalSpacing;
                PrepareBullet(rightBullet, startTransform, layer);
                PrepareBullet(leftBullet, startTransform, layer);
                PrepareBullet(middleBullet, startTransform, layer);
                rightBullet.transform.position = rightPosition;
                leftBullet.transform.position = leftPosition;
                middleBullet.transform.position = middlePosition;
            };
            
        }
    }
}