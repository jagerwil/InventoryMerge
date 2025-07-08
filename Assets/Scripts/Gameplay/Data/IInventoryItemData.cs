using UnityEngine;

namespace InventoryMerge.Gameplay.Data {
    public interface IInventoryItemData {
        public IInventorySlotsDataContainer Container { get; }
        public Vector2 CenterIndex { get; }
    }
}
