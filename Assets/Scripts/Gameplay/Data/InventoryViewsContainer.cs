using System;
using InventoryMerge.Gameplay.Views;
using InventoryMerge.Utils.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace InventoryMerge.Gameplay.Data {
    [Serializable]
    public class InventoryViewsContainer : GridContainer<InventorySlotView> {
        private IReadOnlyInventoryData _data;
        private InventorySlotView _prefab;
        private Transform _itemsRoot;

        public InventoryViewsContainer(IReadOnlyInventoryData data, InventorySlotView prefab, Transform itemsRoot) {
            _data = data;
            _prefab = prefab;
            _itemsRoot = itemsRoot;
            
            Initialize(data.Size);
        }

        protected override InventorySlotView CreateElement(Vector2Int index) {
            var view = Object.Instantiate(_prefab, _itemsRoot);
            view.BindData(_data.GetSlot(index));
            return view;
        }
    }
}
