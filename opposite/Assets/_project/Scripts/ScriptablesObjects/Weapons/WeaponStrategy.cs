using System;
using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using OppositeGame._project.Scripts.Patterns;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    public abstract class WeaponStrategy : ScriptableObject
    {
        [SerializeField] public float fireRate = 1f;
        [SerializeField] public float bulletSpeed = 1f;
        [SerializeField] public BulletType bulletType;
        
        public virtual void Initialize() { }
        public virtual void SetupWeapon() { }

        protected void PrepareBullet(Bullet bullet, Transform startPosition, int layerMask, float rotationAngle = 0f)
        {
            bullet.transform.position = startPosition.position;
            // rotate to match the start position rotation to avoid messing with the calculation below
            bullet.transform.rotation = startPosition.rotation;
            bullet.transform.SetParent(startPosition);
            bullet.SetBulletSpeed(bulletSpeed);
            bullet.transform.Rotate(0, 0, rotationAngle );
            bullet.gameObject.layer = layerMask;
        }

        protected Bullet GetBullet(PolarityType polarity = PolarityType.Blue)
        {
            var instance = ObjectPoolManager.Retrieve(bulletType.bulletPrefab.gameObject);
            if(instance == null) return null;
            var bullet = instance.GetComponent<Bullet>();
            bullet.OnUpdate = null;
            bullet.PolarityType = polarity;
            return bullet;
        }

        public abstract void Fire(Transform startPosition, int layerMask, PolarityType polarity = PolarityType.Blue);
    }
}