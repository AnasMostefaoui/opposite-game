using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.mechanics.weapons;
using OppositeGame._project.Scripts.Patterns;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    [CreateAssetMenu(fileName = "SingleShotWeapon", menuName = "OppositeGame/Weapons", order = 1)]
    public class SingleShotWeapon : WeaponStrategy
    {
        public override void Fire(Transform startTransform, int layer)
        {
            var bullet = GetBullet();
            //var bullet = Instantiate(bulletType.bulletPrefab, startTransform.position, startTransform.rotation);
            bullet.transform.position = startTransform.position;
            bullet.transform.rotation = startTransform.rotation;
            bullet.transform.SetParent(startTransform);
            bullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
            bullet.layer = layer;
        }
    }
}