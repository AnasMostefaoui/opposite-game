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
            rb.velocity = velocity;
        }
    }
}
