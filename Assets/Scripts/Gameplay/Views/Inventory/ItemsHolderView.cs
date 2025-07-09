using UnityEngine;

namespace InventoryMerge.Gameplay.Views.Inventory {
    public class ItemsHolderView : MonoBehaviour {
        [SerializeField] private Transform _itemsRoot;
        
        public void PlaceItem(InventoryItemView item) {
            item.transform.SetParent(_itemsRoot);
            item.transform.localPosition = Vector3.zero;
        }
    }
}
