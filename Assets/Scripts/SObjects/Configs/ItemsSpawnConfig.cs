using System;
using System.Collections.Generic;
using InventoryMerge.Gameplay.Data;
using UnityEngine;

namespace InventoryMerge.SObjects.Configs {
    [Serializable]
    public class ItemsSpawnConfig {
        [SerializeField] private List<InventoryItemSpawnInfo> _items;
        
        public IReadOnlyList<InventoryItemSpawnInfo> Items => _items;
    }

    [Serializable]
    public class InventoryItemSpawnInfo {
        [field: SerializeField] public InventoryItemId Id { get; private set; }
        [field: SerializeField] public int Level { get; private set; }
    }
}
