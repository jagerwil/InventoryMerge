using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace InventoryMerge.Utils.ObjectPool {
    public class MonoObjectPool<T> where T : MonoPoolObject {
        private readonly Queue<T> _inactiveItems = new();
        
        private readonly T _prefab;
        private readonly Transform _defaultParent;
        private readonly IObjectResolver _objectResolver;
        
        public MonoObjectPool(T prefab, Transform defaultParent, IObjectResolver objectResolver) {
            _prefab = prefab;
            _defaultParent = defaultParent;
            _objectResolver = objectResolver;
        }

        public T Spawn(Transform parent = null) {
            if (_inactiveItems.Count > 0) {
                var obj = _inactiveItems.Dequeue();
                obj.gameObject.SetActive(true);
                return obj;
            }

            var newObject = _objectResolver.Instantiate(_prefab, parent ? parent : _defaultParent);
            newObject.OnDespawned += () => Despawn(newObject);
            return newObject;
        }

        public void Despawn(T obj) {
            obj.gameObject.SetActive(false);
            obj.transform.SetParent(_defaultParent);
            _inactiveItems.Enqueue(obj);
        }
    }
}
