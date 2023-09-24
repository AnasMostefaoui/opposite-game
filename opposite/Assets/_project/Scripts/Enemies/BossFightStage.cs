using System;
using System.Collections.Generic;
using UnityEngine;

namespace OppositeGame._project.Scripts.Enemies
{
    public class BossFightStage : MonoBehaviour
    {
        [SerializeField] public List<Enemy> enemies;
        public bool IsCompleted => enemies.TrueForAll(enemy => enemy == null || enemy.CurrentHealth <= 0);
        public bool isVulnerable = true;
        private void Start()
        {
            enemies.ForEach( enemy => enemy.gameObject.SetActive(false) );
        }

        public void SetupStage()
        {
            enemies.ForEach( enemy => enemy.gameObject.SetActive(true) );
        }

        public void PlayStage()
        {
            
        }
    }
}