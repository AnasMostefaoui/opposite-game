using System;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

namespace OppositeGame._project.Scripts.Inputs
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour
    {
        [SerializeField] public PlayerInput playerInput;
        
        public Action<PolarityType> OnPolarityChanged;
        public Action<PolarityType> OnShieldActivated;
        public Action OnShieldDeactivated;
        
        private InputAction _moveAction;
        private InputAction _fireAction;
        private InputAction _menuAction;
        private InputAction _redPolarityAction;
        private InputAction _bluePolarityAction;

        public Vector2 GetMoveInput => _moveAction.ReadValue<Vector2>();
        public bool IsFiring => _fireAction.ReadValue<float>() > 0f;
        

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerInput.SwitchCurrentActionMap("Player");
            _moveAction = playerInput.actions["Move"];
            _fireAction = playerInput.actions["Fire"]; 
            _redPolarityAction = playerInput.actions["red-polarity"]; 
            _bluePolarityAction = playerInput.actions["blue-polarity"];
            
            _redPolarityAction.performed += OnReadPolarityAction;
            _bluePolarityAction.performed += OnReadPolarityAction;
            _redPolarityAction.canceled += OnPolarityActionCancelled;
        }

        private void OnPolarityActionCancelled(InputAction.CallbackContext obj)
        {
            switch (obj.interaction)
            {
                case HoldInteraction:
                    OnShieldDeactivated?.Invoke();
                    break;
            }
        }

        private void OnReadPolarityAction(InputAction.CallbackContext obj)
        {
            var polarityType = obj.action.name.Contains("red") ? PolarityType.Red : PolarityType.Blue;
            switch (obj.interaction)
            {
                case HoldInteraction:
                    OnShieldActivated?.Invoke(polarityType);
                    break;
                case TapInteraction:
                    OnPolarityChanged?.Invoke(polarityType);
                    break;
            }
        }

        private void Update()
        {
            if (playerInput == null)
            {
                playerInput = GetComponent<PlayerInput>();
                playerInput.SwitchCurrentActionMap("Player");
            }
            if(_moveAction == null) _moveAction = playerInput.actions["Move"];
            if(_fireAction == null) _fireAction = playerInput.actions["Fire"];
        }
    }
}