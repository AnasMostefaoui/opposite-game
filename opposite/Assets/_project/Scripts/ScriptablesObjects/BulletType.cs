using OppositeGame._project.Scripts.mechanics;
using OppositeGame._project.Scripts.mechanics.Bullets;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "OppositeGame/BulletType", order = 1)]
    public class BulletType : ScriptableObject
    {
        public Bullet bulletPrefab;
        public GameObject hitEffectPrefab;
        public GameObject firingEffectPrefab;
        
        public float damage = 1f;
        public float lifeTime = 5f;
        // bullet sound
        
    }
}