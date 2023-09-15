using System;
using UnityEngine;

namespace OppositeGame
{
    public class ParallaxController : MonoBehaviour
    {
        [SerializeField] private Transform[] backgrounds;
        [SerializeField] private float parallaxSmoothing = 10f;
        [SerializeField] private float multiplier = 15f;
        
        private GameObject _camera;
        private Vector3 _previousCameraPosition;
        
        private void Awake()
        {
            if (_camera == null)
            {
                _camera = Camera.main.gameObject;
            }
        }
        private void Start()
        {
            _previousCameraPosition = _camera.transform.position;
        }

        private void Update()
        {
            var cameraPosition = _camera.transform.position;
            for (var i = 0; i < backgrounds.Length; i++)
            {
                // The array will move from the back to the front
                // The first background will be the slowest to move, the second background will move faster, and so on
               var background = backgrounds[i];
               var movementDelta = (_previousCameraPosition.y - cameraPosition.y) * (i * multiplier);
               var backgroundPosition = background.position;
               var verticalPosition = backgroundPosition.y + movementDelta;
               // set the new position of the background based on the camera movement, it's position in the array, the multiplier, and the smoothing factor
               var destination = new Vector3(backgroundPosition.x, verticalPosition, backgroundPosition.z);
               // interpolate the background's position to the new destination with time to avoid a jarring movement
               backgroundPosition = Vector3.Lerp(backgroundPosition, destination, parallaxSmoothing * Time.deltaTime);
               background.position = backgroundPosition;
            }
            // cache the camera's position for the next frame
            _previousCameraPosition = cameraPosition;
        }
    }
}