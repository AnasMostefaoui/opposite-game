using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace OppositeGame._project.Scripts.Environment
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField][Range(-1, 1)] private float scrollSpeed = 0f;
        [SerializeField] private  float currentSpeed = 0;
        [SerializeField]public float speedModifier = 1;
        [SerializeField]public float accelerationRate = 0.05f;
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private Material _backgroundMaterial;
        private float _offset = 0f;
        private Camera _mainCamera;
        
        void Start()
        {
            _mainCamera ??= Camera.main;
            _backgroundMaterial = GetComponent<SpriteRenderer>().material;
        }

        private void Update()
        {
            
            currentSpeed = Mathf.MoveTowards(currentSpeed, 
                scrollSpeed * speedModifier, 
                // once at max speed don't accelerate any more.
                accelerationRate * Time.deltaTime);
            // Update the texture offset using the lerped speed.
            _offset += (Time.deltaTime * currentSpeed) * 0.1f;
            _backgroundMaterial.SetTextureOffset(MainTex, new Vector2(0, _offset));
        }
    }
}