using InventoryMerge.Gameplay.Data;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryMerge.Gameplay.Views.Inventory {
    public class InventorySlotView : MonoBehaviour {
        [SerializeField] private Image _image;
        [SerializeField] private Transform _itemPlace;
        [SerializeField] private InventorySlotViewColorChanger _colorChanger;

        private RectTransform _rectTransform;
        private IInventorySlotData _data;

        public Vector2Int Index => _data?.Index ?? Vector2Int.one * -1;
        public IInventoryItemData Item => _data.Item.CurrentValue;
        
        public Transform ItemPlace => _itemPlace;

        protected bool IsSlotAvailable => _data.State == InventorySlotState.Available;

        private void Awake() {
            _rectTransform = transform as RectTransform;
        }

        public void BindData(IInventorySlotData data) {
            _data = data;

            if (!IsSlotAvailable) {
                _colorChanger.SetDisabledColor();
                return;
            }

            _colorChanger.SetDefaultColor();
        }

        public void ShowPreview(PreviewSlotViewState state) {
            if (IsSlotAvailable) {
                _colorChanger.SetPreviewColor(state);
            }
        }

        public void StopPreview() {
            if (IsSlotAvailable) {
                _colorChanger.SetDefaultColor();
            }
        }

        public Vector2 GetApproxIndex(Vector2 screenPosition) {
            return Index + GetCenterOffsetRatio(screenPosition);
        }

        private Vector2 GetCenterOffsetRatio(Vector2 screenPosition) {
            var rect = _rectTransform.GetWorldRect();
            var deltaPos = screenPosition - rect.center;
            return new Vector2(deltaPos.x / rect.width, deltaPos.y / rect.height);
        }
    }
}
