using OppositeGame._project.Scripts.mechanics.Magnetism;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    [CreateAssetMenu(fileName = "DoubleShootWeapon", menuName = "OppositeGame/Weapons/DoubleShootWeapon", order = 1)]
    public class DoubleShootWeapon : WeaponStrategy
    {
        [SerializeField] private float spacing = 2f;
        public override void Fire(Transform startTransform, int layer, PolarityType polarity = PolarityType.Blue )
        {
            if(startTransform == null) return;
            
            var rightBullet = GetBullet(polarity);
            var leftBullet = GetBullet(polarity);
            if(rightBullet && leftBullet)
            {
                var rightPosition = startTransform.position + startTransform.right * spacing;
                var leftPosition = startTransform.position - startTransform.right * spacing;
                PrepareBullet(rightBullet, startTransform, layer);
                PrepareBullet(leftBullet, startTransform, layer);
                rightBullet.transform.position = rightPosition;
                leftBullet.transform.position = leftPosition;
            };
            
        }
    }
}