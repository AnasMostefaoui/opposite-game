using System.Collections.Generic;
using OppositeGame._project.Scripts.CameraScripts;
using OppositeGame._project.Scripts.mechanics.Movement;
using OppositeGame._project.Scripts.ScriptablesObjects;
using UnityEngine;

namespace OppositeGame._project.Scripts.Patterns
{
    public class EnemyFactory
    {
        // first draft of pooling
        private List<GameObject> _enemies = new List<GameObject>();
        
        public GameObject CreateEnemy(EnemyType enemyType, Vector3 position, Quaternion rotation)
        {
            var builder = new EnemyBuilder();
            var enemy = builder.SetEnemyType(enemyType)
                .SetBulletType(enemyType.bulletType)
                .SetPosition(position)
                .SetRotation(rotation)
                .Build();
            _enemies.Add(enemy);
            return enemy;
        }
        
        public void DestroyEnemy(GameObject enemy)
        {
            _enemies.Remove(enemy);
            // in case we subscribed to these events somewhere, clear them up to avoid memory leaks
            enemy.GetComponent<ViewPortObserver>().OnEnteredViewport = null;
            enemy.GetComponent<ViewPortObserver>().OnLeftViewport = null;
            Object.Destroy(enemy);
        }
    }
}