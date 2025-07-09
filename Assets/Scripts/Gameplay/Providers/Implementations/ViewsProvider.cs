using InventoryMerge.Gameplay.Views.Inventory;
using UnityEngine;

namespace InventoryMerge.Gameplay.Providers.Implementations {
    public class ViewsProvider : IViewsProvider {
        public InventoryView InventoryView { get; private set; }
        public ItemsHolderView ItemsHolderView { get; private set; }
        public Transform DefaultItemsRoot { get; private set; }

        public ViewsProvider(InventoryView inventoryView, ItemsHolderView itemsHolderView, Transform defaultItemsRoot) {
            InventoryView = inventoryView;
            ItemsHolderView = itemsHolderView;
            DefaultItemsRoot = defaultItemsRoot;
        }
    }
}
