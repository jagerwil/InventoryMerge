using InventoryMerge.Gameplay.Factories;
using InventoryMerge.SObjects.Configs;
using VContainer;

namespace InventoryMerge.Architecture.StateMachine.States {
    public class ObjectSpawningState : IState {
        private IGameStateMachine _stateMachine;
        private IInventoryItemViewFactory _itemViewFactory;
        private ItemsSpawnConfig _itemsSpawnConfig;

        [Inject]
        private void Inject(
            IGameStateMachine stateMachine,
            IInventoryItemViewFactory itemViewFactory,
            ItemsSpawnConfig itemsSpawnConfig) {
            _stateMachine = stateMachine;
            _itemViewFactory = itemViewFactory;
            _itemsSpawnConfig = itemsSpawnConfig;
        }
        
        public void Enter() {
            var items = _itemsSpawnConfig.Items;
            foreach (var item in items) {
                _itemViewFactory.Spawn(item.Id, item.Level);
            }
            
            _stateMachine.Enter<GameplayState>();
        }
    }
}
