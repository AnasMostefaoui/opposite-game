using System;
using System.Collections;
using MoreMountains.Feedbacks;
using OppositeGame._project.Scripts.CameraScripts;
using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.Managers;
using OppositeGame._project.Scripts.mechanics;
using OppositeGame._project.Scripts.mechanics.weapons;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace OppositeGame._project.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    { 
        [SerializeField] private MMF_Player hitFeedback;
        [SerializeField] private MMF_Player absorbFeedback;
        [SerializeField] private MMF_Player timeSlowFeedback;
        [SerializeField] private GameObject player;
        [Header("Camera settings")]
        [SerializeField] private Camera cameraObject;
        [SerializeField] private float lifePoints = 5f;
        [SerializeField] private float invincibilityTime = 3f;
        [Range(0.01f, 0.2f)]
        [SerializeField] private float flickerDuration = 0.01f;
        
        public bool IsAlive => _destructible.LifePoints > 0;
        private PlayerMovementController _movementController;
        private CircleCollider2D _playerCollider;
        private SpriteRenderer _spriteRenderer;
        private TrailRenderer _trailRenderer;
        private Destructible _destructible;
        private PlayerShield _redShield;
        private PlayerShield _blueShield;
        private InputReader _inputReader;
        private Animator _animationController;
        private PlayerWeapon _playerWeapon;
        private Color _originalColor;
        private bool _isReviving;
        private bool _isDead;

        public bool IsDead => _isDead;
        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _animationController = GetComponent<Animator>();
            _playerCollider = GetComponent<CircleCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
            _destructible ??= GetComponent<Destructible>();
            _movementController = GetComponentInChildren<PlayerMovementController>();
            _playerWeapon = GetComponentInChildren<PlayerWeapon>();
            
            var shields = GetComponents<PlayerShield>();
            if (shields != null && shields.Length > 1)
            {
                // this is risky, better have a search by polarity.
                _redShield = shields[0];
                _blueShield = shields[1];
            }
            
            _originalColor = _spriteRenderer.color;
            
            GameManager.Instance.OnContinueScreen += OnContinueScreen;
            GameManager.Instance.OnContinuePlaying += WillKeepPlaying;
            GameManager.Instance.OnMainMenu += OnMainMenu;
            GameManager.Instance.OnLifePointChanged += OnLifePointsChanged;
            _inputReader.OnPowerUpActivated += OnPowerUpActivated;
        }

        private void OnLifePointsChanged(int lifepoint)
        {
            if(gameObject.activeSelf == false) return;
            if(lifepoint <= 0)
            {
                // play a die animation? maybe later
                _animationController.SetTrigger("Die");
                return;
            }

            if(_isDead) return;
            _isDead = true;
            _playerWeapon.enabled = false;
            StartCoroutine(WaitAndRevive(2f));
        }

        private IEnumerator WaitAndRevive(float duration)
        {
            _inputReader.enabled = false;
            _playerCollider.enabled = false;
            _spriteRenderer.enabled = false;
            _trailRenderer.enabled = false;
            _destructible.enabled = false;
            yield return new WaitForSeconds(duration);
            _movementController.ResetPosition(transform.position.With(x: 0)
                .With(y: Camera.main.transform.position.y - 1f));
            _inputReader.enabled = true;
            _destructible.enabled = true;
            _spriteRenderer.enabled = true;
            _trailRenderer.enabled = false;
            _playerCollider.enabled = true;
            _playerWeapon.enabled = true;
            WillKeepPlaying(this, EventArgs.Empty);
        }
        
        private void OnPowerUpActivated()
        {
            if(GameManager.Instance.IsTimeSlowed) return;
            var energyRequirements = 0.9f;
            var hasEnoughEnergy = GameManager.Instance.RedCurrentEnergy >= energyRequirements &&
                                  GameManager.Instance.BlueCurrentEnergy >= energyRequirements;
            if (hasEnoughEnergy == false) return;
            timeSlowFeedback.PlayFeedbacks();
            _redShield.ReduceEnergy(_redShield.Energy * 0.9f);
            _blueShield.ReduceEnergy(_blueShield.Energy * 0.9f);
            GameManager.Instance.SlowTime();
        }

        private void Start()
        {
            cameraObject = Camera.main;
            _destructible.LifePoints = lifePoints;
        }
        
        private void OnMainMenu(object sender, EventArgs e)
        {
            transform.position = transform.position.With(x: 0).With(y: 0);
            gameObject.SetActive(true);
        }
        
        private void OnContinueScreen(object sender, EventArgs e)
        {
            gameObject.SetActive(false);
        }
        
        private void WillKeepPlaying(object sender, EventArgs e)
        {
            ResetLifePoints();
            SetInvincible(true);
            gameObject.SetActive(true); 
            StartCoroutine(RevivePlayer(invincibilityTime));
        }

        private void ResetLifePoints()
        {
            _destructible.LifePoints = lifePoints;
        }

        private void SetInvincible(bool invincible)
        {
            _playerCollider.enabled = !invincible;
            _trailRenderer.enabled = !invincible;
        }
        
        private IEnumerator RevivePlayer(float seconds)
        {
            _isReviving = true; 
            SetInvincible(true);
            // add flashing animation we split it in 2 as we have 2 phases (blink and not blink)
            var flickerRate = seconds / (flickerDuration * 2); 
            for (int i = 0; i < flickerRate; i++)
            {
                _spriteRenderer.color = Color.clear;
                yield return new WaitForSeconds(flickerDuration);
                _spriteRenderer.color = _originalColor;
                yield return new WaitForSeconds(flickerDuration);
            }   
            
            SetInvincible(false);
            _isReviving = false;
            _isDead = false;
        }
        
        
        private void OnDestroy()
        {
            GameManager.Instance.OnContinueScreen -= OnContinueScreen;
            GameManager.Instance.OnContinuePlaying -= WillKeepPlaying;
            GameManager.Instance.OnMainMenu -= OnMainMenu;
            _inputReader.OnPowerUpActivated -= OnPowerUpActivated;
            GameManager.Instance.OnLifePointChanged -= OnLifePointsChanged;
        }

        public void HitFeedBack()
        {
            hitFeedback.PlayFeedbacks();
        }      
        
        public void AbsorbFeedBack()
        {
            absorbFeedback.PlayFeedbacks();
        }
    }
}
