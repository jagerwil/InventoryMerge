using System.Collections.Generic;
using UnityEngine;

namespace InventoryMerge.Gameplay.Data {
    public interface IReadOnlyInventoryData {
        public Vector2Int Size { get; }
        
        public IInventorySlotData GetSlot(Vector2Int index);
        public IEnumerable<IInventorySlotData> GetSlots();

        public bool CanFitItem(IInventoryItemData item, Vector2Int startingIndex);
    }
}
