using InventoryMerge.Gameplay.Data;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryMerge.Gameplay.Views.Inventory {
    public class InventoryView : MonoBehaviour {
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private RectTransform _objectsRoot;
        [SerializeField] private InventorySlotView _prefab;

        private InventoryViewsContainer _viewsContainer;

        public void BindData(IReadOnlyInventoryData data) {
            _viewsContainer = new(data, _prefab, _objectsRoot);
            _gridLayout.constraintCount = data.Size.x;
        }

        //It would be better to optimize preview functions and don't update every slot (even unaffected ones),
        //but it'll do for now
        public void ShowItemMergePreview(IInventoryItemData inventoryItem) {
            foreach (var slotView in _viewsContainer.GetElements()) {
                var hasSameItem = slotView.Item == inventoryItem;
                slotView.ShowPreview(hasSameItem ? PreviewSlotViewState.MergeItem : PreviewSlotViewState.Unaffected);
            }
        }

        public void ShowItemPlacedPreview(IInventoryItemData item, Vector2Int startingSlot) {
            var endSlot = item.GetEndIndex(startingSlot);
            foreach (var slotView in _viewsContainer.GetElements()) {
                if (!IsSlotAffected(slotView, item, startingSlot, endSlot)) {
                    slotView.ShowPreview(PreviewSlotViewState.Unaffected);
                    continue;
                }

                var hasItem = slotView.Item != null;
                slotView.ShowPreview(hasItem ? PreviewSlotViewState.ReplaceItem : PreviewSlotViewState.PlaceItem);
            }
        }

        public void ShowItemNotFitPreview(IInventoryItemData item, Vector2Int startingSlot) {
            var endSlot = item.GetEndIndex(startingSlot);
            foreach (var slotView in _viewsContainer.GetElements()) {
                var isAffected = IsSlotAffected(slotView, item, startingSlot, endSlot);
                slotView.ShowPreview(isAffected ? PreviewSlotViewState.DoesNotFitItem : PreviewSlotViewState.Unaffected);
            }
        }

        public void StopPreview() {
            foreach (var slotView in _viewsContainer.GetElements()) {
                slotView.StopPreview();
            }
        }

        public Vector2 GetSlotPosition(Vector2 approxIndex) {
            var scale = transform.lossyScale;
            var slotSize = _gridLayout.cellSize * scale;
            var spacing = _gridLayout.spacing * scale;
            var rect = _objectsRoot.GetWorldRect();
            
            var firstSlotPosition = rect.min + slotSize * 0.5f;
            var xCoord = firstSlotPosition.x + (slotSize.x * approxIndex.x + Mathf.Floor(approxIndex.x) * spacing.x);
            var yCoord = firstSlotPosition.y + slotSize.y * approxIndex.y + Mathf.Floor(approxIndex.y) * spacing.y;
            return new Vector2(xCoord, yCoord);
        }

        private bool IsSlotAffected(InventorySlotView slotView, IInventoryItemData item, Vector2Int startingSlot, Vector2Int endSlot) {
            if (!slotView.Index.IsInRange(startingSlot, endSlot)) {
                return false;
            }
            
            var itemSlot = item.Container.GetSlot(slotView.Index - startingSlot);
            return itemSlot.State == InventorySlotState.Available;
        }
    }
}
