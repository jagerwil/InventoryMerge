using UnityEngine;

namespace InventoryMerge.Utils.Initialization {
    public class TargetFramerateSetter : MonoBehaviour {
        [SerializeField] private int _targetFramerate = 60;

        private void Start() {
            Application.targetFrameRate = _targetFramerate;
        }
    }
}
