using System;
using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Views.Inventory;
using InventoryMerge.Gameplay.Views.Items;
using InventoryMerge.Utils.UI;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class InventoryPreviewService : IInventoryPreviewService, IDisposable {
        private readonly IInputService _inputService;
        private readonly IInventoryService _inventoryService;
        private readonly IInventoryViewProvider _inventoryViewProvider;
        
        private InventoryItemView _selectedItem;
        
        private bool _isPreviewing;

        [Inject]
        public InventoryPreviewService(
            IInputService inputService,
            IInventoryService inventoryService,
            IInventoryViewProvider inventoryViewProvider) {
            _inputService = inputService;
            _inventoryService = inventoryService;
            _inventoryViewProvider = inventoryViewProvider;

        }

        public void Dispose() {
            Disable();
        }

        public void Enable() {
            _inputService.OnTapStarted += TapStarted;
            _inputService.OnTapEnded += TapEnded;
            _inputService.OnTapCanceled += TapCancelled;
            _inputService.OnTouchPositionChanged += TouchPositionChanged;
        }
        
        public void Disable() {
            _inputService.OnTapStarted -= TapStarted;
            _inputService.OnTapEnded -= TapEnded;
            _inputService.OnTapCanceled -= TapCancelled;
            _inputService.OnTouchPositionChanged -= TouchPositionChanged;
        }

        private void TapStarted(Vector2 touchPosition) {
            _selectedItem = UiRaycaster.RaycastFirst<InventoryItemView>(touchPosition);
            
            TryShowPreview(touchPosition);
        }

        private void TapEnded(Vector2 touchPosition) {
            StopPreview();
        }

        private void TapCancelled() {
            StopPreview();
        }

        private void TouchPositionChanged(Vector2 touchPosition) {
            TryShowPreview(touchPosition);
        }

        private void TryShowPreview(Vector2 touchPosition) {
            if (_selectedItem == null) {
                return;
            }

            var slot = UiRaycaster.RaycastAny<InventorySlotView>(touchPosition);
            if (!slot) {
                StopPreview();
                return;
            }

            var approxIndex = slot.GetApproxIndex(touchPosition);
            StartPreview(_selectedItem.Data, approxIndex, slot.Item);
        }

        private void StopPreview() {
            if (!_isPreviewing) {
                return;
            }
            
            _inventoryViewProvider.InventoryView.StopPreview();
            _isPreviewing = false;
        }
        
        public void StartPreview(IInventoryItemData item, Vector2 approxSlotIndex, IInventoryItemData targetItem) {
            var result = _inventoryService.GetItemPlacementResult(item, approxSlotIndex);

            var inventoryView = _inventoryViewProvider.InventoryView;
            switch (result) {
                case InventoryItemPlacementResultType.FitItem:
                    inventoryView.ShowItemPlacedPreview(item, item.GetStartIndex(approxSlotIndex));
                    break;
                case InventoryItemPlacementResultType.MergeItem:
                    inventoryView.ShowItemMergePreview(targetItem);
                    break;
                case InventoryItemPlacementResultType.NoResult:
                    inventoryView.ShowItemNotFitPreview(item, item.GetStartIndex(approxSlotIndex));
                    break;
                default:
                    Debug.LogError($"{GetType().Name}.{nameof(StartPreview)}(): ResultType \"{result.ToString()}\" is not supported");
                    StopPreview();
                    return;
            }
            
            _isPreviewing = true;
        }
    }
}
