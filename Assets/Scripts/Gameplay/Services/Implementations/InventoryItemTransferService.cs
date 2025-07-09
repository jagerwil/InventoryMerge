using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Views.Inventory;
using UnityEngine;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class InventoryItemTransferService : IInventoryItemTransferService {
        private readonly IInventoryService _inventoryService;
        private readonly IViewsProvider _viewsProvider;

        public InventoryItemTransferService(
            IInventoryService inventoryService,
            IViewsProvider viewsProvider) {
            _inventoryService = inventoryService;
            _viewsProvider = viewsProvider;
        }

        public bool TryAttachToInventory(InventoryItemView item, Vector2 lerpSlotIndex) {
            var itemCenterIndex = item.Data.CenterIndex;

            var roundedSlotIndex = Vector2.zero;
            roundedSlotIndex.x = RoundItemCenterIndex(lerpSlotIndex.x, itemCenterIndex.x);
            roundedSlotIndex.y = RoundItemCenterIndex(lerpSlotIndex.y, itemCenterIndex.y);

            if (!_inventoryService.TryFitItem(item.Data, roundedSlotIndex)) {
                Debug.Log($"[ItemTransfer] ITEM NOT FIT! item size: {item.Data.Container.Size}, position: {roundedSlotIndex}");
                return false;
            }

            var inventoryView = _viewsProvider.InventoryView;
            item.transform.position = inventoryView.GetSlotPosition(roundedSlotIndex);
            return true;
        }

        public void AttachToHolder(InventoryItemView item) {
            _viewsProvider.ItemsHolderView.PlaceItem(item);
        }

        public bool TryDetachFromCurrentPlacement(InventoryItemView item) {
            if (!item) {
                return false;
            }

            item.transform.SetParent(_viewsProvider.DefaultItemsRoot);
            _inventoryService.TryRemoveItem(item.Data);
            return true;
        }

        private float RoundItemCenterIndex(float lerpIndex, float itemCenterIndex) {
            if (Mathf.Approximately(itemCenterIndex, Mathf.Round(itemCenterIndex))) {
                return Mathf.Round(lerpIndex);
            }
            
            return Mathf.Round(lerpIndex + 0.5f) - 0.5f;
        }
    }
}
