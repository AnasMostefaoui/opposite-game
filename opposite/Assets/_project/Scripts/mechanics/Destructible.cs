using System;
using OppositeGame._project.Scripts.Enemies;
using OppositeGame._project.Scripts.Environment;
using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using OppositeGame._project.Scripts.mechanics.Scoring;
using OppositeGame._project.Scripts.mechanics.Traps;
using OppositeGame._project.Scripts.Patterns;
using OppositeGame._project.Scripts.Player;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics
{
    public class Destructible : MonoBehaviour, IPoolable<GameObject>
    {
        [SerializeField] public float LifePoints = 5f;
        [SerializeField] public ParticleSystem explosionEffectPrefab;
        public Action<GameObject> OnRelease { get; set; }
        public PolarityProvider PolarityProvider { get; set; }
        public PlayerShield PlayerShield { get; set; }
        private Camera _camera;


        private void Awake()
        {
            _camera = Camera.main;
            if (TryGetComponent<PlayerPolarity>(out var playerPolarityProvider))
            {
                PolarityProvider = playerPolarityProvider;
            }           
            if (TryGetComponent<PlayerShield>(out var shield))
            {
                PlayerShield = shield;
            } 
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(_camera.IsPointInViewport(transform.position) == false) return;
            
            if (other.TryGetComponent<PlayerController>(out var player))
            {
                EnemyCollideWithPlayer(player);
            }           
            if (other.TryGetComponent<Enemy>(out var enemy))
            {
                CollideWithEnemy(enemy);
            }
            
            if (other.TryGetComponent<Bullet>(out var bulletComponent))
            {
                CollideWithBullet(bulletComponent);
            }
            
            if (other.TryGetComponent<LaserTrap>(out var laserTrap))
            {
                CollideWithLaser(laserTrap);
            }
            
            if (other.TryGetComponent<Asteroid>(out var asteroid))
            {
                CollideWithAsteroid(asteroid);
            }            
            if (other.TryGetComponent<RotorMine>(out var mine))
            {
                CollidedWithMine(mine);
            }
            
        }

        private void EnemyCollideWithPlayer(PlayerController component)
        {
            TakeDamage(9999);
        }

        private void CollideWithEnemy(Enemy enemy)
        {
            TakeDamage(9999);
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.TryGetComponent<LaserTrap>(out var laserTrap))
            {
                CollideWithLaser(laserTrap);
            }
        }

        private void CollideWithBullet(Bullet bullet)
        {
            if (PlayerShield == null || PlayerShield.isShieldActive == false)
            {
                TakeDamage(bullet.Damage);
                return;
            }
            
            if(PlayerShield.isShieldActive && bullet.PolarityType == PolarityProvider.PolarityType)
            {
                // or reduced damage?
                TakeDamage(0);
            }
            else
            {
                TakeDamage(bullet.Damage);
            }
        } 
        
        private void CollideWithAsteroid(Asteroid astroid)
        {
            TakeDamage(astroid.Damage);
        } 
        
        private void CollideWithLaser(LaserTrap laser)
        {
            if(!CompareTag("Player") || PolarityProvider == null) return;
            var shield = GetComponent<PlayerShield>();
            if(laser.polarityType == PolarityProvider.PolarityType && shield.isShieldActive) return;
            TakeDamage(laser.damage);
        } 

        public void TakeDamage(float damage)
        {
            LifePoints -= damage;
            if (LifePoints > 0) return;
            
            DisplayHitEffect();

            if (!CompareTag("Player"))
            {
                gameObject.SetActive(false);
            }
            OnRelease?.Invoke(gameObject);
        }
        
        
        private void CollidedWithMine(RotorMine component)
        {
            TakeDamage(component.Damage);
        }
        
        private void DisplayHitEffect()
        {
            if(explosionEffectPrefab == null || explosionEffectPrefab.gameObject == null) return;
            var instance = ObjectPoolManager.Retrieve(explosionEffectPrefab.gameObject);
            if(instance == null) return;
            
            instance.transform.position = transform.position;   
            if (instance.TryGetComponent<ParticleSystem>(out var explosionEffect))
            {
                explosionEffect.Play();
            }
        }
    }
}