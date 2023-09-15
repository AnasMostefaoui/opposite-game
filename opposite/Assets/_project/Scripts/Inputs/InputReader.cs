using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace OppositeGame
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputReader : MonoBehaviour
    {
        [SerializeField] public PlayerInput playerInput;
        private InputAction moveAction;

        public Vector2 GetMoveInput => moveAction.ReadValue<Vector2>();

        private void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            moveAction = playerInput.actions["Move"];
        }
    }
}