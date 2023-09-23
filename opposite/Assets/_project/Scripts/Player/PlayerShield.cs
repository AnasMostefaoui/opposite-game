using System;
using System.Collections;
using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using Unity.VisualScripting;
using UnityEngine;

namespace OppositeGame._project.Scripts.Player
{
    public enum ShieldState
    {
        Active, Inactive, Depleted, Recharging
    }
    public class PlayerShield : MonoBehaviour
    {
        [Range(0f, 1f)]
        [SerializeField] private float shieldTransparency = 0.3f;
        // How many energy points the shield has
        [Range(0, 100)]
        [SerializeField] private float energy = 10;
        
        [Range(0, 100)]
        [SerializeField] private float maxEnergy = 10;
        // How many energy points the shield consumes per second
        [Range(0, 100)]
        [SerializeField] private float energyConsumptionRate = 2f;
        // How many energy points the shield recovers per second
        [Range(0, 100)]
        [SerializeField] private float energyRecoveryRate = 1f;
        
        public Action OnShieldActivated;
        public Action OnShieldDeactivated;        
        public bool isShieldActive => _shieldState == ShieldState.Active; 


        private ShieldState _shieldState = ShieldState.Active; 
        private PolarityType _currentPolarityType = PolarityType.Blue;
        private PlayerPolarity _playerPolarity;
        private SpriteRenderer _maskSpriteRenderer;
        private TrailRenderer _trailRenderer;
        private InputReader _inputReader;
        private GameObject _shieldMaskObject;
        // flag to know that the player is still pressing the buttons
        private bool _activationIsRequested = false;
        private Color RedShieldColor
        {
            get
            {
                var color = Color.red;
                    color.a = shieldTransparency;
                return color;
            }
        }

        private Color BlueShieldColor => new Color(34f / 255f, 168f / 255f, 204f / 255f, shieldTransparency);
        
        private bool CanEnableShield => energy > 0;
        
        private void Awake()
        {
            _shieldMaskObject = GameObject.FindWithTag("player-shield-mask");
            _playerPolarity = GetComponent<PlayerPolarity>();
            _maskSpriteRenderer = _shieldMaskObject.GetComponent<SpriteRenderer>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
            _inputReader = GetComponent<InputReader>();
            _inputReader.OnShieldActivated += ActivateShield;
            _inputReader.OnShieldDeactivated += DeactivateShield;
            _playerPolarity.OnPolarityChanged += OnPolarityChanged;
            _shieldMaskObject.SetActive(false);
        }

        private void ActivateShield(PolarityType obj)
        {
            _activationIsRequested = true;
            _playerPolarity.PolarityType = obj;
            
            if(!CanEnableShield) return;
            _shieldState = ShieldState.Active;
            OnPolarityChanged(obj, _playerPolarity.PolarityType);
            _shieldMaskObject.SetActive(true);
            OnShieldActivated?.Invoke();
        }

        private void DeactivateShield()
        {
            _shieldState = ShieldState.Recharging;
            if(_shieldMaskObject.activeSelf == false) return;
            _activationIsRequested = false;
            _shieldMaskObject.SetActive(false);
            OnShieldDeactivated?.Invoke();
        }
        
        private void Update()
        {
            _maskSpriteRenderer.color = _currentPolarityType == PolarityType.Red ? RedShieldColor : BlueShieldColor;
            switch (_shieldState)   
            { 
                case ShieldState.Active when energy > 0:
                    energy -= energyConsumptionRate * Time.deltaTime;
                    break;
                case ShieldState.Active when energy <= 0:
                    _shieldState = ShieldState.Depleted;
                    break;
                case ShieldState.Depleted:
                    DeactivateShield();
                    break;
                case ShieldState.Recharging:
                    Refill();
                    break;
            }
        }
        
        private void Refill()
        {
            energy += energyRecoveryRate * Time.deltaTime;
            energy = Math.Clamp(energy, 0, maxEnergy);
                
        }
        private void OnPolarityChanged(PolarityType newPolarityType, PolarityType oldPolarityType)
        {
            _currentPolarityType = newPolarityType;
            var polarityColor = newPolarityType == PolarityType.Red ? RedShieldColor : BlueShieldColor;
            _maskSpriteRenderer.color = polarityColor;
            _trailRenderer.startColor = polarityColor;
            _trailRenderer.endColor = polarityColor;
        }

        private void OnDestroy()
        {
            _inputReader.OnShieldActivated -= ActivateShield;
            _inputReader.OnShieldDeactivated -= DeactivateShield;
            _playerPolarity.OnPolarityChanged -= OnPolarityChanged;
        }
    }
}