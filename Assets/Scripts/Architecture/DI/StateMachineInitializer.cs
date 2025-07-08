using InventoryMerge.Architecture.StateMachine;
using InventoryMerge.Architecture.StateMachine.States;
using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Services;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Architecture.DI {
    public class StateMachineInitializer : MonoBehaviour {
        //I do not bind GameStateMachine states so there would be no way to get states outside of the StateMachine itself
        //(if you bind it via DI, you can technically access it anywhere)
        [Inject]
        private void Inject(IGameStateMachine stateMachine, 
            IInventoryService inventoryService, 
            IInventoryDragDropService dragDropService, 
            IViewsProvider viewsProvider) {
            stateMachine.Register(new InitializationState(stateMachine, dragDropService));
            stateMachine.Register(new DataBindingState(inventoryService, viewsProvider));
            
            stateMachine.Enter<InitializationState>();
        }
    }
}
