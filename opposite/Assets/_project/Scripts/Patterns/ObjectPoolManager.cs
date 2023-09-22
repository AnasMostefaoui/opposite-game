using System;
using System.Collections.Generic;
using System.Linq;
using OppositeGame._project.Scripts.Patterns;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Pools
{
    public class ObjectPoolManager : MonoBehaviour
    {
        private static readonly Dictionary<string, GameObjectPool<GameObject>> Pools = new();
        public static GameObject Retrieve(GameObject gameObject)
        {
            if (Pools.TryGetValue(gameObject.tag, out var pool))
            {
                return pool?.Get();
            }

            var newPool = new GameObjectPool<GameObject>(
                () => Instantiate(gameObject), 
                Destroy,
                objectToActivate => objectToActivate.SetActive(true),
                objectToDeactivate => objectToDeactivate.SetActive(false)
            );
            Pools[gameObject.tag] = newPool;
            return newPool.Get();
        }
        
        public static void Recycle(GameObject gameObject)
        {
            if (Pools.TryGetValue(gameObject.tag, out var pool))
            {
                pool.Release(gameObject);
            }
            else
            {
                Debug.LogError($"No pool found for key: {gameObject.tag}");
            }
        }

    }
}