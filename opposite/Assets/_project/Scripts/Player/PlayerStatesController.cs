using System;
using System.Collections;
using OppositeGame._project.Scripts.Managers;
using OppositeGame._project.Scripts.mechanics;
using OppositeGame._project.Scripts.mechanics.weapons;
using OppositeGame._project.Scripts.ScriptablesObjects.Weapons;
using Unity.VisualScripting;
using UnityEngine;

namespace OppositeGame._project.Scripts.Player
{
    public class WeaponPowerUp
    {
        
    }
    
    [RequireComponent(typeof(Destructible))]
    public class PlayerStatesController  : MonoBehaviour
    {
        [SerializeField] private float lifePoints = 5f;
        [SerializeField] private float invincibilityTime = 3f;
        [Range(0.01f, 0.2f)]
        [SerializeField] private float flickerDuration = 0.01f;

        private Destructible _destructible;
        private float _currentLifePoints;
        private Camera _camera;
        private bool _isReviving;
        private CircleCollider2D _playerCollider;
        private SpriteRenderer _spriteRenderer;
        private TrailRenderer _trailRenderer;
        private Color _originalColor;

        public float LifePoints
        {
            set => _destructible.LifePoints = value;
            get => _destructible.LifePoints;
        }

        private void Awake()
        { 
            _playerCollider = GetComponent<CircleCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
            _originalColor = _spriteRenderer.color;
        }

        private void Start()
        {
            _destructible ??= GetComponent<Destructible>();
            _destructible.LifePoints = lifePoints;
            _camera ??= Camera.main;
            GameManager.Instance.OnContinueScreen += OnContinueScreen;
            GameManager.Instance.OnContinuePlaying += WillKeepPlaying;
            GameManager.Instance.OnMainMenu += OnMainMenu;
        }

        private void OnMainMenu(object sender, EventArgs e)
        {
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