using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OppositeGame
{
    public interface IMovable
    {
        float getSpeed();
    }
    
    public class Move : MonoBehaviour
    {
        private Vector2 velocity =Vector2.zero; // Adjust the speed as needed

        private void Start()
        {
            var movable = GetComponent<IMovable>();
        }

        void Update()
        {
            // Get the current position of the object
            Vector3 currentPosition = transform.position;

            // Calculate the new position based on input or other conditions
            Vector3 newPosition = currentPosition + Vector3.right * moveSpeed * Time.deltaTime;

            // Update the object's position
            transform.position = newPosition;
        }
    }
}
