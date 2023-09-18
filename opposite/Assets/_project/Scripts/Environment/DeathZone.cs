using System;
using UnityEngine;

namespace OppositeGame._project.Scripts.Environment
{
    public class DeathZone : MonoBehaviour
    {
        private void Awake()
        {
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.IsGameOver = true;
            }
        }
    }
}