using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    public abstract class WeaponStrategy : ScriptableObject
    {
        [SerializeField] public float fireRate = 1f;
        [SerializeField] public float bulletSpeed = 1f;
        [SerializeField] public BulletsPool bulletsPool;
        
        public virtual void SetupWeapon()
        {
        }

        protected GameObject GetBullet() => bulletsPool.GetBullet();
        
        public abstract void Fire(Transform startPosition, int layerMask);
    }
}