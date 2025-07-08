using System;
using System.Collections.Generic;
using InventoryMerge.Utils.Data;
using UnityEngine;

namespace InventoryMerge.Gameplay.Data.Implementations {
    [Serializable]
    public class InventorySlotsDataContainer : GridContainer<InventorySlotData>, IInventorySlotsDataContainer {
        public InventorySlotsDataContainer(Vector2Int size) {
            Initialize(size);
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

                if (localSlot.State != InventorySlotState.Available
                    || localSlot.Item.CurrentValue != null) {
                    return false;
                }
            }

            return true;
        }
        
        public bool TryFit(IInventorySlotsDataContainer other, Vector2Int startingIndex) {
            if (!CanFit(other, startingIndex)) {
                return false;
            }

            foreach (var slot in other.GetSlots()) {
                if (slot.State != InventorySlotState.Available) {
                    continue;
                }
                
                var slotIndex = slot.Index + startingIndex;
                var localSlot = GetSlot(slotIndex);
                localSlot.SetItem(slot.Item.CurrentValue);
            }

            return true;
        }
    }
}
