using System;
using UnityEngine;

namespace InventoryMerge.Gameplay.Data.Implementations {
    [Serializable]
    public class InventoryItemData : IInventoryItemData {
        public InventoryItemId Id { get; private set; }
        public int Level { get; private set; }

        public IInventorySlotsDataContainer Container { get; private set; }
        public Vector2 CenterIndex { get; private set; }

        public InventoryItemData(InventoryItemId id, int level, IInventorySlotsDataContainer container) {
            Id = id;
            Level = level;
            Container = container;
            
            CenterIndex = (container.Size - Vector2.one) * 0.5f;
        }
    }
}
