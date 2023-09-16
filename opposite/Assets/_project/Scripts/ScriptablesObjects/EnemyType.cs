using OppositeGame._project.Scripts.mechanics;
using UnityEngine;
using UnityEngine.Serialization;

namespace OppositeGame
{
    [CreateAssetMenu(fileName = "EnemyType", menuName = "OppositeGame/EnemyType", order = 0)]
    public class EnemyType : ScriptableObject, IMovable
    {
        public GameObject enemyPrefab;
        public BulletType bulletType;
        public float speed = 5f;
        public float fireRate = 1f;
        public float health = 1f;
        public uint score = 1;

        public float getSpeed() => speed;
    }
}