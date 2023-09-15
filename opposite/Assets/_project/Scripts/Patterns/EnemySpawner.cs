using System;
using System.Collections.Generic;
using UnityEngine;

namespace OppositeGame
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyType> enemyTypes;
        [SerializeField] private int maxEnemies = 10;
        [SerializeField] private float spawnIntervalInSeconds = 1f;
        [SerializeField] private Vector3 direction = Vector3.down;
        [SerializeField] private Vector3 velocity = Vector3.zero;
        private EnemyFactory _factory;
        private float _spawnTimer; 
        private int _enemyCount;
        private Vector3[] _waypoints;

        private void Start()
        {
            _factory = new EnemyFactory();
        }

        private void Update()
        {
            _spawnTimer += Time.deltaTime;

            if (!(_spawnTimer >= spawnIntervalInSeconds) || _enemyCount >= maxEnemies) return;
            
            _spawnTimer = 0f;
            _enemyCount+= 1;
                
            // method calls in updates are expensive, since we only spawn, we can inline the code
            var enemyType = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count)];
            var bulletType = enemyType.bulletType;
                
            //TODO: use pooling?
            var enemy = _factory.CreateEnemy(enemyType, bulletType);
            enemy.GetComponent<Move>().velocity = velocity;
            enemy.transform.position = transform.position;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            
            Gizmos.DrawWireSphere( transform.position, 0.1f);
            Gizmos.DrawRay(transform.position, velocity.normalized * 0.25f);
        }
    }
}