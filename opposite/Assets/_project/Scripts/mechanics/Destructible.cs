using System;
using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.Patterns;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics
{
    public class Destructible : MonoBehaviour, IPoolable<GameObject>
    {
        [SerializeField] public GameObject destructionEffect;
        public Action<GameObject> OnRelease { get; set; }
        private bool _canBeDestroyed = false;
        private float _currentLifePoints;
        private Camera _camera;
        
        private void Start()
        {
            _camera ??= Camera.main;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            
            if(_camera.IsPointInViewport(transform.position) == false) return;
            var bullet = other.TryGetComponent<Bullet>(out var bulletComponent);
            if (bullet && _currentLifePoints > 0)
            {
                _currentLifePoints -= bulletComponent.Damage;
                Debug.Log("_currentLifePoints: " + _currentLifePoints);
                
            }
            
            if (_currentLifePoints <= 0)
            {
                DisplayHitEffect();
                OnRelease?.Invoke(gameObject);
                
            }
 
        }
        
        private void DisplayHitEffect()
        {
            if (destructionEffect)
            {
                Instantiate(destructionEffect, transform.position, Quaternion.identity);
            }
        }
    }
}