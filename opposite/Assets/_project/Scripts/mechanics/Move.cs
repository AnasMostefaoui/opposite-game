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
        public Vector2 velocity =Vector2.zero; // Adjust the speed as needed
        private Rigidbody2D rb;
        private void Start()
        {
            var movable = GetComponent<IMovable>();
            rb = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            // Calculate the rotation angle in radians
            float angleRad = Mathf.Atan2(velocity.y, velocity.x);
            rb.velocity = velocity;
            // Convert the angle to degrees
            float angleDeg = angleRad * Mathf.Rad2Deg * -1;

            
            // Rotate the object to face the velocity direction
            transform.rotation = Quaternion.Euler(0f, 0f, angleDeg);
        }
    }
}
