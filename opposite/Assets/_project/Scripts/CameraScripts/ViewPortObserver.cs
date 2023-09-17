using System;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.CameraScripts
{
    public class ViewPortObserver: MonoBehaviour
    {
        public Action OnEnteredViewport;
        public Action OnLeftViewport;
        
        
        private bool _isInViewport;
        private bool _hasEnteredViewport;
        private Camera _camera;
        [SerializeField] private float safeZone = 0f;


        private void Start()
        {
            _camera = Camera.main;
        }
        
        private void FixedUpdate()
        {
            var transformPosition = transform.position + new Vector3(0, safeZone, 0);
            _isInViewport = _camera.IsPointInViewport(transformPosition);

            if (_hasEnteredViewport == false && _isInViewport)
            {
                _hasEnteredViewport = true;
                ObjectEnteredViewport();
            }
            
           
            if (_isInViewport == false && _hasEnteredViewport == true)
            {
                ObjectLeftViewport();
            }
        }
        
        // This can be an event delegate to allow other scripts to hook function when an object enter screen
        private void ObjectEnteredViewport()
        {
            OnEnteredViewport?.Invoke();
        }

        private void ObjectLeftViewport()
        {
            OnLeftViewport?.Invoke();
        }
    }
}