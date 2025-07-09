using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Data.Implementations;
using UnityEngine;

namespace InventoryMerge.Gameplay.Views {
    public class InventoryItemView : MonoBehaviour {
        [SerializeField] private Vector2Int _size;
        
        public IInventoryItemData Data { get; private set; }

        private void Awake() {
            var dataContainer = new InventorySlotsDataContainer(_size);
            Data = new InventoryItemData(dataContainer);
            dataContainer.SetItem(Data);
        }
    }
}
