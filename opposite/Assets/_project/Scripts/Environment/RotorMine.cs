using System;
using UnityEngine;

namespace OppositeGame._project.Scripts.Environment
{
    public class RotorMine : MonoBehaviour
    {
        [SerializeField] public float rotationSpeed = 1f;
        [SerializeField] public float Damage = 9999;

        private void Update()
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
    }
}