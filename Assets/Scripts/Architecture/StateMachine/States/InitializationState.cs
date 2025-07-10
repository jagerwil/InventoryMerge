using Cysharp.Threading.Tasks;
using VContainer;

namespace InventoryMerge.Architecture.StateMachine.States {
    public class InitializationState : IState {
        private IGameStateMachine _stateMachine;
        
        [Inject]
        private void Inject(
            IGameStateMachine stateMachine) {
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
