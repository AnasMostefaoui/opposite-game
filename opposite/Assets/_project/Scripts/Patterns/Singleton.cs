using UnityEngine;

namespace OppositeGame._project.Scripts.Patterns
{
        public class Singleton<T> : MonoBehaviour where T : Singleton<T>
        {
            protected static T _instance = null;
        
            public static T Instance
            {
                get
                {
                    if (!_instance)
                        InstantiateInternal(null, null, true);
                    return _instance;
                }
            }

            public static void Instantiate(T instance = null, GameObject gameObject = null)
            {
                InstantiateInternal(instance, gameObject, true);
            }

            private static void InstantiateInternal(T instance = null, GameObject gameObject = null, bool forceCreate = false)
            {
                if (_instance != null && instance != null)
                {
                    Destroy(instance.gameObject);
                    return;
                }

                _instance = FindObjectOfType<T>();

                if (_instance || !forceCreate) return;
            
                if (gameObject == null) gameObject = new GameObject(typeof(T).ToString());
                _instance = gameObject.AddComponent<T>();
            }

            protected virtual void Awake()
            {
                if (_instance == this) return;
            
                InstantiateInternal((T)this);
            }

            protected virtual void OnDestroy()
            {
                if (_instance != this) return;
            
                _instance = null;
            }
        }
}