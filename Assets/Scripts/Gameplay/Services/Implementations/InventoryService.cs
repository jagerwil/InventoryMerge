using System.Collections.Generic;
using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Data.Implementations;
using InventoryMerge.SObjects.Configs;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class InventoryService : IInventoryService {
        private readonly IInventoryItemMergeService _itemMergeService;
        private readonly InventoryData _inventoryData;

        public IReadOnlyInventoryData Data => _inventoryData;

        [Inject]
        public InventoryService(IInventoryItemMergeService itemMergeService, InventoryConfig config) {
            _itemMergeService = itemMergeService;
            _inventoryData = new InventoryData(config.DataContainer);
        }

        public bool CanFitItem(IInventoryItemData item, Vector2 lerpSlotIndex) {
            var startingIndex = GetStartingIndex(item, lerpSlotIndex);
            return _inventoryData.CanFitItem(item, startingIndex);
        }

        public bool TryFitItem(IInventoryItemData item, Vector2 lerpSlotIndex, out IEnumerable<IInventoryItemData> removedItems) {
            var startingIndex = GetStartingIndex(item, lerpSlotIndex);
            return _inventoryData.TryFitItem(item, startingIndex, out removedItems);
        }

        public bool TryMergeItem(IInventoryItemData item, Vector2 lerpSlotIndex) {
            var slotIndex = new Vector2Int(Mathf.RoundToInt(lerpSlotIndex.x), Mathf.RoundToInt(lerpSlotIndex.y));
            var slotItem = _inventoryData.GetSlot(slotIndex).Item.CurrentValue;

            return _itemMergeService.TryMerge(slotItem, item);
        }

        public bool TryRemoveItem(IInventoryItemData item) {
            return _inventoryData.TryRemoveItem(item);
        }

        private Vector2Int GetStartingIndex(IInventoryItemData item, Vector2 lerpSlotIndex) {
            var lerpStartingIndex = lerpSlotIndex - item.CenterIndex;
            return new Vector2Int(Mathf.RoundToInt(lerpStartingIndex.x), Mathf.RoundToInt(lerpStartingIndex.y));
        }
    }
}
