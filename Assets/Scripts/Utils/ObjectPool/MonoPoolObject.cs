using System;
using UnityEngine;

namespace InventoryMerge.Utils.ObjectPool {
    public class MonoPoolObject : MonoBehaviour {
        public event Action OnDespawned;

        public void Despawn() {
            OnDespawned?.Invoke();
        }
    }
}
