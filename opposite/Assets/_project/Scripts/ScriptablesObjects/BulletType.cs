using OppositeGame._project.Scripts.mechanics;
using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "OppositeGame/BulletType", order = 1)]
    public class BulletType : ScriptableObject
    {
        public Bullet bulletPrefab;
        public  BulletImpactPool bulletImpactPool;
        public  BulletImpactPool firingEffectPool;
        
        public float damage = 1f;
        
        public float lifeTime = 5f;
        // bullet sound
        
    }
}