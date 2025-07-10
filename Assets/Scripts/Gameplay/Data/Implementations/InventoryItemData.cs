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

        public InventoryItemData(InventoryItemId id, int level, InventorySlotsDataContainer container) {
            Id = id;
            _level.Value = level;
            Container = container;
            Container.SetItem(this);
            
            CenterIndex = (container.Size - Vector2.one) * 0.5f;
        }

        public Vector2Int GetStartIndex(Vector2 approxSlotIndex) {
            var approxStartingIndex = approxSlotIndex - CenterIndex;
            return new Vector2Int(Mathf.RoundToInt(approxStartingIndex.x), Mathf.RoundToInt(approxStartingIndex.y));
        }

        public Vector2Int GetEndIndex(Vector2Int startingSlot) {
            return startingSlot + Container.Size - Vector2Int.one;
        }

        public void IncreaseLevel() {
            _level.Value += 1;
        }
    }
}
