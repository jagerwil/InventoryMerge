using System.Collections.Generic;
using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Views.Inventory;

namespace InventoryMerge.Gameplay.Providers.Implementations {
    public class InventoryItemViewsProvider : IInventoryItemViewsProvider {
        private readonly Dictionary<IInventoryItemData, InventoryItemView> _itemViews = new();
        
        public void Register(InventoryItemView itemView) {
            _itemViews.TryAdd(itemView.Data, itemView);
        }
        
        public InventoryItemView GetItemView(IInventoryItemData data) {
            return _itemViews.GetValueOrDefault(data);
        }
    }
}
