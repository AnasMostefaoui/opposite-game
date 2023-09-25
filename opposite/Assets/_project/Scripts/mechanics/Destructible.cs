using System;
using System.Linq;
using MoreMountains.Feedbacks;
using OppositeGame._project.Scripts.Enemies;
using OppositeGame._project.Scripts.Environment;
using OppositeGame._project.Scripts.Managers;
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
        public PlayerShield RedPlayerShield { get; set; }
        public PlayerShield BluePlayerShield { get; set; }
        private Camera _camera;


        private void Awake()
        {
            _camera = Camera.main;
            if (TryGetComponent<PolarityProvider>(out var playerPolarityProvider))
            {
                PolarityProvider = playerPolarityProvider;
            }           
            if (TryGetComponent<PlayerShield>(out var redShield))
            {
                RedPlayerShield = redShield;
            } 
            var shields = GetComponents<PlayerShield>();
            if (shields != null && shields.Length > 1)
            {
                // this is risky, better have a search by polarity.
                RedPlayerShield = shields[0];
                BluePlayerShield = shields[1];
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("audio-boss-level")) return;
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
            var isPlayer = CompareTag("Player");
            
            if(!isPlayer)
            {
                if (PolarityProvider == null)
                {
                    TakeDamage(bullet.Damage);
                }
                else
                {
                    var damageMultiplier = bullet.PolarityType == PolarityProvider.PolarityType ? 1 : 2;
                    TakeDamage(bullet.Damage * damageMultiplier);
                }
                return;
            }
            
            var player = GetComponent<PlayerController>();
            var samePolarity = bullet.PolarityType == PolarityProvider.PolarityType;
            var samePolarityShield = bullet.PolarityType == PolarityType.Red ? RedPlayerShield : BluePlayerShield;
            // we take full damage if the bullet has opposite polarity then the player
            if (!samePolarity)
            {
                TakeDamage(bullet.Damage);
                player.HitFeedBack();
                return;
            }

            // If the shield is on and polarity are similar, absorb same bullets
            if (samePolarityShield && samePolarityShield.isShieldActive)
            {
                player.AbsorbFeedBack();
                TakeDamage(0);
            }
            else
            {
                // if the shield is off, but we have the same polarity reduce the polarity shield energy
                samePolarityShield.ReduceEnergy(bullet.Damage * 0.5f);
                player.HitFeedBack();
                // if the energy is off take full damage, otherwise absorb damage
                TakeDamage( samePolarityShield.Energy > 0 ? 0 : bullet.Damage);
            }
        } 
        
        private void CollideWithAsteroid(Asteroid astroid)
        {
            TakeDamage(astroid.Damage);
        } 
        
        private void CollideWithLaser(LaserTrap laser)
        {
            if(!CompareTag("Player") || PolarityProvider == null) return;
            
            var samePolarity = laser.polarityType == PolarityProvider.PolarityType;
            var samePolarityShield = laser.polarityType == PolarityType.Red ? RedPlayerShield : BluePlayerShield;
            if(samePolarity && samePolarityShield.isShieldActive)
            {
                samePolarityShield.PlayLaserVoidFeedback();
                return;
            };
            TakeDamage(laser.damage);
        } 

        public void TakeDamage(float damage)
        {
            LifePoints -= damage;
            // call shake feedback?
            if (LifePoints > 0) return;
            
            DisplayHitEffect();

            if (!CompareTag("Player"))
            {
                gameObject.SetActive(false);
            }
            else
            {
                
                var playerController = GetComponent<PlayerController>();
                if(playerController.IsDead) return;
                GameManager.Instance.currentLifePoint -= 1;
                
            }
            // if it's player shake the camera ?
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