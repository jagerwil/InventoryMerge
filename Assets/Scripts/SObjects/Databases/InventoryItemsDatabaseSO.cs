using System;
using System.Collections.Generic;
using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Data.Implementations;
using InventoryMerge.Gameplay.Views.Inventory;
using InventoryMerge.Gameplay.Views.Items;
using InventoryMerge.SObjects.Configs;
using InventoryMerge.Utils.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace InventoryMerge.SObjects.Databases {
    [CreateAssetMenu(fileName = "Items Database", menuName = "Configs/Databases/Items Database", order = 1)]
    public class InventoryItemsDatabaseSO : ScriptableObject {
        [field: SerializeField] public InventoryItemsDatabase Data { get; private set; }
    }

    [Serializable]
    public class InventoryItemsDatabase {
        [SerializeField] private List<InventoryItemInfo> _items;
        
        private LookupTable<InventoryItemId, InventoryItemInfo> _lookupTable;
        
        public IReadOnlyList<InventoryItemInfo> Items => _items;

        [CanBeNull]
        public InventoryItemInfo GetItemInfo(InventoryItemId id) {
            if (_lookupTable == null) {
                _lookupTable = new LookupTable<InventoryItemId, InventoryItemInfo>(_items, itemInfo => itemInfo.ItemId);
            }

            return _lookupTable.GetElement(id);
        }

        [CanBeNull]
        public InventoryItemLevelInfo GetItemLevelInfo(InventoryItemId id, int level) {
            var itemInfo = GetItemInfo(id);
            if (itemInfo == null) {
                return null;
            }
            
            if (level < 0 || level >= itemInfo.Levels.Count) {
                Debug.LogError($"{GetType().Name}.{nameof(GetItemLevelInfo)}(): Cannot find info for level {level}");
                return null;
            }

            return itemInfo.Levels[level];
        }
    }

    [Serializable]
    public class InventoryItemInfo {
        [field: SerializeField] public InventoryItemId ItemId { get; private set; }
        [field: SerializeField] public InventoryItemView Prefab { get; private set; }
        [SerializeField] private InventorySlotsDataContainerSpawnInfo _spawnInfo;
        [SerializeField] private List<InventoryItemLevelInfo> _levels;
        
        public InventorySlotsDataContainer ItemSlots => _spawnInfo.CreateInstance();
        public IReadOnlyList<InventoryItemLevelInfo> Levels => _levels;
    }

    [Serializable]
    public class InventoryItemLevelInfo {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Color Color { get; private set; } = Color.white;
        //You can put the item stats in here
    }
}
