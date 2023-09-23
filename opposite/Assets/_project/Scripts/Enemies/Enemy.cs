using System;
using OppositeGame._project.Scripts.Managers;
using OppositeGame._project.Scripts.mechanics.Scoring;
using OppositeGame._project.Scripts.ScriptablesObjects;
using UnityEngine;

namespace OppositeGame._project.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public EnemyType EnemyType;
        private ScorableBehavior _scoreBehavior;
        
        private void Awake()
        {
            _scoreBehavior = GetComponent<ScorableBehavior>();
        }

        private void OnDisable()
        {
            if(_scoreBehavior == null) return;
            GameManager.Instance.CurrentScore += _scoreBehavior.ScoreValue;
        }
        
    }
}