using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Services;
using VContainer;

namespace InventoryMerge.Architecture.StateMachine.States {
    public class DataBindingState : IState {
        private IGameStateMachine _stateMachine;
        private IInventoryService _inventoryService;
        private IViewsProvider _viewsProvider;
        
        [Inject]
        private void Inject(
            IGameStateMachine stateMachine, 
            IInventoryService inventoryService, 
            IViewsProvider viewsProvider) {
            _stateMachine = stateMachine;
            _inventoryService = inventoryService;
            _viewsProvider = viewsProvider;
        }
        
        public void Enter() {
            _viewsProvider.InventoryView.BindData(_inventoryService.Data);
            _stateMachine.Enter<ObjectSpawningState>();
        }
    }
}