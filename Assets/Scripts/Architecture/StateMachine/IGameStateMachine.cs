
namespace InventoryMerge.Architecture.StateMachine {
    public interface IGameStateMachine {
        public void Register(IState state);
        public void Enter<TState>() where TState : IState;
    }
}
