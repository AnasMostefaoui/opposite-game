using OppositeGame._project.Scripts.mechanics;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "OppositeGame/BulletType", order = 1)]
    public class BulletType : ScriptableObject
    {
        public GameObject bulletPrefab;
        public GameObject hitEffectPrefab;
        public GameObject firingEffectPrefab;
        
        public float damage = 1f;
        public float lifeTime = 5f;
        // bullet sound
        
    }
}