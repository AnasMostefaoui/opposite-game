using System;
using OppositeGame._project.Scripts.CameraScripts;
using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.Managers;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private float speed = 5f;
        // This is a shoot them up game, having precise control up to the pixel perfect movement is critical for the game play
        // if the smoothness is low, the character will slide to destination, if the smoothness is high, the character will stop immediately
        // We need to find sweet spot for the smoothness that works for our game, right now 10f is a good number
        [SerializeField] private float smoothness = 10f;
        
        [Header("Camera settings")]
        [SerializeField] private Camera cameraObject;
        // The padding is used to prevent the player from going off screen
        // these values work best with the current player size. setting it less than that will cause the player sprite to be clamped to the edge of the screen
        [Range(0.03f, 1f)] [SerializeField] private float horizontalPadding = 0.03f;
        [Range(0.08f, 1f)] [SerializeField] private float verticalPadding = 0.08f;

        private Animator _animationController;
        private CameraController _cameraController;
        private PlayerPolarity _playerPolarity;
        private InputReader _inputReader;
        private Vector3 _destination;
        private Vector3 _force;

        private void Awake()
        {
            cameraObject = Camera.main;
            _inputReader = GetComponent<InputReader>();
            _destination = transform.position;
            _animationController = GetComponent<Animator>();
            _playerPolarity = GetComponent<PlayerPolarity>();
            _cameraController = cameraObject.GetComponent<CameraController>();
            GameManager.Instance.OnMainMenu += OnMainMenu;
        }

        private void OnMainMenu(object sender, EventArgs e)
        {
            transform.position = transform.position.With(x: 0).With(y: 0);
        }

        public void ResetPosition(Vector3 newPosition)
        {
            _destination = newPosition;
        }
        private void Update()
        {
            var deltaTime = GameManager.Instance.IsPaused ? Time.deltaTime : Time.unscaledDeltaTime;
            // calculate the new position based on the input
            _destination += new Vector3(_inputReader.GetMoveInput.x, _inputReader.GetMoveInput.y, 0) * (speed * deltaTime);
            _destination = _destination.With(y: _destination.y + _cameraController.CurrentSpeed * deltaTime);
            _animationController.SetBool("isMovingLeft", _inputReader.GetMoveInput.x < 0);
            _animationController.SetBool("isMovingRight", _inputReader.GetMoveInput.x > 0);

            // add force to destination
            _destination += _force * deltaTime;
            // limit the movement to viewport
            // first, convert the destination world position to viewport position 
            var viewportPosition = cameraObject.WorldToViewportPoint(_destination);
            // Clamp the position inside the camera's rect with padding.
            // The view port is a normalized value 0-1, the clamping should be done with normalized value
            viewportPosition.x = Mathf.Clamp(viewportPosition.x, horizontalPadding, 1 - horizontalPadding);
            viewportPosition.y = Mathf.Clamp(viewportPosition.y, verticalPadding, 1 - verticalPadding);
            
            // Update the object's position.
            _destination = cameraObject.ViewportToWorldPoint(viewportPosition);
            
            // smooth the movement
            transform.position = Vector2.Lerp(transform.position, _destination, smoothness * deltaTime);
        }
        
        private void OnTriggerStay2D(Collider2D other)
        {
            // if the player is inside the area effect, apply force to the player based on it's shield polarity
            if (other.gameObject.layer == LayerMask.NameToLayer("Area-effects"))
            {
                var polarityProvider = other.GetComponent<PolarityProvider>();
                var direction = (transform.position - other.transform.position).normalized;
                var samePolarity = _playerPolarity.PolarityType == polarityProvider.PolarityType;
                
                var isShieldOn = polarityProvider.PolarityType == PolarityType.Blue ? 
                    GameManager.Instance.IsBlueShieldOn : GameManager.Instance.IsRedShieldOn;
                // if we have similar polarity repulse, otherwise attract
                var force = isShieldOn && samePolarity ? 2f : -2f;
                _force = direction * force;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Area-effects"))
            {
                _force = Vector2.zero;
                return;
            }
            _force = Vector2.zero;
        }

        private void OnDestroy()
        {
            GameManager.Instance.OnMainMenu -= OnMainMenu;
        }
    }
}