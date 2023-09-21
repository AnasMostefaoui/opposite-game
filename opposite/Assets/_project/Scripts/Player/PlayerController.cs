using System;
using OppositeGame._project.Scripts.Inputs;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    { 
        [SerializeField] private float speed = 5f;
        // This is a shoot them up game, having precise control up to the pixel perfect movement is critical for the game play
        // if the smoothness is low, the character will slide to destination, if the smoothness is high, the character will stop immediately
        // We need to find sweet spot for the smoothness that works for our game, right now 10f is a good number
        [SerializeField] private float smoothness = 10f;
        [SerializeField] private GameObject player;
        
        [Header("Camera settings")]
        [SerializeField] private Camera cameraObject;
        // The padding is used to prevent the player from going off screen
        // these values work best with the current player size. setting it less than that will cause the player sprite to be clamped to the edge of the screen
        [Range(0.03f, 1f)] [SerializeField] private float horizontalPadding = 0.03f;
        [Range(0.08f, 1f)] [SerializeField] private float verticalPadding = 0.08f;

        private InputReader _inputReader;
        private Vector3 _destination;

        private void Start()
        {
            _inputReader = GetComponent<InputReader>();
            if (cameraObject == null)
            {
                cameraObject = Camera.main;
            }
            _destination = transform.position;
            GameManager.Instance.OnMainMenu += OnMainMenu;
        }

        private void OnMainMenu(object sender, EventArgs e)
        {
            transform.position = transform.position.With(x: 0).With(y: 0);
        }

        private void Update()
        {
            var deltaTime = GameManager.Instance.IsPaused ? Time.deltaTime : Time.unscaledDeltaTime;
            // calculate the new position based on the input
            _destination += new Vector3(_inputReader.GetMoveInput.x, _inputReader.GetMoveInput.y, 0) * (speed * deltaTime);

            if(_inputReader.GetMoveInput.x > 0 )
            Debug.Log("Input X "+ _inputReader.GetMoveInput.x);
            if(_inputReader.GetMoveInput.y > 0 )
            Debug.Log("Input Y " + _inputReader.GetMoveInput.y);
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

        private void OnDestroy()
        {
            GameManager.Instance.OnMainMenu -= OnMainMenu;
        }
    }
}
