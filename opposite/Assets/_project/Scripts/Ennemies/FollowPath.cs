using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OppositeGame
{
    public class FollowPath : MonoBehaviour
    {
        
        [SerializeField] public Vector3[] waypoints;
        [SerializeField] private float speed = 1f;
        [SerializeField] private float rotationSpeed = 1f;
        [SerializeField] public bool shouldLoop = false;
        
        private int _nextWaypointIndex = 0;
        private GameObject _target;
        
        private void Start()
        {
            if (waypoints == null || waypoints.Length == 0)
            {
                return;
            }
            
            transform.position = waypoints[0];
            _nextWaypointIndex = waypoints.Length > 1 ? 1 : 0;
            
            _target = GameObject.FindGameObjectWithTag("Player");
            Vector3 targetDirection = _target.transform.position - transform.position;
            targetDirection.z = 0; // Ensure only Z rotation is affected
            transform.rotation = Quaternion.LookRotation(Vector3.forward, targetDirection.normalized);
        }
        
        private void Update()
        {
            if (_nextWaypointIndex >= waypoints.Length && shouldLoop == false)
            {
                return;
            }
           
            if(_nextWaypointIndex >= waypoints.Length && shouldLoop)
            {
                _nextWaypointIndex = 0;
            }
            
            var currentPosition = transform.position;
            var targetPosition = waypoints[_nextWaypointIndex];
            var movementThisFrame = speed * Time.deltaTime;
            //transform.position = Vector3.MoveTowards(currentPosition, targetPosition, movementThisFrame);
            
            targetPosition.z = 0;
            
            Vector2 targetDirection = (targetPosition - transform.position).normalized;
            float targetRotation = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
            
            // Calculate the interpolated rotation
            float currentRotation = transform.rotation.eulerAngles.z;
            float newRotation = Mathf.LerpAngle(currentRotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Apply the new rotation to your object's transform
            transform.rotation = Quaternion.Euler(0f, 0f, newRotation);
            
            if (transform.position == targetPosition)
            {
                _nextWaypointIndex += 1;
            }
        }
    }
}
