using InventoryMerge.Gameplay.Views.Items;
using UnityEngine;

namespace InventoryMerge.Gameplay.Services {
    public interface IInventoryItemTransferService {
        public void Setup(Transform defaultItemsRoot, Transform itemsHolderRoot, Transform dragDropItemsRoot);

        public void AttachToDragDrop(InventoryItemView item);
        public bool TryAttachToInventory(InventoryItemView item, Vector2 approxSlotIndex);
        public void AttachToHolder(InventoryItemView item);
        
        public bool TryDetachFromCurrentPlacement(InventoryItemView item);
    }
}
