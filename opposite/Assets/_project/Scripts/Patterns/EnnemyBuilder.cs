﻿using OppositeGame._project.Scripts.ScriptablesObjects;
using UnityEngine;

namespace OppositeGame._project.Scripts.Patterns
{
    public class EnemyBuilder
    {
        private EnemyType _enemyType;
        private BulletType _bulletType;
        private Vector3[] _waypoints;
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
            
            var instance = GameObject.Instantiate(_enemyType.enemyPrefab, _position, _rotation);
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