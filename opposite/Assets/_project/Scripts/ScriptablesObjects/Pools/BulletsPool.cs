using System;
using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.Patterns;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Pools
{
    [CreateAssetMenu(fileName = "BulletsPool", menuName = "OppositeGame/Pools/BulletsPool", order = 1)]
    public class BulletsPool : ScriptableObject
    {
        public  BulletType bulletType;
        public  static BulletImpactPool bulletImpactPool;
        private static GameObjectPool<Bullet> _objectPool;
        public Bullet GetBullet() => ObjectPoolManager.Retrieve(bulletType.bulletPrefab.gameObject).GetComponent<Bullet>();
    }
}