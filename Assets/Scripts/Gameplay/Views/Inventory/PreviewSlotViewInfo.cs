using System;
using UnityEngine;

namespace InventoryMerge.Gameplay.Views.Inventory {
    public enum PreviewSlotViewState {
        Unaffected = 0,
        PlaceItem = 1,
        DoesNotFitItem = 2,
        ReplaceItem = 3,
        MergeItem = 4,
    }

    [Serializable]
    public class PreviewSlotViewInfo {
        [field: SerializeField] public PreviewSlotViewState State { get; private set; }
        [field: SerializeField] public Color Color { get; private set; }
    }
}
