using System.Collections.Generic;
using UnityEngine;

namespace OppositeGame._project.Scripts.Patterns
{
    public static class ObjectPoolManager 
    {
        
        private static Dictionary<string, GameObjectPool<GameObject>> _pools = new();
        public static GameObject Retrieve(GameObject gameObject)
        {
            if (_pools.TryGetValue(gameObject.tag, out var pool))
            {
                Debug.Log("getting from pool");
                return pool.Get();
            }

            Debug.Log("Creating pool");
            var newPool = new GameObjectPool<GameObject>(
                () => Object.Instantiate(gameObject), 
                objectToDestroy =>
                {
                    if (objectToDestroy != null)
                    {
                        objectToDestroy.SetActive(false);
                    }
                },
                objectToActivate =>
                {
                    if (objectToActivate != null)
                    {
                        objectToActivate.SetActive(true);
                    }
                },
                objectToDeactivate =>
                {
                    
                    if (objectToDeactivate != null)
                    {
                        objectToDeactivate.SetActive(false);
                    }
                });
            _pools[gameObject.tag] = newPool;
            return newPool.Get();
        }
        
        public static void Recycle(GameObject gameObject)
        {
            if (_pools.TryGetValue(gameObject.tag, out var pool))
            {
                if (gameObject.gameObject.activeSelf == false) return;
                pool.Release(gameObject);
            }
            else
            {
                Debug.LogError($"No pool found for key: {gameObject.tag}");
            }
        }

        public static void Cleanup()
        {
            _pools = new();
        }

    }
}