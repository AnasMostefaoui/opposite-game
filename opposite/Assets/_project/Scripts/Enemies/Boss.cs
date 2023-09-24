using System;
using System.Collections.Generic;
using OppositeGame._project.Scripts.Managers;
using OppositeGame._project.Scripts.mechanics.Bullets;
using UnityEngine;

namespace OppositeGame._project.Scripts.Enemies
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private float maxLifePoints = 50;
        private float _currentLifePoints;
        private Collider2D _collider2D;
        private List<BossFightStage> _bossFightStages;
        private int currentStageIndex = 0;
        public float normalizedLifePoints => _currentLifePoints / maxLifePoints;
        public static event Action OnBossDefeated;
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _currentLifePoints = maxLifePoints;
        }

        private void Start()
        {
            _currentLifePoints = maxLifePoints;
            _collider2D.enabled = true;
            foreach (var bossFightStage in _bossFightStages)
            {
                bossFightStage.enemies.ForEach( enemy => enemy.OnEnemyDisabled += CheckStage );
            }
        }
        
        private bool IsStageCompleted => _bossFightStages[currentStageIndex].IsCompleted;

        private void CheckStage()
        {
            if(IsStageCompleted)
            {
                PlayNextStage();
            }
        }
        
        private void PlayNextStage()
        {
            _collider2D.enabled = true;
            currentStageIndex += 1;
            if(currentStageIndex >= _bossFightStages.Count) return;
            _bossFightStages[currentStageIndex].PlayStage();
        }

        private void SetTheStage()
        {
            var stage = _bossFightStages[currentStageIndex];
            stage.SetupStage();
            _collider2D.enabled = !stage.isVulnerable;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var isPlayerBullets = other.CompareTag("pr-small-bullet") || other.CompareTag("pb-small-bullet");
            if(!isPlayerBullets) return;
            var bullet = other.GetComponent<Bullet>();
            _currentLifePoints -= bullet.Damage;
            if(_currentLifePoints <= 0)
            {
                Defeated();
            }
        }

        private void Defeated()
        {
            _collider2D.enabled = false;
            Debug.Log( "Boss defeated" );
            // play destroy animation?
            
            OnBossDefeated?.Invoke();
        }
        
        
    }
}