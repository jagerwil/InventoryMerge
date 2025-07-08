using InventoryMerge.Gameplay.Data;
using UnityEngine;

namespace InventoryMerge.Gameplay.Services {
    public interface IInventoryService {
        public IReadOnlyInventoryData Data { get; }
        
        public bool CanFitItem(IInventoryItemData item, Vector2 lerpSlotIndex);
        public bool TryFitItem(IInventoryItemData item, Vector2 lerpSlotIndex);

        public bool TryRemoveItem(IInventoryItemData item);
    }
}
