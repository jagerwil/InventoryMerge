using System;
using UnityEngine;
using UnityEngine.InputSystem;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class InputService : IInputService, IDisposable {
        private readonly PlayerInput _input;
        private Vector2 _touchPosition;

        private Vector2 CurrentTouchPosition => Touchscreen.current.position.ReadValue();
        
        public event Action<Vector2> OnTapStarted;
        public event Action<Vector2> OnTapEnded;
        public event Action OnTapCanceled;
        
        public event Action<Vector2> OnTouchPositionChanged;

        public InputService() {
            _input = new PlayerInput();
            _input.Player.TouchPosition.performed += TouchPositionChanged;
            _input.Player.TouchPhase.performed += TouchPhaseChanged;
        }

        public void Dispose() {
            Disable();
            _input?.Dispose();
        }

        public void Enable() {
            _input.Enable();
        }
        
        public void Disable() {
            _input.Disable();
        }

        private void TouchPhaseChanged(InputAction.CallbackContext ctx) {
            var phase = ctx.ReadValue<TouchPhase>();
            switch (phase) {
                case TouchPhase.Began:
                    OnTapStarted?.Invoke(CurrentTouchPosition);
                    break;
                case TouchPhase.Ended:
                    OnTapEnded?.Invoke(_touchPosition);
                    break;
                case TouchPhase.Canceled:
                    OnTapCanceled?.Invoke();
                    break;
                default:
                    break;
            }
        }

        private void TouchPositionChanged(InputAction.CallbackContext ctx) {
            _touchPosition = ctx.ReadValue<Vector2>();
            OnTouchPositionChanged?.Invoke(_touchPosition);
        }
    }
}
