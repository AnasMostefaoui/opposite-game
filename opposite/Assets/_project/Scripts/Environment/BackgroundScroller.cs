using System;
using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
using UnityEngine;
using UnityEngine.Serialization;

namespace OppositeGame._project.Scripts.Environment
{
    public class BackgroundScroller : MonoBehaviour
    {
        [SerializeField][Range(-1, 1)] private float scrollSpeed = 0f; 
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
            _offset += (Time.deltaTime * scrollSpeed) * 0.1f;
            _backgroundMaterial.SetTextureOffset(MainTex, new Vector2(0, _offset));
        }
    }
}