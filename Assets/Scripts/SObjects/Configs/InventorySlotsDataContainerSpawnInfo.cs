using System;
using System.Collections.Generic;
using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Data.Implementations;
using UnityEngine;

namespace InventoryMerge.SObjects.Configs {
    [Serializable]
    public class InventorySlotsDataContainerSpawnInfo {
        [SerializeField] private Vector2Int _size;
        [SerializeField] private List<InventorySlotDataSpawnInfo> _slots;
        
        [NonSerialized] private InventorySlotsDataContainer _dataContainer;

        public InventorySlotsDataContainer GetOrCreateInstance() {
            if (_dataContainer == null) {
                _dataContainer = new InventorySlotsDataContainer(_size, _slots);
            }
            return _dataContainer;
        }
    }

    [Serializable]
    public class InventorySlotDataSpawnInfo {
        [field: SerializeField] public InventorySlotState State { get; private set; }

#if UNITY_EDITOR
        public void SetState(InventorySlotState state) {
            State = state;
        }
#endif
    }
}
