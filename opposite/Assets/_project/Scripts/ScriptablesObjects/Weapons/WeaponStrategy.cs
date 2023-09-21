using System;
using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    public abstract class WeaponStrategy : ScriptableObject
    {
        [SerializeField] public float fireRate = 1f;
        [SerializeField] public float bulletSpeed = 1f;
        [SerializeField] public BulletsPool bulletsPool;
        
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

        protected Bullet GetBullet(PolarityType polarity = PolarityType.Blue, String Tag)
        {
            Debug.Log("ss " + bulletsPool);
            var bullet =  ObjectPoolManager.Retrieve()
            bullet.OnUpdate = null;
            bullet.PolarityType = polarity;
            return bullet;
        }

        public abstract void Fire(Transform startPosition, int layerMask, PolarityType polarity = PolarityType.Blue);
    }
}