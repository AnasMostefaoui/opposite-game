using System;
using System.Collections;
using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.Managers;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using Unity.VisualScripting;
using UnityEngine;

namespace OppositeGame._project.Scripts.Player
{
    public enum ShieldState
    {
        Inactive, Active, Depleted, Recharging
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

        [SerializeField] private PolarityType polarity;
        
        public Action OnShieldActivated;
        public Action OnShieldDeactivated;
        public bool isShieldActive => _shieldState == ShieldState.Active && _activationIsRequested;
        public PolarityType test => polarity;
        private PlayerPolarity _playerPolarity;
        private SpriteRenderer _maskSpriteRenderer;
        private TrailRenderer _trailRenderer;
        private InputReader _inputReader;
        private ShieldState _shieldState = ShieldState.Inactive; 
        private GameObject _shieldMaskObject;
        // flag to know that the player is still pressing the buttons
        private bool _activationIsRequested = false;
        private Color ShieldColor  {
            get
            {
                var color = Color.red;;
                if (polarity == PolarityType.Blue)
                {
                    color = new Color(34f / 255f, 168f / 255f, 204f / 255f, shieldTransparency);
                    return color;
                }
                color.a = shieldTransparency;
                return color;
            }
        }
        
        private bool CanEnableShield => energy > 0;
        
        public float Energy => energy;
        public void ReduceEnergy(float amount)
        {
            energy -= amount;
            energy = Mathf.Clamp(energy, 0, maxEnergy);
            GameManager.Instance.UpdateRedEnergy(energy/maxEnergy, polarity);
        }
        
        private void Awake()
        {
            var tagName = polarity == PolarityType.Blue ? "blue-player-shield-mask" : "red-player-shield-mask";
  
            _shieldMaskObject = GameObject.FindWithTag(tagName);
            _maskSpriteRenderer = _shieldMaskObject.GetComponent<SpriteRenderer>();
            _playerPolarity = GetComponent<PlayerPolarity>();
            _trailRenderer = GetComponentInChildren<TrailRenderer>();
            _inputReader = GetComponent<InputReader>();
            _inputReader.OnShieldActivated += ActivateShield;
            _inputReader.OnShieldDeactivated += DeactivateShield;
            _maskSpriteRenderer.color = ShieldColor;
            _shieldMaskObject.SetActive(false);
        }

        private void ActivateShield(PolarityType obj)
        {
            if(obj != polarity) return;
            _activationIsRequested = true;
            _playerPolarity.PolarityType = obj;
            
            if(!CanEnableShield) return;
            _shieldState = ShieldState.Active;
            _shieldMaskObject.SetActive(true);
            OnShieldActivated?.Invoke();
        }

        private void DeactivateShield(PolarityType obj)
        {
            if(energy < maxEnergy)
                _shieldState = ShieldState.Recharging;
            else 
                _shieldState = ShieldState.Inactive;
            
            if(_shieldMaskObject.activeSelf == false) return;
            _activationIsRequested = false;
            _shieldMaskObject.SetActive(false);
            OnShieldDeactivated?.Invoke();
        }
        
        private void Update()
        {
            switch (_shieldState)   
            { 
                case ShieldState.Active when energy > 0:
                    energy -= energyConsumptionRate * Time.deltaTime;
                    GameManager.Instance.UpdateRedEnergy(energy/maxEnergy, polarity);
                    break;
                case ShieldState.Active when energy <= 0:
                    _shieldState = ShieldState.Depleted;
                    break;
                case ShieldState.Depleted:
                    DeactivateShield(polarity);
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
            GameManager.Instance.UpdateRedEnergy(energy/maxEnergy, polarity);
        }
        
        private void OnDestroy()
        {
            _inputReader.OnShieldActivated -= ActivateShield;
            _inputReader.OnShieldDeactivated -= DeactivateShield;
        }
    }
}