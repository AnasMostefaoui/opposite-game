using OppositeGame._project.Scripts.Patterns;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Pools
{
    [CreateAssetMenu(fileName = "BulletImpactPool", menuName = "OppositeGame/Pools/BulletImpactPool", order = 1)]
    public class BulletImpactPool : ScriptableObject
    {
        [SerializeField] private ParticleSystem prefab;
        private GameObjectPool<ParticleSystem> _objectPool;
        private void OnEnable()
        {            
            _objectPool = new GameObjectPool<ParticleSystem>(
                CreateObject, 
                OnPoolCleanup, 
                OnActivation, 
                OnRemoval, 
                10, 
                20);
        }
        
        public ParticleSystem GetHitEffect() => _objectPool.Get();
        private ParticleSystem CreateObject()
        {
            var particleSystem = Instantiate(prefab);
            
            return particleSystem;
        }

        private void OnActivation(ParticleSystem objectToActivate)
        {
        }

        private void OnRemoval(ParticleSystem objectToRemove) {
        }
        
        private void OnPoolCleanup(ParticleSystem objectToDestroy)
        {
            
            Destroy(objectToDestroy);
        }
    }
}