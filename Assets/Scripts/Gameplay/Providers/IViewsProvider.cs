using InventoryMerge.Gameplay.Views.Inventory;
using UnityEngine;

namespace InventoryMerge.Gameplay.Providers {
    public interface IViewsProvider {
        public InventoryView InventoryView { get; }
        public ItemsHolderView ItemsHolderView { get; }
        public Transform DefaultItemsRoot { get; }
    }
}
