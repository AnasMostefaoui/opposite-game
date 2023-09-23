using System.Collections.Generic;
using UnityEngine;

namespace OppositeGame._project.Scripts.Patterns
{
    public class ObjectPoolManager : MonoBehaviour
    {
        private static readonly Dictionary<string, GameObjectPool<GameObject>> Pools = new();
        public static GameObject Retrieve(GameObject gameObject)
        {
            if (Pools.TryGetValue(gameObject.tag, out var pool))
            {
                var obj = pool.Get();
                Debug.Log("Get? " + obj);
                return obj;
            }

            var newPool = new GameObjectPool<GameObject>(
                () => Instantiate(gameObject), 
                objectToDestroy => objectToDestroy.SetActive(false),
                objectToActivate => objectToActivate.SetActive(true),
                objectToDeactivate => objectToDeactivate.SetActive(false)
            );
            Pools[gameObject.tag] = newPool;
            var newObject = newPool.Get();
            
            Debug.Log("newObject? " + newObject);
            return newObject;
        }
        
        public static void Recycle(GameObject gameObject)
        {
            if (Pools.TryGetValue(gameObject.tag, out var pool))
            {
                if (gameObject.gameObject.activeSelf == false) return;
                pool.Release(gameObject);
            }
            else
            {
                Debug.LogError($"No pool found for key: {gameObject.tag}");
            }
        }

    }
}