
namespace InventoryMerge.Architecture.StateMachine {
    public interface IState {
        public void Enter();
        public void Exit() { }
    }
}
