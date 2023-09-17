using OppositeGame._project.Scripts.mechanics.Bullets;
using UnityEngine;
using UnityEngine.Serialization;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    [CreateAssetMenu(fileName = "SpreadShootWeapon", menuName = "OppositeGame/Weapons/SpreadShootWeapon")]
    public class SpreadShootWeapon : WeaponStrategy
    {
        [SerializeField] private float spreadAngle;
        [SerializeField] private int numberOfBullets;

        public override void Fire(Transform startPosition, int layerMask)
        {
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
                bullet.transform.position = startPosition.position;
                // rotate to match the start position rotation to avoid messing with the calculation below
                bullet.transform.rotation = startPosition.rotation;
                // ensure that it will only hit the target from the weapon carrier layer masks.
                bullet.gameObject.layer = layerMask;
                bullet.transform.Rotate(0, 0, currentAngle );
                bullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
                
                currentAngle += angleStep; 
            }
        }
    }
}