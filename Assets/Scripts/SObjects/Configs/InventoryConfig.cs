using System;
using UnityEngine;

namespace InventoryMerge.SObjects.Configs {
    [Serializable]
    public class InventoryConfig {
        [field: SerializeField] public Vector2Int Size { get; private set; }
    }
}
