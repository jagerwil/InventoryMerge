using UnityEngine;

namespace InventoryMerge.Gameplay.Data.Implementations {
    public class InventoryItemData : IInventoryItemData {
        public IInventorySlotsDataContainer Container { get; private set; }
        public Vector2 CenterIndex { get; private set; }

        public InventoryItemData(IInventorySlotsDataContainer container) {
            Container = container;
            CenterIndex = (container.Size - Vector2.one) * 0.5f;
        }
    }
}
