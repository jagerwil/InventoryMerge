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

        public InventoryItemPlacementResultType GetItemPlacementResult(IInventoryItemData item, Vector2 approxSlotIndex) {
            if (CanMergeItem(item, approxSlotIndex)) {
                return InventoryItemPlacementResultType.MergeItem;
            }

            if (CanFitItem(item, approxSlotIndex)) {
                return InventoryItemPlacementResultType.FitItem;
            }
            
            return InventoryItemPlacementResultType.NoResult;
        }

        public InventoryItemPlacementResultType TryPlaceItem(IInventoryItemData item, Vector2 approxSlotIndex, out IEnumerable<IInventoryItemData> removedItems) {
            if (TryMergeItem(item, approxSlotIndex)) {
                removedItems = null;
                return InventoryItemPlacementResultType.MergeItem;
            }

            if (TryFitItem(item, approxSlotIndex, out removedItems)) {
                return InventoryItemPlacementResultType.FitItem;
            }
            
            return InventoryItemPlacementResultType.NoResult;
        }

        public bool TryRemoveItem(IInventoryItemData item) {
            return _inventoryData.TryRemoveItem(item);
        }

        private bool CanFitItem(IInventoryItemData item, Vector2 approxSlotIndex) {
            var startingIndex = item.GetStartIndex(approxSlotIndex);
            return _inventoryData.CanFitItem(item, startingIndex);
        }

        private bool CanMergeItem(IInventoryItemData item, Vector2 approxSlotIndex) {
            var slotIndex = approxSlotIndex.RoundToInt();
            var slotItem = _inventoryData.GetSlot(slotIndex).Item.CurrentValue;

            return _itemMergeService.CanMerge(slotItem, item);
        }

        private bool TryFitItem(IInventoryItemData item, Vector2 approxSlotIndex, 
            out IEnumerable<IInventoryItemData> removedItems) {
            var startingIndex = item.GetStartIndex(approxSlotIndex);
            return _inventoryData.TryFitItem(item, startingIndex, out removedItems);
        }

        private bool TryMergeItem(IInventoryItemData item, Vector2 approxSlotIndex) {
            var slotIndex = approxSlotIndex.RoundToInt();
            var slotItem = _inventoryData.GetSlot(slotIndex).Item.CurrentValue;

            return _itemMergeService.TryMerge(slotItem, item);
        }
    }
}
