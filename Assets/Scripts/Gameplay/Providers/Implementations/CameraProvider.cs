using UnityEngine;

namespace InventoryMerge.Gameplay.Providers.Implementations {
    public class CameraProvider : ICameraProvider {
        private Camera _camera;

        public Camera Camera { 
            get {
                if (!_camera) {
                    _camera = Camera.main;
                }
                return _camera;
            }   
        }
    }
}
