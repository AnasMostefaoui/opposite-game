using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace OppositeGame._project.Scripts.Patterns
{
    public class GamePool : MonoBehaviour
    {
        public static GamePool Instance { get; private set; }
        private Dictionary<String, GameObjectPool<GameObject>> pool;

        private void Awake()
        {
            if( Instance == null )
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public GameObjectPool<GameObject> Get ([NotNull] Type objectType, GameObject prefab)
        {
            if (objectType == null) throw new ArgumentNullException(nameof(objectType));
            var key = nameof(objectType);
            if (pool.TryGetValue(key, out var value))
            {
                return value;
            }

            pool[key] = new GameObjectPool<GameObject>(
                () => Instantiate(prefab),
                gameObject => Destroy(gameObject),
                gameObject => gameObject.SetActive(true),
                gameObject => gameObject.SetActive(false)
            );
            return pool[key];
        }
    }
    
    
}