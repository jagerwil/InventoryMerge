using System;
using System.Collections.Generic;
using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Data.Implementations;
using InventoryMerge.Utils.Data;
using JetBrains.Annotations;
using UnityEngine;

namespace InventoryMerge.SObjects.Databases {
    [Serializable]
    public class InventoryItemsDatabase {
        [SerializeField] private List<InventoryItemInfo> _items;
        
        private LookupTable<InventoryItemId, InventoryItemInfo> _lookupTable;

        [CanBeNull]
        public InventoryItemInfo GetItemInfo(InventoryItemId id) {
            if (_lookupTable == null) {
                _lookupTable = new LookupTable<InventoryItemId, InventoryItemInfo>(_items, itemInfo => itemInfo.ItemId);
            }

            return _lookupTable.GetElement(id);
        }
    }

    [Serializable]
    public class InventoryItemInfo {
        [field: SerializeField] public InventoryItemId ItemId { get; private set; }
        [SerializeField] private InventorySlotsDataContainer _itemSlots;
        [SerializeField] private List<InventoryItemLevelsInfo> _levels;
        
        public IInventorySlotsDataContainer ItemSlots => _itemSlots;
        public IEnumerable<InventoryItemLevelsInfo> Levels => _levels;
    }

    [Serializable]
    public class InventoryItemLevelsInfo {
        [field: SerializeField] public Sprite Sprite { get; private set; }
        //You can put the item stats in here
    }
}
