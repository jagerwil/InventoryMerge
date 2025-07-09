using System;
using R3;
using UnityEngine;

namespace InventoryMerge.Gameplay.Data.Implementations {
    [Serializable]
    public class InventoryItemData : IInventoryItemData {
        private ReactiveProperty<int> _level = new();
        
        public ReadOnlyReactiveProperty<int> Level => _level;
        
        public InventoryItemId Id { get; private set; }
        public IInventorySlotsDataContainer Container { get; private set; }
        public Vector2 CenterIndex { get; private set; }

        public InventoryItemData(InventoryItemId id, int level, IInventorySlotsDataContainer container) {
            Id = id;
            _level.Value = level;
            Container = container;
            
            CenterIndex = (container.Size - Vector2.one) * 0.5f;
        }
        
        public void IncreaseLevel() {
            _level.Value += 1;
        }
    }
}
