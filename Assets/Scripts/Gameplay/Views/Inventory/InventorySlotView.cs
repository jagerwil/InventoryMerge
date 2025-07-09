using InventoryMerge.Gameplay.Data;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryMerge.Gameplay.Views.Inventory {
    public class InventorySlotView : MonoBehaviour {
        [SerializeField] private Image _image;
        [SerializeField] private Transform _itemPlace;
        [SerializeField] private Color _freeSlotColor;
        [SerializeField] private Color _occupiedSlotColor;
        
        private readonly CompositeDisposable _disposables = new();
        
        private RectTransform _rectTransform;
        private IInventorySlotData _data;
        
        public Vector2Int Index => _data?.Index ?? Vector2Int.one * -1;
        public Transform ItemPlace => _itemPlace;

        private void Awake() {
            _rectTransform = transform as RectTransform;
        }

        public void BindData(IInventorySlotData data) {
            _data = data;
            _data.Item.Subscribe(SlotItemUpdated).AddTo(_disposables);
        }

        private void OnDestroy() {
            _disposables?.Dispose();
        }

        public Vector2 GetCenterOffsetRatio(Vector2 screenPosition) {
            var rect = _rectTransform.GetWorldRect();
            var deltaPos = screenPosition - rect.center;
            return new Vector2(deltaPos.x / rect.width, deltaPos.y / rect.height);
        }

        private void SlotItemUpdated(IInventoryItemData item) {
            _image.color = item == null ? _freeSlotColor : _occupiedSlotColor;
        }
    }
}
