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
        private GameObjectPool<GameObject> _objectPool;
        
        private void OnEnable()
        {            
            _objectPool = new GameObjectPool<GameObject>(CreateBullet, OnPoolCleanup, OnActivation, OnRemoval);
        }

        public GameObject GetBullet() => _objectPool.Get();
        private GameObject CreateBullet()
        {
            var bullet = Instantiate(bulletType.bulletPrefab);
            bullet.GetComponent<Bullet>().OnBulletDestroyed ??= (b) => _objectPool.Release(b.gameObject); 
            return bullet;
        }

        private void OnActivation(GameObject objectToActivate) => objectToActivate.SetActive(true);
        private void OnRemoval(GameObject objectToRemove) =>  objectToRemove.SetActive(false);
        private void OnPoolCleanup(GameObject objectToDestroy)
        {
            objectToDestroy.GetComponent<Bullet>().OnBulletDestroyed = null; 
            Destroy(objectToDestroy);
        }
    }
}