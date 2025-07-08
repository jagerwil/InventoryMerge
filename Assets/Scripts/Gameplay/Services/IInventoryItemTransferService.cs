using InventoryMerge.Gameplay.Views;
using UnityEngine;

namespace InventoryMerge.Gameplay.Services {
    public interface IInventoryItemTransferService {
        public bool TryAttachToInventory(InventoryItemView item, Vector2 lerpSlotIndex);
        public bool TryDetachFromCurrentPlacement(InventoryItemView item);
    }
}
