using InventoryMerge.Gameplay.Views.Inventory;

namespace InventoryMerge.Gameplay.Providers.Implementations {
    public class InventoryViewProvider : IInventoryViewProvider {
        public InventoryView InventoryView { get; private set; }

        public void Setup(InventoryView inventoryView) {
            InventoryView = inventoryView;
        }
    }
}
