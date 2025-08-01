using R3;
using UnityEngine;

namespace InventoryMerge.Gameplay.Data.Implementations {
    public class InventorySlotData : IInventorySlotData {
        private readonly ReactiveProperty<IInventoryItemData> _item = new();
        
        public Vector2Int Index { get; private set; }
        public InventorySlotState State { get; private set; }
        public ReadOnlyReactiveProperty<IInventoryItemData> Item => _item;

        public InventorySlotData(Vector2Int index, InventorySlotState state) {
            Index = index;
            State = state;
        }

        public void SetItem(IInventoryItemData item) {
            if (State == InventorySlotState.Available) {
                _item.Value = item;
            }
        }
    }
}
