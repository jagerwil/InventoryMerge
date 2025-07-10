using InventoryMerge.Gameplay.Data;

namespace InventoryMerge.Gameplay.Services {
    public interface IInventoryItemMergeService {
        public bool CanMerge(IInventoryItemData item, IInventoryItemData other);
        public bool TryMerge(IInventoryItemData item, IInventoryItemData other);
    }
}
