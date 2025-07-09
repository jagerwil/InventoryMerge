using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Views.Inventory;
using JetBrains.Annotations;

namespace InventoryMerge.Gameplay.Providers {
    public interface IInventoryItemViewsProvider {
        public void Register(InventoryItemView itemView);
        
        [CanBeNull]
        public InventoryItemView GetItemView(IInventoryItemData data);
    }
}
