using System.Collections.Generic;
using InventoryMerge.Utils.Data;
using UnityEngine;
using UnityEngine.UI;

namespace InventoryMerge.Gameplay.Views.Inventory {
    public class InventorySlotViewColorChanger : MonoBehaviour {
        [SerializeField] private Image _image;
        [SerializeField] private Color _defaultSlotColor;
        [SerializeField] private Color _disabledSlotColor;
        [Header("Preview Colors")]
        [SerializeField] private List<PreviewSlotViewInfo> _previewColors;

        private LookupTable<PreviewSlotViewState, PreviewSlotViewInfo> _previewColorsLookupTable;

        private void Awake() {
            _previewColorsLookupTable = new LookupTable<PreviewSlotViewState, PreviewSlotViewInfo>(_previewColors, 
                (viewInfo) => viewInfo.State);
        }

        public void SetDefaultColor() {
            _image.color = _defaultSlotColor;
        }

        public void SetDisabledColor() {
            _image.color = _disabledSlotColor;
        }

        public void SetPreviewColor(PreviewSlotViewState state) {
            var viewInfo = _previewColorsLookupTable.GetElement(state);;
            if (viewInfo == null) {
                Debug.LogError($"{GetType().Name}.{nameof(SetPreviewColor)}(): State \"{state}\" is not present in {nameof(_previewColors)}");
                return;
            }

            _image.color = viewInfo.Color;
        }
    }
}
