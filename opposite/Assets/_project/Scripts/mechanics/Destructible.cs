using System;
using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.Patterns;
using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics
{
    public class Destructible : MonoBehaviour, IPoolable<GameObject>
    {
        [SerializeField] public ParticleSystem explosionEffectPrefab;
        public Action<GameObject> OnRelease { get; set; }
        private Camera _camera;

        public float LifePoints { set; get; } = 5f;

        private void Awake()
        {
            _camera = Camera.main;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_camera.IsPointInViewport(transform.position) == false) return;
            var bullet = other.TryGetComponent<Bullet>(out var bulletComponent);
            Debug.Log("Destructible hit by bullet");
            if (bullet && LifePoints > 0)
            {
                LifePoints -= bulletComponent.Damage;
                
            }
            
            if (LifePoints <= 0)
            {
                Debug.Log("Destructible destroyed");
                DisplayHitEffect();
                gameObject.SetActive(false);
                OnRelease?.Invoke(gameObject);
            }
 
        }
        
        private void DisplayHitEffect()
        {
            if(explosionEffectPrefab == null) return;
            var instance = ObjectPoolManager.Retrieve(explosionEffectPrefab.gameObject);
            if(instance == null) return;
            
            instance.transform.SetParent(transform);   
            if (instance.TryGetComponent<ParticleSystem>(out var explosionEffect))
            {
                explosionEffect.Play();
            }
        }
    }
}