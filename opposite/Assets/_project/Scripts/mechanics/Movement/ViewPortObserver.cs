using System;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.Movement
{
    public class ViewPortObserver: MonoBehaviour
    {
        public Action OnEnteredViewport;
        public Action OnLeftViewport;
        
        
        private bool _isInViewport;
        private bool _hasEnteredViewport;
        private Camera _camera;
        private const float SafeZone = 1f;


        private void Start()
        {
            _camera = Camera.main;
        }
        
        private void FixedUpdate()
        {
            if (_isInViewport)
            {
                
            }
            var transformPosition = transform.position + new Vector3(0, SafeZone, 0);
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