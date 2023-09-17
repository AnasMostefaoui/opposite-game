using System;
using OppositeGame._project.Scripts.mechanics.Movement;
using OppositeGame._project.Scripts.Patterns;
using OppositeGame._project.Scripts.ScriptablesObjects;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Pool;

namespace OppositeGame._project.Scripts.mechanics.Bullets
{
    public class Bullet : MonoBehaviour, IPoolable<Bullet>
    {
        [SerializeField] public PolarityType polarityType = PolarityType.None;
        [SerializeField] private BulletType bulletType; 
        public Action<Bullet> OnRelease { get; set; }
        public Action OnUpdate;
        
        private Camera _camera;
        private float _speed = 1f;
        private GameObject _firingEffect;
        private GameObject _bullet;
        private float _lifetimeTimer = 0f;


        public float Damage => bulletType.damage;
        public void SetBulletSpeed(float speed)
        {
            _speed = speed;
        }
        
        private void Start()
        {
            if (bulletType.firingEffectPrefab)
            {
                _firingEffect = Instantiate(bulletType.firingEffectPrefab, transform.position, Quaternion.identity);
                _firingEffect.transform.forward = gameObject.transform.forward;
                _firingEffect.transform.SetParent(transform);
            }
            _camera ??= Camera.main;
        }

        private void Update()
        {
            transform.SetParent(null);
            transform.position += transform.up * (_speed * Time.deltaTime);
            OnUpdate?.Invoke();
            
            _lifetimeTimer += Time.deltaTime;
            // we release the bullet from the pool if it's destroyed or it's life time is over
            if (_lifetimeTimer >= bulletType.lifeTime)
            {
                DisplayHitEffect();
                _lifetimeTimer = 0;
                OnRelease?.Invoke(this);
            }

        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_camera.IsPointInViewport(transform.position) == false) return;
            DisplayHitEffect();
            _lifetimeTimer = 0;
            OnRelease?.Invoke(this);
        }
        
        private void DisplayHitEffect()
        {
            if (bulletType.hitEffectPrefab)
            {
                Instantiate(bulletType.hitEffectPrefab, transform.position, Quaternion.identity);
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, transform.up * 0.25f);
        }
    }
}