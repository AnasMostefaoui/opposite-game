using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Pools
{
    public class ObjectPoolManager : MonoBehaviour
    {
        public static readonly List<PooledObjectInfo> ObjectPools = new List<PooledObjectInfo>(200);
        public static GameObject Retrieve(GameObject gameObject)
        {
            var pool = ObjectPools.Find(x => gameObject.CompareTag(x.LookupKey));
            if (pool == null)
            {
                pool = new PooledObjectInfo() { LookupKey = gameObject.tag };
                ObjectPools.Add(pool);
            }

            var pooledObject = pool.InactiveObjects.FirstOrDefault();
            if (pooledObject == null)
            {
                return Instantiate(gameObject);
            }
            pool.InactiveObjects.RemoveAt(0);
            pooledObject.SetActive(true);
            
            return pooledObject;
        }
        
        public static void Recycle(GameObject gameObject)
        {
            var pool = ObjectPools.Find(x => gameObject.CompareTag(x.LookupKey));
            if (pool == null)
            {
                Debug.LogError($"No pool found for key: {gameObject.tag}");
                return;
            }
            gameObject.SetActive(false);
            pool.InactiveObjects.Add(gameObject);
        }

    }
    
    public class PooledObjectInfo
    {
        public string LookupKey;
        public List<GameObject> InactiveObjects = new List<GameObject>();
    }
}