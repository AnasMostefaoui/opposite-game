using System;
using OppositeGame._project.Scripts.CameraScripts;
using OppositeGame._project.Scripts.Patterns;
using UnityEngine;

namespace OppositeGame._project.Scripts.Environment
{
    [RequireComponent(typeof(ViewPortObserver))]
    public class BackgroundObject : MonoBehaviour, IPoolable<GameObject>
    {
        public Action<GameObject> OnRelease { get; set; }

        private void OnDisable()
        {
            OnRelease?.Invoke(gameObject);
        }
    }
}