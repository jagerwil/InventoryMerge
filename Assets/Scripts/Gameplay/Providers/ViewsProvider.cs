using InventoryMerge.Gameplay.Views;

namespace InventoryMerge.Gameplay.Providers {
    public class ViewsProvider : IViewsProvider {
        public InventoryView InventoryView { get; private set; }

        public ViewsProvider(InventoryView inventoryView) {
            InventoryView = inventoryView;
        }
    }
}
