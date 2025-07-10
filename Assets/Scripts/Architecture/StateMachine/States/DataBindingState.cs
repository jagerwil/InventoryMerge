using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Services;
using VContainer;

namespace InventoryMerge.Architecture.StateMachine.States {
    public class DataBindingState : IState {
        private IGameStateMachine _stateMachine;
        private IInventoryService _inventoryService;
        private IInventoryViewProvider _inventoryViewProvider;
        
        [Inject]
        private void Inject(
            IGameStateMachine stateMachine, 
            IInventoryService inventoryService, 
            IInventoryViewProvider inventoryViewProvider) {
            _stateMachine = stateMachine;
            _inventoryService = inventoryService;
            _inventoryViewProvider = inventoryViewProvider;
        }
        
        public void Enter() {
            _inventoryViewProvider.InventoryView.BindData(_inventoryService.Data);
            _stateMachine.Enter<ObjectSpawningState>();
        }
    }
}