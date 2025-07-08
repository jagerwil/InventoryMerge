using System;
using InventoryMerge.Gameplay.Views;
using InventoryMerge.Utils.UI;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class InventoryDragDropService : IInventoryDragDropService, IDisposable {
        private readonly IInputService _inputService;
        private readonly IInventoryItemTransferService _itemTransferService;
        private readonly IMoveUiWithTouchService _moveUiWithTouchService;
        
        private InventoryItemView _selectedItem;
        
        [Inject]
        public InventoryDragDropService(IInputService inputService, 
                                        IInventoryItemTransferService itemTransferService,
                                        IMoveUiWithTouchService moveUiWithTouchService) {
            _inputService = inputService;
            _itemTransferService = itemTransferService;
            _moveUiWithTouchService = moveUiWithTouchService;
            
            _inputService.OnTapStarted += TapStarted;
            _inputService.OnTapEnded += TapEnded;
            _inputService.OnTapCanceled += TapCanceled;
        }

        public void Dispose() {
            _inputService.OnTapStarted -= TapStarted;
            _inputService.OnTapEnded -= TapEnded;
            _inputService.OnTapCanceled -= TapCanceled;
        }

        private void TapStarted(Vector2 touchPosition) {
            Debug.Log("[DragDrop] Tap started!");
            _selectedItem = UiRaycaster.RaycastFirst<InventoryItemView>(touchPosition);
            if (_selectedItem) {
                Debug.Log("[DragDrop] Tap started with a selected item, wowee");
                _itemTransferService.TryDetachFromCurrentPlacement(_selectedItem);
                _moveUiWithTouchService.Attach(_selectedItem.transform);
            }
        }

        private void TapEnded(Vector2 touchPosition) {
            Debug.Log("[DragDrop] Tap ended");
            if (!_selectedItem) {
                return;
            }
            
            var slot = UiRaycaster.RaycastAny<InventorySlotView>(touchPosition);
            if (!slot) {
                Debug.Log("[DragDrop] Tap ended with no slot in sight, sadge...");
                _moveUiWithTouchService.Detach(_selectedItem.transform);
                _selectedItem = null;
                return;
            }
            
            var slotOffset = slot.GetCenterOffsetRatio(touchPosition);
            _moveUiWithTouchService.Detach(_selectedItem.transform);
            Debug.Log($"[DragDrop] Attaching item to inventory! Slot index: {slot.Index}, slotOffset: {slotOffset}");
            _itemTransferService.TryAttachToInventory(_selectedItem, slot.Index + slotOffset);
            _selectedItem = null;
        }

        private void TapCanceled() {
            _selectedItem = null;
        }
    }
}
