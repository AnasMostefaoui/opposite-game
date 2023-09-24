using OppositeGame._project.Scripts.mechanics;
using OppositeGame._project.Scripts.ScriptablesObjects;
using UnityEngine;

namespace OppositeGame._project.Scripts.Patterns
{
    public class EnemyBuilder
    {
        private EnemyType _enemyType;
        private BulletType _bulletType;
        private Vector3 _position;
        private Quaternion _rotation;

        public EnemyBuilder SetEnemyType(EnemyType enemyType)
        {
            _enemyType = enemyType;
            return this;
        }
        
        public EnemyBuilder SetBulletType(BulletType bulletType)
        {
            _bulletType = bulletType;
            return this;
        }
        
        public EnemyBuilder SetPosition(Vector3 position)
        {
            _position = position;
            return this;
        }
        
        public EnemyBuilder SetRotation(Quaternion rotation)
        {
            _rotation = rotation;
            return this;
        }
        
        public GameObject Build()
        {
            // pull from object pool
            var instance = ObjectPoolManager.Retrieve(_enemyType.enemyPrefab);
            instance.transform.position = _position;
            instance.transform.rotation = _rotation;
            var enemy = instance.GetComponent<Destructible>();
            enemy.LifePoints = _enemyType.health;
            return instance;
        }
    }
}