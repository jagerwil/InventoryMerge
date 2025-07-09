using Cysharp.Threading.Tasks;
using InventoryMerge.Gameplay.Services;
using VContainer;

namespace InventoryMerge.Architecture.StateMachine.States {
    public class InitializationState : IState {
        private IGameStateMachine _stateMachine;
        
        [Inject]
        private void Inject(IGameStateMachine stateMachine, IInventoryDragDropService dragDropService) {
            _stateMachine = stateMachine;
        }
        
        public void Enter() {
            StartDataBindingNextFrame().Forget();
        }

        private async UniTask StartDataBindingNextFrame() {
            await UniTask.WaitForEndOfFrame();
            
            _stateMachine.Enter<DataBindingState>();
        }
    }
}
