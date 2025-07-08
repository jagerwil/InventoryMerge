using R3;
using UnityEngine;

namespace InventoryMerge.Gameplay.Data {
    public interface IInventorySlotData {
        public Vector2Int Index { get; }
        public InventorySlotState State { get; }
        
        ReadOnlyReactiveProperty<IInventoryItemData> Item { get; }

        public void SetItem(IInventoryItemData item);
    }

    public enum InventorySlotState {
        Available = 0,
        Disabled = 1,
    }
}
