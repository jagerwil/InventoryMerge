using InventoryMerge.Gameplay.Data;
using InventoryMerge.SObjects.Databases;
using InventoryMerge.Utils.ObjectPool;
using R3;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace InventoryMerge.Gameplay.Views.Inventory {
    public class InventoryItemView : MonoPoolObject {
        private readonly CompositeDisposable _disposables = new();
        
        [SerializeField] private Image _image;
        
        [Inject] private InventoryItemsDatabase _itemsDatabase;

        public IInventoryItemData Data { get; private set; }

        public void Initialize(IInventoryItemData data) {
            Data = data;
            data.Level.Subscribe(LevelUpdated).AddTo(_disposables);
        }

        private void OnDestroy() {
            _disposables?.Dispose();
        }

        private void LevelUpdated(int newLevel) {
            var levelInfo = _itemsDatabase.GetItemLevelInfo(Data.Id, newLevel);
            if (levelInfo == null) {
                return;
            }

            _image.color = levelInfo.Color;
            if (levelInfo.Sprite) {
                _image.sprite = levelInfo.Sprite;
            }
        }
    }
}
