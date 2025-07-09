using System;
using System.Collections.Generic;
using System.Linq;
using InventoryMerge.Utils.Data;
using UnityEngine;

namespace InventoryMerge.Gameplay.Data.Implementations {
    [Serializable]
    public class InventorySlotsDataContainer : GridContainer<InventorySlotData>, IInventorySlotsDataContainer {
        private List<IInventoryItemData> _emptyList = new();
        
        public InventorySlotsDataContainer(Vector2Int size) {
            Initialize(size);
        }
        
        public void SetItem(IInventoryItemData item) {
            foreach (var slot in GetSlots()) {
                slot.SetItem(item);
            }
        }
        
        protected override InventorySlotData CreateElement(Vector2Int index) {
            return new InventorySlotData(index);
        }

        public IInventorySlotData GetSlot(Vector2Int index) => GetElement(index);
        public IEnumerable<IInventorySlotData> GetSlots() => GetElements();

        public bool CanFit(IInventorySlotsDataContainer other, Vector2Int startingIndex) {
            if (startingIndex.x < 0 || startingIndex.y < 0) {
                return false;
            }
            
            var endIndex = startingIndex + other.Size - Vector2Int.one;
            if (endIndex.x >= Size.x || endIndex.y >= Size.y) {
                return false;
            }

            foreach (var slot in other.GetSlots()) {
                if (slot.State != InventorySlotState.Available) {
                    continue;
                }
                
                var slotIndex = slot.Index + startingIndex;
                var localSlot = GetSlot(slotIndex);

                if (localSlot.State != InventorySlotState.Available) {
                    return false;
                }
            }

            return true;
        }

        public bool TryFit(IInventorySlotsDataContainer other, Vector2Int startingIndex, out IEnumerable<IInventoryItemData> removedItems) {
            if (!CanFit(other, startingIndex)) {
                removedItems = _emptyList;
                return false;
            }

            var removedItemsHashSet = new HashSet<IInventoryItemData>();
            foreach (var slot in other.GetSlots()) {
                if (slot.State != InventorySlotState.Available) {
                    continue;
                }
                
                var slotIndex = slot.Index + startingIndex;
                var localSlot = GetSlot(slotIndex);

                if (localSlot.Item.CurrentValue != null && localSlot.Item.CurrentValue != slot.Item.CurrentValue) {
                    removedItemsHashSet.Add(localSlot.Item.CurrentValue);
                }

                localSlot.SetItem(slot.Item.CurrentValue);
            }

            removedItems = removedItemsHashSet;
            RemoveItems(removedItems);

            return true;
        }

        private void RemoveItems(IEnumerable<IInventoryItemData> items) {
            foreach (var slot in GetSlots()) {
                if (items.Contains(slot.Item.CurrentValue)) {
                    slot.SetItem(null);
                }
            }
        }
    }
}
