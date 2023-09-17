using UnityEngine;

namespace OppositeGame._project.Scripts.mechanics
{
    public class Destructible : MonoBehaviour
    {
        private int _currentLifePoints;
        public void SetLifePoints(int lifePoints)
        {
            _currentLifePoints = lifePoints;
        }
        
    }
}