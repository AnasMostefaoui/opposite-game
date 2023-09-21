using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OppositeGame._project.Scripts.Inputs
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour
    {
        [SerializeField] public PlayerInput playerInput;
        private InputAction _moveAction;
        private InputAction _fireAction;
        private InputAction _menuAction;

        public Vector2 GetMoveInput => _moveAction.ReadValue<Vector2>();
        public bool IsFiring => _fireAction.ReadValue<float>() > 0f;
        

        private void Awake()
        {
            playerInput = GetComponent<PlayerInput>();
            playerInput.SwitchCurrentActionMap("Player");
            _moveAction = playerInput.actions["Move"];
            _fireAction = playerInput.actions["Fire"]; 
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