using InventoryMerge.SObjects.Configs;
using UnityEngine;

namespace InventoryMerge.SObjects {
    [CreateAssetMenu(fileName = "Game Config", menuName = "Configs/Game Config")]
    public class GameConfig : ScriptableObject {
        [field: SerializeField] public InventoryConfig Inventory { get; private set; }
    }
}
