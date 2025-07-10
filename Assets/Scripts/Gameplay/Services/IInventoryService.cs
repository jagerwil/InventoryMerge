using System.Collections.Generic;
using InventoryMerge.Gameplay.Data;
using UnityEngine;

namespace InventoryMerge.Gameplay.Services {
    public interface IInventoryService {
        public IReadOnlyInventoryData Data { get; }
        public bool CanFitItem(IInventoryItemData item, Vector2 lerpSlotIndex);
        public bool TryFitItem(IInventoryItemData item, Vector2 lerpSlotIndex, out IEnumerable<IInventoryItemData> removedItems);
        
        public bool TryMergeItem(IInventoryItemData item, Vector2 lerpSlotIndex);
        public bool TryRemoveItem(IInventoryItemData item);
    }
}
