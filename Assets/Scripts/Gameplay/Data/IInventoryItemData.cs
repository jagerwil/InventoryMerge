using UnityEngine;

namespace InventoryMerge.Gameplay.Data {
    public interface IInventoryItemData {
        public InventoryItemId Id { get; }
        public int Level { get; }
        
        public IInventorySlotsDataContainer Container { get; }
        public Vector2 CenterIndex { get; }
    }
}
