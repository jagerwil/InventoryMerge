using InventoryMerge.Gameplay.Services;
using InventoryMerge.Gameplay.Services.Implementations;
using InventoryMerge.SObjects;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace InventoryMerge.Architecture {
    public class GameLifetimeScope : LifetimeScope {
        [SerializeField] private GameConfig _config;
        
        protected override void Configure(IContainerBuilder builder) {
            builder.RegisterInstance(_config);
            
            builder.Register<IInputService, InputService>(Lifetime.Singleton);
            builder.Register<IInventoryService, InventoryService>(Lifetime.Singleton);
        }
    }
}
