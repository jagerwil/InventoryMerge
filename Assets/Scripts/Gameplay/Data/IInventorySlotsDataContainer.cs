using System.Collections.Generic;
using UnityEngine;

namespace InventoryMerge.Gameplay.Data {
    public interface IInventorySlotsDataContainer {
        public Vector2Int Size { get; }

        public void SetItem(IInventoryItemData item);

        public IInventorySlotData GetSlot(Vector2Int index);
        public IEnumerable<IInventorySlotData> GetSlots();
        
        public bool CanFit(IInventorySlotsDataContainer other, Vector2Int startingIndex);
        public bool TryFit(IInventorySlotsDataContainer other, Vector2Int startingIndex, out IEnumerable<IInventoryItemData> removedItems);
    }
}
