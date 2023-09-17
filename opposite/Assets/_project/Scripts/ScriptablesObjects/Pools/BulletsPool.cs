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
        public  BulletImpactPool bulletImpactPool;
        private GameObjectPool<Bullet> _objectPool;
        
        private void OnEnable()
        {            
            _objectPool = new GameObjectPool<Bullet>(CreateBullet, OnPoolCleanup, OnActivation, OnRemoval);
        }

        public Bullet GetBullet() => _objectPool.Get();
        private Bullet CreateBullet()
        {
            var bullet = Instantiate(bulletType.bulletPrefab);
            bullet.OnRelease ??= (b) =>
            {
                var hitEffect = bulletImpactPool.GetHitEffect();
                hitEffect.transform.position = b.transform.position;
                hitEffect.Play();
                _objectPool.Release(b);
            }; 
            return bullet;
        }

        private void OnActivation(Bullet objectToActivate)
        {
            if (objectToActivate != null )
            {
                objectToActivate.gameObject.SetActive(true);
            } 
        }

        private void OnRemoval(Bullet objectToRemove)
        {
            if (objectToRemove != null )
            {
                objectToRemove.gameObject.SetActive(false);
            } 
        }

        private void OnPoolCleanup(Bullet objectToDestroy)
        {
            objectToDestroy.GetComponent<Bullet>().OnRelease = null; 
            Destroy(objectToDestroy);
        }
    }
}