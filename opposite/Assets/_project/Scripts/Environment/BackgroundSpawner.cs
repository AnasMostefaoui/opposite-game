using System;
using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
using UnityEngine;
using UnityEngine.Serialization;

namespace OppositeGame._project.Scripts.Environment
{
    public class BackgroundSpawner : MonoBehaviour
    {
        [SerializeField][Range(-1, 1)] private float scrollSpeed = 0f;
        [SerializeField] private Camera mainCamera;
        [SerializeField] private BackgroundPool objectPool;
        private GameObject bg;
        private static readonly int MainTex = Shader.PropertyToID("_MainTex");
        private Material _backgroundMaterial;
        private float _offset = 0f;
        void Start()
        {
            mainCamera ??= Camera.main;
            bg = objectPool.GetBackground();
            bg.transform.SetParent(transform);
            bg.transform.position = transform.position;
            _backgroundMaterial = bg.GetComponent<SpriteRenderer>().material;
        }

        private void Update()
        {
            _offset += (Time.deltaTime * scrollSpeed) * 0.1f;
            Debug.Log("offset: " + _offset);
            _backgroundMaterial.SetTextureOffset(MainTex, new Vector2(0, _offset));
        }
    }
}