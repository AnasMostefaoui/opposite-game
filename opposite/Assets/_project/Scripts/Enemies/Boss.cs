using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using OppositeGame._project.Scripts.Managers;
using OppositeGame._project.Scripts.mechanics.Bullets;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.Splines;

namespace OppositeGame._project.Scripts.Enemies
{
    public class Boss : MonoBehaviour
    {
        [SerializeField] private MMF_Player explosionFeedback;
        [SerializeField] private ParticleSystem explosionParticles;
        [SerializeField] private float maxLifePoints = 50;
        [SerializeField] private float stageSwitchTime = 3f;
        private float _currentLifePoints;
        private Collider2D _collider2D;
        public List<BossFightStage> bossFightStages;
        private int currentStageIndex = 0;
        public static event Action OnBossDefeated;
        private SplineAnimate _splineAnimate;
        private SpriteRenderer _spriteRenderer;
        private float _stageSwitchTimer;
        private bool _isDefeated;
        private void Awake()
        {
            _collider2D = GetComponent<Collider2D>();
            _splineAnimate = GetComponent<SplineAnimate>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _currentLifePoints = maxLifePoints;
        }

        private void Start()
        {
            _currentLifePoints = maxLifePoints;
            _collider2D.enabled = true;
            foreach (var bossFightStage in bossFightStages)
            {
                // bossFightStage.enemies.ForEach( enemy => enemy.OnEnemyDisabled += CheckStage );
            }
        }

        private void Update()
        {
            if(_isDefeated) return;
            _stageSwitchTimer += Time.deltaTime;
            if(_stageSwitchTimer >= stageSwitchTime)
            {
                _stageSwitchTimer = 0;
                PlayNextStage();
            }
        }

        private bool IsStageCompleted => bossFightStages[currentStageIndex].IsCompleted;

        private void CheckStage()
        {
            if(IsStageCompleted)
            {
                PlayNextStage();
            }
        }
        
        private void PlayNextStage()
        {
            bossFightStages[currentStageIndex].PauseStage();
            _collider2D.enabled = true;
            currentStageIndex = (currentStageIndex + 1) % bossFightStages.Count;
            var index = currentStageIndex % bossFightStages.Count;
            bossFightStages[index].SetupStage();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var isPlayerBullets = other.CompareTag("pr-small-bullet") || other.CompareTag("pb-small-bullet");
            if(!isPlayerBullets) return;
            var bullet = other.GetComponent<Bullet>();
            _currentLifePoints -= bullet.Damage;
            if(_currentLifePoints <= 0 && !_isDefeated)
            {
                Defeated();
            }
        }

        private void Defeated()
        {
            _isDefeated = true;
            _collider2D.enabled = false;
            foreach (var boxCollider2D in GetComponents<BoxCollider2D>())
            {
                boxCollider2D.enabled = false;
            }
            _splineAnimate.Pause();
            bossFightStages.ForEach( stage => stage.gameObject.SetActive(false) );
            _spriteRenderer.color = Color.clear;
            GameManager.Instance.CurrentScore += 500;
            if (explosionParticles)
            {
                explosionParticles.Play();
            }

            if (explosionFeedback)
            {
                Debug.Log("Pla");
                explosionFeedback.PlayFeedbacks();
                
                explosionFeedback.Events.OnComplete.AddListener(() =>
                {
                    gameObject.SetActive(false);
                    OnBossDefeated?.Invoke();
                });
            }
        }
        
        
        
        
    }
}