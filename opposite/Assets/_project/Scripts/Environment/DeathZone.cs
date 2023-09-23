using System;
using OppositeGame._project.Scripts.Managers;
using UnityEngine;

namespace OppositeGame._project.Scripts.Environment
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.Instance.HasNoLifePoints = true;
            }
        }
    }
}