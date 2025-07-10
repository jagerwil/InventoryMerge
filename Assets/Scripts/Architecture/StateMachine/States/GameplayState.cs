using InventoryMerge.Gameplay.Services;
using VContainer;

namespace InventoryMerge.Architecture.StateMachine.States {
    public class GameplayState : IState {
        private IInputService _inputService;
        private IInventoryDragDropService _dragDropService;
        private IInventoryPreviewService _previewService;
        
        [Inject]
        private void Inject(IInputService inputService,
            IInventoryDragDropService dragDropService,
            IInventoryPreviewService previewService) {
            _inputService = inputService;
            _dragDropService = dragDropService;
            _previewService = previewService;
        }
        
        public void Enter() {
            _inputService.Enable();
            _dragDropService.Enable();
            _previewService.Enable();
        }

        public void Exit() {
            _inputService.Disable();
            _dragDropService.Disable();
            _previewService.Disable();
        }
    }
}
