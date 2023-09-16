using OppositeGame._project.Scripts.mechanics;
using UnityEngine;

namespace OppositeGame
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "OppositeGame/BulletType", order = 1)]
    public class BulletType : ScriptableObject, IMovable
    {
        public GameObject bulletPrefab;
        public GameObject hitEffectPrefab;
        public GameObject firingEffectPrefab;
        
        public float speed = 5f;
        public float damage = 1f;
        public float lifeTime = 5f;
        // bullet sound
        
        
        public float getSpeed() => speed;
    }
}