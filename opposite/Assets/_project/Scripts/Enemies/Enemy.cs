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
            var scoreMultiplier = GameManager.Instance.CurrentScore > 0 ? GameManager.Instance.CurrentScore : 1;
            var timeBonus = (GameManager.Instance.totalPlayTime % 10) * 10;
            var finalScore = (_scoreBehavior.ScoreValue * scoreMultiplier) + timeBonus;
            // flash the score text if it's more than 20?
            GameManager.Instance.CurrentScore += (int) finalScore;
        }
        
    }
}