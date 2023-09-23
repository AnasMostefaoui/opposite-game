using OppositeGame._project.Scripts.Managers;
using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics.Scoring
{
    public class ScorableBehavior : MonoBehaviour
    {
        [SerializeField] public int ScoreValue = 1;
        
        public void AssignToScoreboard()
        {
            GameManager.Instance.CurrentScore += ScoreValue;
        }
    }
}