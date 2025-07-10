using System.Collections.Generic;
using InventoryMerge.Gameplay.Data;
using UnityEngine;

namespace InventoryMerge.Gameplay.Services {
    public interface IInventoryService {
        public IReadOnlyInventoryData Data { get; }

        public InventoryItemPlacementResultType GetItemPlacementResult(IInventoryItemData item, Vector2 approxSlotIndex);

        public bool TryPlaceItem(IInventoryItemData item, Vector2 approxSlotIndex, out IEnumerable<IInventoryItemData> removedItems);
        public bool TryMergeItem(IInventoryItemData item, Vector2 approxSlotIndex);
        public bool TryRemoveItem(IInventoryItemData item);
    }

    public enum InventoryItemPlacementResultType {
        PlaceItem = 0,
        MergeItem = 1,
        NoResult = 2,
    }
}
