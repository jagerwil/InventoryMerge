using InventoryMerge.Gameplay.Data;
using InventoryMerge.Utils.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryMerge.Gameplay.Views {
    public class InventoryView : MonoBehaviour {
        [SerializeField] private GridLayoutGroup _gridLayout;
        [SerializeField] private RectTransform _objectsRoot;
        [SerializeField] private InventorySlotView _prefab; 
        
        private InventoryViewsContainer _viewsContainer;

        public void BindData(IReadOnlyInventoryData data) {
            _viewsContainer = new(data, _prefab, _objectsRoot);
            _gridLayout.constraintCount = data.Size.x;
        }

        public Vector2 GetSlotPosition(Vector2 lerpIndex) {
            var slotSize = _gridLayout.cellSize;
            var spacing = _gridLayout.spacing;
            var rect = _objectsRoot.GetWorldRect();
            
            var firstSlotPosition = rect.min + slotSize * 0.5f;
            var xCoord = firstSlotPosition.x + slotSize.x * lerpIndex.x + Mathf.Floor(lerpIndex.x) * spacing.x;
            var yCoord = firstSlotPosition.y + slotSize.y * lerpIndex.y + Mathf.Floor(lerpIndex.y) * spacing.y;
            return new Vector2(xCoord, yCoord);
        }
    }
}
