using System;
using System.Collections;
using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.Managers;
using OppositeGame._project.Scripts.mechanics;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    { 
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
        private InputReader _inputReader;
        private Animator _animationController;
        private Color _originalColor;
        private bool _isReviving;

        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _animationController = GetComponent<Animator>();
            _playerCollider = GetComponent<CircleCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
            _destructible ??= GetComponent<Destructible>();
            _movementController = GetComponentInChildren<PlayerMovementController>();
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
            Debug.Log("Reviving player");
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
            WillKeepPlaying(this, EventArgs.Empty);
        }
        
        private void OnPowerUpActivated()
        {
            var hasEnoughEnergy = GameManager.Instance.RedCurrentEnergy >= 1f && GameManager.Instance.BlueCurrentEnergy >= 1f;
            if (hasEnoughEnergy == false) return;
            GameManager.Instance.SlowTime();
        }

        private void Start()
        {
            cameraObject ??= Camera.main;
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
        }
        
        private void OnDestroy()
        {
            GameManager.Instance.OnContinueScreen -= OnContinueScreen;
            GameManager.Instance.OnContinuePlaying -= WillKeepPlaying;
            GameManager.Instance.OnMainMenu -= OnMainMenu;
            _inputReader.OnPowerUpActivated -= OnPowerUpActivated;
            GameManager.Instance.OnLifePointChanged -= OnLifePointsChanged;
        }
    }
}
