using OppositeGame._project.Scripts.CameraScripts;
using OppositeGame._project.Scripts.mechanics.Movement;
using OppositeGame._project.Scripts.Patterns;
using UnityEngine;

namespace OppositeGame._project.Scripts.ScriptablesObjects.Pools
{
    [CreateAssetMenu(fileName = "BackgroundPool", menuName = "OppositeGame/Pools/BackgroundPool", order = 1)]
    public class BackgroundPool : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        private GameObjectPool<GameObject> _objectPool;
        private void OnEnable()
        {            
            _objectPool = new GameObjectPool<GameObject>(
                CreateObject, 
                OnPoolCleanup, 
                OnActivation, 
                OnRemoval, 
                5, 
                5);
        }
        
        public GameObject GetBackground() => _objectPool.Get();
        private GameObject CreateObject()
        {
            var background = Instantiate(prefab);
            background.GetComponent<IPoolable<GameObject>>().OnRelease ??= (b) => _objectPool.Release(b.gameObject); 
            background.GetComponent<ViewPortObserver>().OnLeftViewport ??= () => _objectPool.Release(background.gameObject); 
            return background;
        }

        private void OnActivation(GameObject objectToActivate) => objectToActivate.SetActive(true);
        private void OnRemoval(GameObject objectToRemove) =>  objectToRemove.SetActive(false);
        private void OnPoolCleanup(GameObject objectToDestroy)
        {
            objectToDestroy.GetComponent<IPoolable<GameObject>>().OnRelease = null; 
            objectToDestroy.GetComponent<ViewPortObserver>().OnLeftViewport = null; 
            Destroy(objectToDestroy);
        }
    }
}