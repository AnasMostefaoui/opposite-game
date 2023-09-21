using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using UnityEngine;
using UnityEngine.Serialization;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    [CreateAssetMenu(fileName = "SpreadShotWeapon", menuName = "OppositeGame/Weapons/SpreadShotWeapon")]
    public class SpreadShotWeapon : WeaponStrategy
    {
        [SerializeField] private float spreadAngle;
        [SerializeField] private int numberOfBullets;

        public override void Fire(Transform startPosition, int layerMask, PolarityType polarity = PolarityType.Blue)
        {
            if(startPosition == null) return;
            // Calculate the angle between the start position and the target position and the angle step for each bullet.
            var targetDirection = startPosition.forward;
            var targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            var halfAngle = spreadAngle / 2f;
            var startAngle = targetAngle - halfAngle;
            var angleStep = spreadAngle / (numberOfBullets - 1);
            var currentAngle = startAngle;
            // place the bullets in a circle around the start position and assign speed and rotation based on the position on the circle.
            for (var i = 0; i < numberOfBullets; i++)
            {
                var bullet = GetBullet();
                if(bullet)
                {
                    PrepareBullet(bullet, startPosition, layerMask, currentAngle);
                    currentAngle += angleStep; 
                };  
            }
        }
    }
}