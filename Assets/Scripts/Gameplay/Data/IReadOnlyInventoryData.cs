using InventoryMerge.Gameplay.Data.Implementations;
using UnityEngine;

namespace InventoryMerge.Gameplay.Data {
    public interface IReadOnlyInventoryData {
        public Vector2Int Size { get; }
        
        public InventorySlotData GetSlot(Vector2Int index);

        public bool CanFitItem(IInventoryItemData item, Vector2Int startingIndex);
    }
}
