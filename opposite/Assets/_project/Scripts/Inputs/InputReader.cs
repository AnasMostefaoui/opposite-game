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

        public Vector2 GetMoveInput => _moveAction.ReadValue<Vector2>();
        public bool IsFiring => _fireAction.ReadValue<float>() > 0f;

        private void Start()
        {
            playerInput = GetComponent<PlayerInput>();
            _moveAction = playerInput.actions["Move"];
            _fireAction = playerInput.actions["Fire"];
        }
    }
}