using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Data.Implementations;
using InventoryMerge.Gameplay.Providers;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Gameplay.Views.Inventory {
    public class InventoryItemView : MonoBehaviour {
        [SerializeField] private InventoryItemId _id;
        [SerializeField] private int _level;
        [SerializeField] private Vector2Int _size;
        
        [Inject] private IInventoryItemViewsProvider _itemsProvider;
        
        public IInventoryItemData Data { get; private set; }

        private void Awake() {
            var dataContainer = new InventorySlotsDataContainer(_size);
            Data = new InventoryItemData(_id, _level, dataContainer);
            dataContainer.SetItem(Data);
        }

        private void Start() {
            _itemsProvider.Register(this);
        }
    }
}
