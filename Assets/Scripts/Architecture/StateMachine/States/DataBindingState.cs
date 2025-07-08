using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Services;

namespace InventoryMerge.Architecture.StateMachine.States {
    public class DataBindingState : IState {
        private readonly IInventoryService _inventoryService;
        private readonly IViewsProvider _viewsProvider;
        
        public DataBindingState(IInventoryService inventoryService, IViewsProvider viewsProvider) {
            _inventoryService = inventoryService;
            _viewsProvider = viewsProvider;
        }
        
        public void Enter() {
            _viewsProvider.InventoryView.BindData(_inventoryService.Data);
        }
    }
}