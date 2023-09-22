using OppositeGame._project.Scripts.mechanics;
using OppositeGame._project.Scripts.mechanics.Bullets;
using OppositeGame._project.Scripts.mechanics.Magnetism;
using OppositeGame._project.Scripts.ScriptablesObjects.Pools;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects
{
    [CreateAssetMenu(fileName = "BulletType", menuName = "OppositeGame/BulletType", order = 1)]
    public class BulletType : ScriptableObject
    {
        public ParticleSystem explosionEffect;
        public Bullet bulletPrefab;
        public float lifeTime = 5f;
        public float damage = 1f;
        // bullet sound
        
    }
}