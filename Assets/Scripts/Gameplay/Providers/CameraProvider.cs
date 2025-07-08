using UnityEngine;

namespace InventoryMerge.Gameplay.Providers {
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
