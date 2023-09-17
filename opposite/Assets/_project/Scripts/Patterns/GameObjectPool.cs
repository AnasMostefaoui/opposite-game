using System;
using UnityEngine;
using UnityEngine.Pool;

namespace OppositeGame._project.Scripts.Patterns
{
    
    public class GameObjectPool<T> where T : class
    {
        private readonly ObjectPool<T> _objectPool;
        private readonly Func<T> _createObject;
        private readonly Action<T> _onDestroyObject;
        private readonly Action<T> _onActivation;
        private readonly Action<T> _onDeactivation;

        public GameObjectPool(
            Func<T> createObject, 
            Action<T> onDestroyObject, 
            Action<T> onActivation, 
            Action<T> onDeactivation, 
            int defaultCapacity = 20, 
            int maxCapacity = 50)
        {
            _createObject = createObject;
            _onDestroyObject = onDestroyObject;
            _onActivation = onActivation;
            _onDeactivation = onDeactivation;
            
            _objectPool = new ObjectPool<T>(CreateBullet,
                OnRequest, 
                OnRemoval,
                OnDestroy, 
                true, 
                defaultCapacity, 
                maxCapacity);
        }
        
        public T Get()
        {
            return _objectPool.Get();
        }
        
        public void Release(T gameObject)
        {
            _objectPool.Release(gameObject);
        }
        
        private T CreateBullet()
        {
            return  _createObject?.Invoke();
        }
        
        private void OnRequest(T gameObject)
        {
            _onActivation?.Invoke(gameObject);
        }
        
        private void OnRemoval(T gameObject)
        {
            _onDeactivation?.Invoke(gameObject);
        }
        
        private void OnDestroy(T gameObject)
        {
            _onDestroyObject?.Invoke(gameObject);
        }
    }
}