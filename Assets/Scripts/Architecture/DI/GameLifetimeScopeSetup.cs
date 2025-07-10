using InventoryMerge.Architecture.StateMachine;
using InventoryMerge.Architecture.StateMachine.States;
using InventoryMerge.Gameplay.Factories;
using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Views.Inventory;
using InventoryMerge.Gameplay.Views.Items;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Architecture.DI {
    public class GameLifetimeScopeSetup : MonoBehaviour {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private ItemsHolderView _itemsHolderView;
        [SerializeField] private Transform _defaultItemsRoot;
        
        //I do not bind GameStateMachine states so there would be no way to get states outside of the StateMachine itself
        //(if you bind it via DI, you can technically access it anywhere)
        [Inject]
        private void Inject(IGameStateMachine stateMachine, IObjectResolver objectResolver) {
            var itemViewFactory = objectResolver.Resolve<IInventoryItemViewFactory>();
            itemViewFactory.Setup(_defaultItemsRoot, objectResolver);
            
            var viewsProvider = objectResolver.Resolve<IViewsProvider>();
            viewsProvider.Setup(_inventoryView, _itemsHolderView, _defaultItemsRoot);
            
            SetupStateMachine(stateMachine, objectResolver);
        }

        private void SetupStateMachine(IGameStateMachine stateMachine, IObjectResolver objectResolver) {
            stateMachine.Register(objectResolver.InjectNewObject<InitializationState>());
            stateMachine.Register(objectResolver.InjectNewObject<DataBindingState>());
            stateMachine.Register(objectResolver.InjectNewObject<ObjectSpawningState>());
            stateMachine.Register(objectResolver.InjectNewObject<GameplayState>());
            
            stateMachine.Enter<InitializationState>();
        }
    }
}
