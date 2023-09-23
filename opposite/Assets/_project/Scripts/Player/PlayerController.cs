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
            _originalColor = _spriteRenderer.color;
            
            GameManager.Instance.OnContinueScreen += OnContinueScreen;
            GameManager.Instance.OnContinuePlaying += WillKeepPlaying;
            GameManager.Instance.OnMainMenu += OnMainMenu;
        }

        private void Start()
        {
            cameraObject ??= Camera.main;
            _destructible.LifePoints = lifePoints;
        }
        
        private void Update()
        {
            if (GameManager.Instance.currentScreen == GameScreen.Game && !IsAlive)
            {
                Debug.Log("Game Over");
                GameManager.Instance.ContinueRequest();
            }
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
            SetInvincible(true);
            gameObject.SetActive(true); 
            StartCoroutine(RevivePlayer(invincibilityTime));
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
            // add flashing animation
            var flickerRate = seconds / flickerDuration; 
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
        }
    }
}
