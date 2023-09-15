using UnityEngine;

namespace OppositeGame
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "OppositeGame/BulletType", order = 1)]
    public class BulletType : ScriptableObject, IMovable
    {
        public GameObject bulletPrefab;
        public float speed = 5f;
        public float damage = 1f;
        // bullet sound
        
        
        public float getSpeed() => speed;
    }
}