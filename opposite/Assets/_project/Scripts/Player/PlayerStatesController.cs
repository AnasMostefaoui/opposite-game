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

        public bool isAlive => _destructible.LifePoints > 0;
        

        private void Awake()
        { 
            _playerCollider = GetComponent<CircleCollider2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _destructible ??= GetComponent<Destructible>();
        }

        private void Start()
        {
            _destructible.LifePoints = lifePoints;
            _camera ??= Camera.main;
        }

        private void Update()
        {
            Debug.Log("IsAlive: " + _destructible.LifePoints);
            if (GameManager.Instance.currentScreen == GameScreen.Game && !isAlive)
            {
                Debug.Log("Game Over");
                GameManager.Instance.ContinueRequest();
            }
        }
    }
}