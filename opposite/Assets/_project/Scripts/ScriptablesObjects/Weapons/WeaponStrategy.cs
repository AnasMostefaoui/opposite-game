using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Weapons
{
    public abstract class WeaponStrategy : ScriptableObject
    {
        public BulletType bulletType;
        public float fireRate = 1f;
        public float bulletSpeed = 1f;

        public virtual void setupWeapon()
        {
        }

        public abstract void Fire(Transform startPosition, LayerMask layerMask);
    }
}