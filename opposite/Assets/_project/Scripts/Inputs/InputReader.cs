using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OppositeGame
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour
    {
        public PlayerInput playerInput;
        public InputAction moveAction;

        public Vector2 GetMoveInput => moveAction.ReadValue<Vector2>();

        private void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
        }
        
        private void OnEnable()
        {
            moveAction.Enable();
        }
    }
}