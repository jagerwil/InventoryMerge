using InventoryMerge.Architecture.StateMachine;
using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Services;
using InventoryMerge.Gameplay.Services.Implementations;
using InventoryMerge.Gameplay.Views;
using InventoryMerge.SObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace InventoryMerge.Architecture.DI {
    public class GameLifetimeScope : LifetimeScope {
        [Header("Views")]
        [SerializeField] private InventoryView _inventoryView;
        [Header("Configs")]
        [SerializeField] private GameConfig _config;

        protected override void Configure(IContainerBuilder builder) {
            //Register configs
            builder.RegisterInstance(_config.Inventory);
            
            //Register providers
            builder.Register<ICameraProvider, CameraProvider>(Lifetime.Singleton);
            builder.RegisterInstance<IViewsProvider>(new ViewsProvider(_inventoryView));
            
            //Register services
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
            builder.Register<IInventoryService, InventoryService>(Lifetime.Singleton);
            builder.Register<IInventoryDragDropService, InventoryDragDropService>(Lifetime.Singleton);
            builder.Register<IInventoryItemTransferService, InventoryItemTransferService>(Lifetime.Singleton);
            builder.Register<IMoveUiWithTouchService, MoveUiWithTouchService>(Lifetime.Singleton);

            //Register state machine
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
        }
    }
}
