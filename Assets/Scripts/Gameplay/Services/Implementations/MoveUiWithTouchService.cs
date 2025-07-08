using System;
using System.Collections.Generic;
using InventoryMerge.Gameplay.Providers;
using UnityEngine;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class MoveUiWithTouchService : IMoveUiWithTouchService, IDisposable {
        private readonly HashSet<Transform> _objects = new();
        
        private readonly ICameraProvider _cameraProvider;
        private readonly IInputService _inputService;
        
        public MoveUiWithTouchService(ICameraProvider cameraProvider, IInputService inputService) {
            _cameraProvider = cameraProvider;
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
            if (_objects.Count > 0) {
                Debug.Log("Objects are moving bruv");
            }
            foreach (var obj in _objects) {
                obj.position = touchPosition;
            }
        }
    }
}
