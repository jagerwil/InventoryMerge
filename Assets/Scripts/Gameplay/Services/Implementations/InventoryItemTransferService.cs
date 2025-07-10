using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Views.Items;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class InventoryItemTransferService : IInventoryItemTransferService {
        private readonly IInventoryService _inventoryService;
        private readonly IInventoryViewProvider _inventoryViewProvider;
        private readonly IInventoryItemViewsProvider _itemViewsProvider;

        private Transform _defaultItemsRoot;
        private Transform _itemsHolderRoot;
        private Transform _dragDropItemsRoot;

        [Inject]
        public InventoryItemTransferService(
            IInventoryService inventoryService,
            IInventoryViewProvider inventoryViewProvider,
            IInventoryItemViewsProvider itemViewsProvider) {
            _inventoryService = inventoryService;
            _inventoryViewProvider = inventoryViewProvider;
            _itemViewsProvider = itemViewsProvider;
        }

        public void Setup(Transform defaultItemsRoot, Transform itemsHolderRoot, Transform dragDropItemsRoot) {
            _defaultItemsRoot = defaultItemsRoot;
            _itemsHolderRoot = itemsHolderRoot;
            _dragDropItemsRoot = dragDropItemsRoot;
        }

        public void AttachToDragDrop(InventoryItemView item) {
            SetItemParent(item, _dragDropItemsRoot);
        }

        public bool TryAttachToInventory(InventoryItemView item, Vector2 approxSlotIndex) {
            var itemCenterIndex = item.Data.CenterIndex;

            var result = _inventoryService.TryPlaceItem(item.Data, approxSlotIndex, out var removedItems);
            if (result == InventoryItemPlacementResultType.NoResult) {
                return false;
            }

            if (result == InventoryItemPlacementResultType.MergeItem) {
                return true;
            }

            foreach (var removedItem in removedItems) {
                var itemView = _itemViewsProvider.GetItemView(removedItem);
                if (itemView) {
                    AttachToHolder(itemView);
                }
            }

            var roundedSlotIndex = Vector2.zero;
            roundedSlotIndex.x = RoundItemCenterIndex(approxSlotIndex.x, itemCenterIndex.x);
            roundedSlotIndex.y = RoundItemCenterIndex(approxSlotIndex.y, itemCenterIndex.y);

            var inventoryView = _inventoryViewProvider.InventoryView;
            SetItemParent(item, _defaultItemsRoot, false);
            item.transform.position = inventoryView.GetSlotPosition(roundedSlotIndex);
            return true;
        }

        public void AttachToHolder(InventoryItemView item) {
            SetItemParent(item, _itemsHolderRoot);
            item.transform.localPosition = Vector3.zero;
        }

        public bool TryDetachFromCurrentPlacement(InventoryItemView item) {
            if (!item) {
                return false;
            }

            SetItemParent(item, _defaultItemsRoot);
            return true;
        }

        private void SetItemParent(InventoryItemView item, Transform parent, bool removeItemFromInventory = true) {
            item.transform.SetParent(parent);
            if (removeItemFromInventory) {
                _inventoryService.TryRemoveItem(item.Data);
            }
        }

        private float RoundItemCenterIndex(float approxIndex, float itemCenterIndex) {
            if (Mathf.Approximately(itemCenterIndex, Mathf.Round(itemCenterIndex))) {
                return Mathf.Round(approxIndex);
            }
            
            return Mathf.Round(approxIndex + 0.5f) - 0.5f;
        }
    }
}
