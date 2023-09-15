using System;
using UnityEngine;

namespace OppositeGame
{
    public class EnemyFactory
    {
        public GameObject CreateEnemy(EnemyType enemyType, BulletType bulletType, Vector3[] waypoints = null )
        {
            var builder = new EnemyBuilder();
            builder.SetEnemyType(enemyType)
                .SetBulletType(bulletType)
                .SetWaypoints(waypoints);

            return builder.SetEnemyType(enemyType)
                .SetBulletType(bulletType)
                .SetWaypoints(waypoints)
                .Build();
        }
    }
}