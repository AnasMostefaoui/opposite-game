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
        [SerializeField] public GameObject destructionEffect;
        [SerializeField] public BulletImpactPool explosionPool;
        public Action<GameObject> OnRelease { get; set; }
        private bool _canBeDestroyed = false;
        private float _currentLifePoints = 5f;
        private Camera _camera;

        public float LifePoints
        {
            set => _currentLifePoints = value;
            get => _currentLifePoints;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_camera.IsPointInViewport(transform.position) == false) return;
            var bullet = other.TryGetComponent<Bullet>(out var bulletComponent);
            if (bullet && _currentLifePoints > 0)
            {
                _currentLifePoints -= bulletComponent.Damage;
                
            }
            
            if (_currentLifePoints <= 0)
            {
                Debug.Log("Destructible destroyed");
                DisplayHitEffect();
                gameObject.SetActive(false);
                OnRelease?.Invoke(gameObject);
                
            }
 
        }
        
        private void DisplayHitEffect()
        {
            if (explosionPool)
            {
                explosionPool.GetHitEffect();
            }
        }
    }
}