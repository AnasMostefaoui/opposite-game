using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OppositeGame
{
    public class LaserRenderer : MonoBehaviour
    {
        private LineRenderer lineRenderer;
        public SpriteMaskInteraction playerMask;

        private void Start()
        {
            lineRenderer =  GetComponent<LineRenderer>();
            var component =  GameObject.FindGameObjectWithTag("player-shield-mask");
            if (component)
            {
                playerMask = component.GetComponent<SpriteMaskInteraction>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            lineRenderer.maskInteraction = playerMask;
        }
    }
}
