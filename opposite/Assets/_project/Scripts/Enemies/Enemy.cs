using System;
using OppositeGame._project.Scripts.Managers;
using OppositeGame._project.Scripts.mechanics;
using OppositeGame._project.Scripts.mechanics.Scoring;
using OppositeGame._project.Scripts.ScriptablesObjects;
using UnityEngine;

namespace OppositeGame._project.Scripts.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] public EnemyType EnemyType;
        [SerializeField] public float maxLifePoints = 50;
        private ScorableBehavior _scoreBehavior;
        private Destructible _destructible;
        public Action OnEnemyDisabled;
        public float CurrentHealth => _destructible.LifePoints;
        
        private void Awake()
        {
            _scoreBehavior = GetComponent<ScorableBehavior>();
            _destructible = GetComponent<Destructible>();
            if (_destructible)
            {
                _destructible.LifePoints = maxLifePoints;
            }
        }

        private void OnDisable()
        {
            if(_scoreBehavior == null) return;
            var scoreMultiplier = GameManager.Instance.currentLifePoint > 0 ? 
                GameManager.Instance.currentLifePoint : 1;
            var timeBonus = (GameManager.Instance.totalPlayTime / 10) * 10;
            var finalScore = (_scoreBehavior.ScoreValue * scoreMultiplier) + timeBonus;
            // flash the score text if it's more than 20?
            GameManager.Instance.CurrentScore += (int) finalScore;
        }
        
    }
}