using R3;
using UnityEngine;

namespace InventoryMerge.Gameplay.Data {
    public interface IInventoryItemData {
        public InventoryItemId Id { get; }
        public ReadOnlyReactiveProperty<int> Level { get; }
        
        public IInventorySlotsDataContainer Container { get; }
        public Vector2 CenterIndex { get; }

        public void IncreaseLevel();
    }
}
