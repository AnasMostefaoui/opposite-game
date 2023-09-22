using System;
using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using Unity.VisualScripting;
using UnityEngine;

namespace OppositeGame._project.Scripts.Player
{
    public class PlayerShield : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float shieldTransparency = 0.3f;
        // How many energy points the shield has
        [Range(0, 100)]
        [SerializeField] private float energy = 10;
        // How many energy points the shield consumes per second
        [Range(0, 100)]
        [SerializeField] private float energyConsumptionRate = 2f;
        // How many energy points the shield recovers per second
        [Range(0, 100)]
        [SerializeField] private float energyRecoveryRate = 1f;
        // Indicate how long the player should press the button to activate the shield
        [SerializeField] private float activationTime = 1f; 
        
        private PolarityType _currentPolarityType = PolarityType.Blue;
        private PlayerPolarity _playerPolarity;
        private SpriteRenderer _maskSpriteRenderer;
        private TrailRenderer _trailRenderer;
        private InputReader _inputReader;
        private GameObject _shieldMaskObject;

        private Color RedShieldColor => Color.red.WithAlpha(shieldTransparency);
        private Color BlueShieldColor => new Color(34f / 255f, 168f / 255f, 204f / 255f).WithAlpha(shieldTransparency);
        
        private void Awake()
        {
            _shieldMaskObject = GameObject.FindWithTag("player-shield-mask");
            _playerPolarity = GetComponent<PlayerPolarity>();
            _maskSpriteRenderer = _shieldMaskObject.GetComponent<SpriteRenderer>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
            _inputReader = GetComponent<InputReader>();
            _playerPolarity.OnPolarityChanged += OnPolarityChanged;
            OnPolarityChanged(_playerPolarity.PolarityType, _playerPolarity.PolarityType);
        }

        private void Update()
        {
            _maskSpriteRenderer.color = _currentPolarityType == PolarityType.Red ? RedShieldColor : BlueShieldColor;
        }

        private void OnPolarityChanged(PolarityType newPolarityType, PolarityType oldPolarityType)
        {
            _currentPolarityType = newPolarityType;
            var polarityColor = newPolarityType == PolarityType.Red ? RedShieldColor : BlueShieldColor;
            _maskSpriteRenderer.color = polarityColor;
            _trailRenderer.startColor = polarityColor;
            _trailRenderer.endColor = polarityColor;
        }
    }
}