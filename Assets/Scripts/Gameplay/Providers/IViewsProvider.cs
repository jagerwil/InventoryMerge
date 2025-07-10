using InventoryMerge.Gameplay.Views.Inventory;
using InventoryMerge.Gameplay.Views.Items;
using UnityEngine;

namespace InventoryMerge.Gameplay.Providers {
    public interface IViewsProvider {
        public void Setup(InventoryView inventoryView, ItemsHolderView itemsHolderView, Transform defaultItemsRoot);
        
        public InventoryView InventoryView { get; }
        public ItemsHolderView ItemsHolderView { get; }
        public Transform DefaultItemsRoot { get; }
    }
}
