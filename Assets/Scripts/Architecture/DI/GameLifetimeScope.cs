using InventoryMerge.Architecture.StateMachine;
using InventoryMerge.Gameplay.Factories;
using InventoryMerge.Gameplay.Factories.Implementations;
using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Providers.Implementations;
using InventoryMerge.Gameplay.Services;
using InventoryMerge.Gameplay.Services.Implementations;
using InventoryMerge.SObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace InventoryMerge.Architecture.DI {
    public class GameLifetimeScope : LifetimeScope {
        [Header("Configs")]
        [SerializeField] private GameConfigSO _config;

        protected override void Configure(IContainerBuilder builder) {
            //Register configs & databases
            builder.RegisterInstance(_config.Inventory);
            builder.RegisterInstance(_config.ItemsSpawning);
            builder.RegisterInstance(_config.ItemsDatabase.Data);
            
            //Register factories
            builder.Register<IInventoryItemViewFactory, InventoryItemViewFactory>(Lifetime.Singleton);
            
            //Register providers
            builder.Register<ICameraProvider, CameraProvider>(Lifetime.Singleton);
            builder.Register<IInventoryViewProvider, InventoryViewProvider>(Lifetime.Singleton);
            builder.Register<IInventoryItemViewsProvider, InventoryItemViewsProvider>(Lifetime.Singleton);
            
            //Register services
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
            builder.Register<IInventoryService, InventoryService>(Lifetime.Singleton);
            builder.Register<IInventoryItemMergeService, InventoryItemMergeService>(Lifetime.Singleton);
            builder.Register<IInventoryDragDropService, InventoryDragDropService>(Lifetime.Singleton);
            builder.Register<IInventoryItemTransferService, InventoryItemTransferService>(Lifetime.Singleton);
            builder.Register<IMoveUiWithTouchService, MoveUiWithTouchService>(Lifetime.Singleton);
            builder.Register<IInventoryPreviewService, InventoryPreviewService>(Lifetime.Singleton);

            //Register state machine
            builder.Register<IGameStateMachine, GameStateMachine>(Lifetime.Singleton);
        }
    }
}
