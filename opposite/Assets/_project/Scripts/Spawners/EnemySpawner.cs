using System;
using System.Collections.Generic;
using OppositeGame._project.Scripts.mechanics;
using OppositeGame._project.Scripts.mechanics.Movement;
using OppositeGame._project.Scripts.Patterns;
using OppositeGame._project.Scripts.Utilities;
using UnityEngine;

namespace OppositeGame
{
    public class EnemySpawner : MonoBehaviour
    {
        [Header("Enemy settings")]
        [SerializeField] private List<EnemyType> enemyTypes;
        [SerializeField] private int maxEnemies = 10;
        [SerializeField] private Vector3 velocity = Vector3.zero;
        
        [Header("Spawn settings")]
        [SerializeField] private float spawnIntervalInSeconds = 1f;
        [SerializeField] private Camera cameraObject;
        [SerializeField] private float activationDistance = 1f;
        
        private EnemyFactory _factory;
        private float _nextSpawnTime;
        private int _enemyCount;
        private Vector3[] _waypoints;

        private void Start()
        {
            _factory = new EnemyFactory(); 
            _nextSpawnTime = Time.time + spawnIntervalInSeconds;
            cameraObject ??= Camera.main;
        }

        private void Update()
        {
            if (CanSpawn == false) return;
            _enemyCount+= 1;
            Spawn();
            _nextSpawnTime = Time.time + spawnIntervalInSeconds;
        }

        private bool CanSpawn
        {
            get
            {
                var isTimeToSpawn = Time.time >= _nextSpawnTime;
                var isMaxEnemies = _enemyCount >= maxEnemies;
                // make sure the activation point of the spawner is inside the camera viewport to start spawning
                // another idea was to use a trigger collider, but that would require a lot of colliders and calucations power
                // this is a simple and effective solution
                var activationPosition =
                    new Vector3(cameraObject.transform.position.x, transform.position.y - activationDistance, 0);
                return cameraObject.IsPointInViewport(activationPosition) && isTimeToSpawn && !isMaxEnemies;
            }
        }
       
        private void Spawn() 
        {
            // method calls in updates are expensive, since we only spawn, we can inline the code
            var enemyType = enemyTypes[UnityEngine.Random.Range(0, enemyTypes.Count)];
            //TODO: use pooling?
            var enemy = _factory.CreateEnemy(enemyType);
            enemy.GetComponent<Move>().velocity = velocity.normalized * enemyType.speed;
            enemy.transform.position = transform.position;
            enemy.GetComponent<ViewPortObserver>().OnLeftViewport += () => _factory.DestroyEnemy(enemy);
        }
        
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            
            Gizmos.DrawWireSphere( transform.position, 0.1f);
            Gizmos.DrawRay(transform.position, velocity.normalized * 0.25f);
            // draw the activation distance line visual helper
            Gizmos.color = new Color(0.5f, 0.2f, 1f, 0.5f);
            Gizmos.DrawRay(transform.position, new Vector3(0, -activationDistance, 0));
        }
    }
}