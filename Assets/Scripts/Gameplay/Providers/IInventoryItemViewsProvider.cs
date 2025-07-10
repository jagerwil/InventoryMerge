using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Views.Inventory;
using InventoryMerge.Gameplay.Views.Items;
using JetBrains.Annotations;

namespace InventoryMerge.Gameplay.Providers {
    public interface IInventoryItemViewsProvider {
        public void Register(InventoryItemView itemView);
        
        [CanBeNull]
        public InventoryItemView GetItemView(IInventoryItemData data);
    }
}
