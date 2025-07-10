using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Data.Implementations;
using InventoryMerge.Gameplay.Providers;
using InventoryMerge.Gameplay.Services;
using InventoryMerge.Gameplay.Views.Inventory;
using InventoryMerge.SObjects.Databases;
using InventoryMerge.Utils.ObjectPool;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Gameplay.Factories.Implementations {
    public class InventoryItemViewFactory : IInventoryItemViewFactory {
        private readonly IInventoryItemViewsProvider _itemViewsProvider;
        private readonly IInventoryItemTransferService _itemTransferService;
        private readonly InventoryItemsDatabase _itemsDatabase;
        
        private PrefabMonoObjectPool<InventoryItemId, InventoryItemView> _prefabPool;

        [Inject]
        public InventoryItemViewFactory(IInventoryItemViewsProvider itemViewsProvider,
            IInventoryItemTransferService itemTransferService, 
            InventoryItemsDatabase itemsDatabase) {
            _itemViewsProvider = itemViewsProvider;
            _itemTransferService = itemTransferService;
            _itemsDatabase = itemsDatabase;
        }

        public void Setup(Transform defaultParent, IObjectResolver objectResolver) {
            _prefabPool = new PrefabMonoObjectPool<InventoryItemId, InventoryItemView>(defaultParent, objectResolver);
            foreach (var itemInfo in _itemsDatabase.Items) {
                _prefabPool.Register(itemInfo.ItemId, itemInfo.Prefab);
            }
        }
        
        public InventoryItemView Spawn(InventoryItemId id, int level) {
            var itemInfo = _itemsDatabase.GetItemInfo(id);
            if (itemInfo == null) {
                return null;
            }
            
            var obj = _prefabPool.TrySpawnObject(id);
            if (!obj) {
                return null;
            }

            var itemData = new InventoryItemData(id, level, itemInfo.ItemSlots);
            obj.Initialize(itemData);
            
            _itemViewsProvider.Register(obj);
            _itemTransferService.AttachToHolder(obj);
            return obj;
        }

        public void Despawn(InventoryItemView itemView) {
            _prefabPool.DespawnObject(itemView.Data.Id, itemView);
        }
    }
}
