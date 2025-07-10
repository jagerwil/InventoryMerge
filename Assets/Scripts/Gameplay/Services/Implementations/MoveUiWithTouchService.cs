using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class MoveUiWithTouchService : IMoveUiWithTouchService, IDisposable {
        private readonly HashSet<Transform> _objects = new();

        private readonly IInputService _inputService;
        
        [Inject]
        public MoveUiWithTouchService(IInputService inputService) {
            _inputService = inputService;
            _inputService.OnTouchPositionChanged += TouchPositionChanged;
        }

        public void Dispose() {
            _inputService.OnTouchPositionChanged -= TouchPositionChanged;
        }
        
        public void Attach(Transform obj) {
            _objects.Add(obj);
        }

        public void Detach(Transform obj) {
            _objects.Remove(obj);
        }

        private void TouchPositionChanged(Vector2 touchPosition) {
            foreach (var obj in _objects) {
                obj.position = touchPosition;
            }
        }
    }
}
