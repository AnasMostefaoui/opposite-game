using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace OppositeGame._project.Scripts.Environment
{
    public class Asteroid: MonoBehaviour
    {
        public float Damage = 9999;
        public float rotationSpeed = 0;
        public float translationSpeed = 1;
        public Vector3 direction = Vector3.zero;
        private void OnEnable()
        {
            rotationSpeed = UnityEngine.Random.Range(-10, 10);
            translationSpeed = UnityEngine.Random.Range(0.01f, 0.02f);
            direction = Random.insideUnitCircle;
        }

        private void Update()
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            transform.position += direction * (translationSpeed * Time.deltaTime);
            
        }
    }
}