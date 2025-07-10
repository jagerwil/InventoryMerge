using InventoryMerge.Gameplay.Views.Inventory;
using InventoryMerge.Gameplay.Views.Items;
using UnityEngine;

namespace InventoryMerge.Gameplay.Services {
    public interface IInventoryItemTransferService {
        public bool TryAttachToInventory(InventoryItemView item, Vector2 approxSlotIndex);
        public void AttachToHolder(InventoryItemView item);
        
        public bool TryDetachFromCurrentPlacement(InventoryItemView item);
    }
}
