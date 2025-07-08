using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Data.Implementations;
using InventoryMerge.SObjects.Configs;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class InventoryService : IInventoryService {
        private readonly InventoryData _inventoryData;

        public IReadOnlyInventoryData Data => _inventoryData;

        [Inject]
        public InventoryService(InventoryConfig config) {
            _inventoryData = new InventoryData(config.Size);
        }

        public bool CanFitItem(IInventoryItemData item, Vector2 lerpSlotIndex) {
            var startingIndex = GetStartingIndex(item, lerpSlotIndex);
            return _inventoryData.CanFitItem(item, startingIndex);
        }

        public bool TryFitItem(IInventoryItemData item, Vector2 lerpSlotIndex) {
            var startingIndex = GetStartingIndex(item, lerpSlotIndex);
            return _inventoryData.TryFitItem(item, startingIndex);
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
