using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using Unity.VisualScripting;
using UnityEngine;

namespace OppositeGame._project.Scripts.Player
{
    public class PlayerPolarity : PolarityProvider
    {
        private Animator _animationController;
        private InputReader _inputReader;
        private GameObject _playerShield;
        private SpriteRenderer _spriteRenderer;
        private TrailRenderer _trailRenderer;
        
        private readonly Color redShieldColor = Color.red.WithAlpha(0.5f);
        private readonly Color blueShieldColor = Color.blue.WithAlpha(0.5f);
         
        private void Awake()
        {
            _inputReader = GetComponent<InputReader>();
            _animationController = GetComponent<Animator>();
            _playerShield = GameObject.FindWithTag("player-shield-mask");
            _spriteRenderer = _playerShield.GetComponent<SpriteRenderer>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
            OnPolarityChanged += OnOnPolarityChanged;
        }

        private void OnOnPolarityChanged(PolarityType newPolarityType, PolarityType oldPolarityType)
        {
            _animationController.SetBool("isRed", PolarityType == PolarityType.Red);
            _animationController.SetBool("isBlue", PolarityType == PolarityType.Blue);
            var polarityColor = PolarityType == PolarityType.Red ? redShieldColor : blueShieldColor;
            _spriteRenderer.color = polarityColor;
            _trailRenderer.startColor = polarityColor;
            _trailRenderer.endColor = polarityColor;
        }

        private void OnDestroy()
        {
            OnPolarityChanged -= OnOnPolarityChanged;
        }
    }
}