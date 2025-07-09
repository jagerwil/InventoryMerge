using System.Collections.Generic;
using UnityEngine;

namespace InventoryMerge.Gameplay.Data.Implementations {
    public class InventoryData : IReadOnlyInventoryData {
        private readonly InventorySlotsDataContainer _container;

        public Vector2Int Size => _container.Size;

        public InventoryData(Vector2Int size) {
            _container = new InventorySlotsDataContainer(size);
        }

        public InventorySlotData GetSlot(Vector2Int index) {
            return _container.GetElement(index);
        }
        
        public bool CanFitItem(IInventoryItemData item, Vector2Int startingIndex) {
            return _container.CanFit(item.Container, startingIndex);
        }

        public bool TryFitItem(IInventoryItemData item, Vector2Int startingIndex, out IEnumerable<IInventoryItemData> removedItems) {
            return _container.TryFit(item.Container, startingIndex, out removedItems);
        }

        public bool TryRemoveItem(IInventoryItemData item) {
            var hasAnyItemSlots = false;
            
            foreach (var slot in _container.GetElements()) {
                if (slot.Item.CurrentValue == item) {
                    slot.SetItem(null);
                    hasAnyItemSlots = true;
                }
            }

            return hasAnyItemSlots;
        }
    }
}
