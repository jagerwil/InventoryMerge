using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Views.Inventory;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class InventoryItemTransferService : IInventoryItemTransferService {
        private readonly IInventoryService _inventoryService;
        private readonly IViewsProvider _viewsProvider;
        private readonly IInventoryItemViewsProvider _itemViewsProvider;

        [Inject]
        public InventoryItemTransferService(
            IInventoryService inventoryService,
            IViewsProvider viewsProvider,
            IInventoryItemViewsProvider itemViewsProvider) {
            _inventoryService = inventoryService;
            _viewsProvider = viewsProvider;
            _itemViewsProvider = itemViewsProvider;
        }

        public bool TryAttachToInventory(InventoryItemView item, Vector2 approxSlotIndex) {
            var itemCenterIndex = item.Data.CenterIndex;

            var roundedSlotIndex = Vector2.zero;
            roundedSlotIndex.x = RoundItemCenterIndex(approxSlotIndex.x, itemCenterIndex.x);
            roundedSlotIndex.y = RoundItemCenterIndex(approxSlotIndex.y, itemCenterIndex.y);

            if (_inventoryService.TryMergeItem(item.Data, roundedSlotIndex)) {
                return true;
            }

            if (!_inventoryService.TryPlaceItem(item.Data, roundedSlotIndex, out var removedItems)) {
                Debug.Log($"[ItemTransfer] ITEM NOT FIT! item size: {item.Data.Container.Size}, position: {roundedSlotIndex}");
                return false;
            }

            foreach (var removedItem in removedItems) {
                var itemView = _itemViewsProvider.GetItemView(removedItem);
                if (itemView) {
                    AttachToHolder(itemView);
                }
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

        private float RoundItemCenterIndex(float approxIndex, float itemCenterIndex) {
            if (Mathf.Approximately(itemCenterIndex, Mathf.Round(itemCenterIndex))) {
                return Mathf.Round(approxIndex);
            }
            
            return Mathf.Round(approxIndex + 0.5f) - 0.5f;
        }
    }
}
