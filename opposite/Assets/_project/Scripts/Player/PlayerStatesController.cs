using System;
using System.Collections;
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

        private Destructible _destructible;
        private float _currentLifePoints;
        private Camera _camera;
        private bool _isReviving;

        public float LifePoints
        {
            set => _destructible.LifePoints = value;
            get => _destructible.LifePoints;
        }
        
        private void Start()
        {
            _destructible ??= GetComponent<Destructible>();
            _destructible.LifePoints = lifePoints;
            _camera ??= Camera.main;
            GameManager.Instance.OnContinueScreen += OnContinueScreen;
            GameManager.Instance.OnContinuePlaying += WillKeepPlaying;
        }

                
        private void OnContinueScreen(object sender, EventArgs e)
        {
            gameObject.SetActive(false);
        }

                
        private void WillKeepPlaying(object sender, EventArgs e)
        {
            gameObject.SetActive(true);
            var playerCollider = GetComponent<CircleCollider2D>();
            var spriteRenderer = GetComponent<SpriteRenderer>();
            if (_isReviving == false)
            {
                StartCoroutine(RevivePlayer(10, playerCollider, spriteRenderer));
            } 
        }

        private IEnumerator RevivePlayer(float seconds, Behaviour playerCollider, SpriteRenderer spriteRenderer)
        {
            _isReviving = true;
            // Disable the Collider
            playerCollider.enabled = false;
            // add flashing animation
            
            // reduce alpha channel
            float alpha = Mathf.PingPong(Time.time, 1f);
            spriteRenderer.color = spriteRenderer.color.WithAlpha(alpha);
            // Wait for the specified duration
            yield return new WaitForSeconds(seconds);

            // Enable the Collider after the duration has passed
            playerCollider.enabled = true;
            spriteRenderer.color = spriteRenderer.color.WithAlpha(1);
            _isReviving = false;
        }

        private void Update()
        {
            
            var spriteRenderer = GetComponent<SpriteRenderer>();
            float alpha = Mathf.PingPong(Time.time, 1.0f); // PingPong between 0 and 1
             spriteRenderer.color = spriteRenderer.color.WithAlpha(alpha);
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnContinueScreen -= OnContinueScreen;
            GameManager.Instance.OnContinuePlaying -= WillKeepPlaying;
        }
    }
}