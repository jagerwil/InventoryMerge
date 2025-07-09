using InventoryMerge.SObjects.Configs;
using InventoryMerge.SObjects.Databases;
using UnityEngine;

namespace InventoryMerge.SObjects {
    [CreateAssetMenu(fileName = "Game Config", menuName = "Configs/Game Config")]
    public class GameConfig : ScriptableObject {
        [field: Header("Configs")]
        [field: SerializeField] public InventoryConfig Inventory { get; private set; }
        [field: Header("Databases")]
        [field: SerializeField] public InventoryItemsDatabase ItemsDatabase { get; private set; }
    }
}
