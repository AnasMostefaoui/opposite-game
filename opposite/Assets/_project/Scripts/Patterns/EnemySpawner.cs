using System;
using System.Collections.Generic;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<EnemyType> enemyTypes;
        [SerializeField] private int maxEnemies = 10;
        [SerializeField] private float spawnIntervalInSeconds = 1f;
        [SerializeField] private Camera cameraObject;
        
        [SerializeField] private float activationDistance = 1f;
        [SerializeField] private Vector3 velocity = Vector3.zero;
        
        private EnemyFactory _factory;
        private float _nextSpawnTime;
        private bool _canSpawn = false;
        private int _enemyCount;
        private Vector3[] _waypoints;

        private void Start()
        {
            _factory = new EnemyFactory(); 
            _nextSpawnTime = Time.time + spawnIntervalInSeconds;
            if (cameraObject == null)
            {
                cameraObject = Camera.main;
            }
        }

        private void Update()
        {
            var activationPosition =
                new Vector3(cameraObject.transform.position.x, transform.position.y - activationDistance, 0);
            
            
            _canSpawn =  cameraObject.IsPointInViewport(activationPosition);
            if (_canSpawn == false || !(Time.time >= _nextSpawnTime) || _enemyCount >= maxEnemies) return;
            
            Debug.Log("Im here");
            _enemyCount+= 1;
                
            // method calls in updates are expensive, since we only spawn, we can inline the code
            var enemyType = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count)];
            var bulletType = enemyType.bulletType;
                
            //TODO: use pooling?
            var enemy = _factory.CreateEnemy(enemyType, bulletType);
            enemy.GetComponent<Move>().velocity = velocity.normalized * enemyType.speed;
            enemy.transform.position = transform.position;
            _nextSpawnTime = Time.time + spawnIntervalInSeconds;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            
            Gizmos.DrawWireSphere( transform.position, 0.1f);
            Gizmos.DrawRay(transform.position, velocity.normalized * 0.25f);
            Gizmos.color = new Color(0.5f, 0.2f, 1f, 0.5f);
            Gizmos.DrawRay(transform.position, new Vector3(0, -activationDistance, 0));
        }
    }
}