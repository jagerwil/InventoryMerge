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
            item.transform.SetParent(_dragDropItemsRoot);
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

            var inventoryView = _inventoryViewProvider.InventoryView;
            item.transform.position = inventoryView.GetSlotPosition(roundedSlotIndex);
            return true;
        }

        public void AttachToHolder(InventoryItemView item) {
            item.transform.SetParent(_itemsHolderRoot);
            item.transform.localPosition = Vector3.zero;
        }

        public bool TryDetachFromCurrentPlacement(InventoryItemView item) {
            if (!item) {
                return false;
            }

            item.transform.SetParent(_defaultItemsRoot);
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
