using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    public abstract class WeaponStrategy : ScriptableObject
    {
        [SerializeField] public float fireRate = 1f;
        [SerializeField] public float bulletSpeed = 1f;
        [SerializeField] public BulletsPool bulletsPool;
        
        public virtual void Initialize()
        {
        }
        
        public virtual void SetupWeapon()
        {
        }

        protected void PrepareBullet(GameObject bullet, Transform startPosition, int layerMask, float rotationAngle = 0f)
        {
            //var bullet = Instantiate(bulletType.bulletPrefab, startTransform.position, startTransform.rotation);
            bullet.transform.position = startPosition.position;
            // rotate to match the start position rotation to avoid messing with the calculation below
            bullet.transform.rotation = startPosition.rotation;
            bullet.transform.SetParent(startPosition);
            bullet.GetComponent<Bullet>().SetBulletSpeed(bulletSpeed);
            bullet.transform.Rotate(0, 0, rotationAngle );
            bullet.layer = layerMask;
        }

        protected GameObject GetBullet() => bulletsPool.GetBullet();
        
        public abstract void Fire(Transform startPosition, int layerMask);
    }
}