using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Providers;
using InventoryMerge.SObjects.Databases;
using VContainer;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class InventoryItemMergeService : IInventoryItemMergeService {
        private readonly InventoryItemsDatabase _itemsDatabase;
        private readonly IInventoryItemViewsProvider _itemViewsProvider;

        [Inject]
        public InventoryItemMergeService(InventoryItemsDatabase itemsDatabase, IInventoryItemViewsProvider itemViewsProvider) {
            _itemsDatabase = itemsDatabase;
            _itemViewsProvider = itemViewsProvider;
        }
        
        public bool CanMerge(IInventoryItemData item, IInventoryItemData other) {
            if (item == null || other == null) {
                return false;
            }

            if (item.Id != other.Id || item.Level.CurrentValue != other.Level.CurrentValue) {
                return false;
            }

            var itemInfo = _itemsDatabase.GetItemInfo(item.Id);
            if (itemInfo == null) {
                return false;
            }

            var nextItemLevel = item.Level.CurrentValue + 1;
            return nextItemLevel < itemInfo.Levels.Count;
        }

        public bool TryMerge(IInventoryItemData item, IInventoryItemData other) {
            if (!CanMerge(item, other)) {
                return false;
            }
            
            item.IncreaseLevel();
            var itemToDespawn = _itemViewsProvider.GetItemView(other);
            itemToDespawn?.Despawn();
            return true;
        }
    }
}
