using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Data.Implementations;
using InventoryMerge.Gameplay.Providers;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Gameplay.Views.Inventory {
    public class InventoryItemView : MonoBehaviour {
        [SerializeField] private Vector2Int _size;
        
        [Inject] private IInventoryItemViewsProvider _itemsProvider;
        
        public IInventoryItemData Data { get; private set; }

        private void Awake() {
            var dataContainer = new InventorySlotsDataContainer(_size);
            Data = new InventoryItemData(dataContainer);
            dataContainer.SetItem(Data);
        }

        private void Start() {
            _itemsProvider.Register(this);
        }
    }
}
