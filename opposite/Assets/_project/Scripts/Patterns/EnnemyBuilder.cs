using System;
using UnityEngine;

namespace OppositeGame
{
    public class EnemyBuilder
    {
        private EnemyType _enemyType;
        private BulletType _bulletType;
        private Vector3[] _waypoints;

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
        public EnemyBuilder SetWaypoints(Vector3[] waypoints = null)
        {
            _waypoints = waypoints ?? Array.Empty<Vector3>();
            return this;
        }
        
        public GameObject Build()
        {
            var instance = GameObject.Instantiate(_enemyType.enemyPrefab);
            var followPath = instance.GetComponent<FollowPath>();
            if (followPath)
            {
                followPath.waypoints = _waypoints;
            } 
            // bullet from pool?
            
            return instance;
        }
    }
}