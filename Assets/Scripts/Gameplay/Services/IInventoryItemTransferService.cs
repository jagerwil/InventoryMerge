using InventoryMerge.Gameplay.Views.Inventory;
using UnityEngine;

namespace InventoryMerge.Gameplay.Services {
    public interface IInventoryItemTransferService {
        public bool TryAttachToInventory(InventoryItemView item, Vector2 lerpSlotIndex);
        public void AttachToHolder(InventoryItemView item);
        
        public bool TryDetachFromCurrentPlacement(InventoryItemView item);
    }
}
