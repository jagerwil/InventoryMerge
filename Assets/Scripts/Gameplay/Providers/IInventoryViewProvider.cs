using InventoryMerge.Gameplay.Views.Inventory;
using InventoryMerge.Gameplay.Views.Items;
using UnityEngine;

namespace InventoryMerge.Gameplay.Providers {
    public interface IInventoryViewProvider {
        public void Setup(InventoryView inventoryView);
        
        public InventoryView InventoryView { get; }
    }
}
