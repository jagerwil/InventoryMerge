using System;
using InventoryMerge.Gameplay.Views.Inventory;
using InventoryMerge.Gameplay.Views.Items;
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
        }

        public void Dispose() {
            Disable();
        }

        public void Enable() {
            _inputService.OnTapStarted += TapStarted;
            _inputService.OnTapEnded += TapEnded;
            _inputService.OnTapCanceled += TapCanceled;
        }

        public void Disable() {
            _inputService.OnTapStarted -= TapStarted;
            _inputService.OnTapEnded -= TapEnded;
            _inputService.OnTapCanceled -= TapCanceled;
        }

        private void TapStarted(Vector2 touchPosition) {
            _selectedItem = UiRaycaster.RaycastFirst<InventoryItemView>(touchPosition);
            if (_selectedItem) {
                _itemTransferService.TryDetachFromCurrentPlacement(_selectedItem);
                _moveUiWithTouchService.Attach(_selectedItem.transform);
            }
        }

        private void TapEnded(Vector2 touchPosition) {
            if (!_selectedItem) {
                return;
            }
            
            var slot = UiRaycaster.RaycastAny<InventorySlotView>(touchPosition);
            if (!slot) {
                _moveUiWithTouchService.Detach(_selectedItem.transform);
                _itemTransferService.AttachToHolder(_selectedItem);
                _selectedItem = null;
                return;
            }
            
            var approxIndex = slot.GetApproxIndex(touchPosition);
            _moveUiWithTouchService.Detach(_selectedItem.transform);
            if (!_itemTransferService.TryAttachToInventory(_selectedItem, approxIndex)) {
                _itemTransferService.AttachToHolder(_selectedItem);
            }
            _selectedItem = null;
        }

        private void TapCanceled() {
            _selectedItem = null;
        }
    }
}
