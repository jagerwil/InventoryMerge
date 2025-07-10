using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Utils.ObjectPool {
    public class PrefabMonoObjectPool<TKey, TObject> where TObject : MonoPoolObject {
        private readonly Dictionary<TKey, MonoObjectPool<TObject>> _pools = new();
        
        private readonly Transform _defaultParent;
        private readonly IObjectResolver _objectResolver;
        
        public PrefabMonoObjectPool(Transform defaultParent, IObjectResolver objectResolver) {
            _defaultParent = defaultParent;
            _objectResolver = objectResolver;
        }

        public void Register(TKey key, TObject prefab) {
            if (_pools.ContainsKey(key)) {
                Debug.LogError($"{GetType().Name}.{nameof(Register)}(): key \"{key}\" has already been added!");
                return;
            }
            
            _pools.Add(key, new MonoObjectPool<TObject>(prefab, _defaultParent, _objectResolver));
        }

        [CanBeNull]
        public TObject TrySpawnObject(TKey key, Transform parent = null) {
            var pool = TryGetPool(key);
            return pool?.Spawn(parent);
        }

        public void DespawnObject(TKey key, TObject obj) {
            var pool = TryGetPool(key);
            pool?.Despawn(obj);
        }

        [CanBeNull]
        private MonoObjectPool<TObject> TryGetPool(TKey key) {
            if (_pools.TryGetValue(key, out var pool)) {
                return pool;
            }
            
            Debug.LogError($"{GetType().Name}.{nameof(TrySpawnObject)}(): key \"{key}\" was not added to pool!");
            return null;
        }
    }
}
