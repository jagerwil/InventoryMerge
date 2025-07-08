using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryMerge.Architecture.StateMachine {
    public class GameStateMachine : IGameStateMachine {
        private readonly Dictionary<Type, IState> _states = new Dictionary<Type, IState>();
        
        private IState _currentState;

        public void Register(IState state) {
            _states.Add(state.GetType(), state);
        }

        public void Enter<TState>() where TState : IState {
            if (!_states.TryGetValue(typeof(TState), out var state)) {
                Debug.LogError($"{GetType().Name}.{nameof(Enter)}(): {typeof(TState)} is not registered");
                return;
            }
            
            _currentState?.Exit();
            _currentState = state;
            state.Enter();
        }
    }
}
