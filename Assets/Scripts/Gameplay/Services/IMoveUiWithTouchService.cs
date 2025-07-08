using UnityEngine;

namespace InventoryMerge.Gameplay.Services {
    public interface IMoveUiWithTouchService {
        public void Attach(Transform obj);
        public void Detach(Transform obj);
    }
}
