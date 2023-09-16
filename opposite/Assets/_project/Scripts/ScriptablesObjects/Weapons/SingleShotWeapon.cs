using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.mechanics.weapons;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    [CreateAssetMenu(fileName = "SingleShotWeapon", menuName = "OppositeGame/Weapons", order = 1)]
    public class SingleShotWeapon : WeaponStrategy
    {
        public override void Fire(Transform startTransform, LayerMask layerMask)
        {
            Debug.Log($"startTransform.position: {startTransform.position}");
            var bullet = Instantiate(bulletType.bulletPrefab, startTransform.position, startTransform.rotation);
            bullet.transform.SetParent(startTransform);
            bullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
            bullet.layer = layerMask;
        }
    }
}