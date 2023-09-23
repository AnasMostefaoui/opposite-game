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
                return pool.Get();
            }

            var newPool = new GameObjectPool<GameObject>(
                () => Instantiate(gameObject), 
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
            Pools[gameObject.tag] = newPool;
            return newPool.Get();
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