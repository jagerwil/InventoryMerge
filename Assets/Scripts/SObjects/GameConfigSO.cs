using InventoryMerge.SObjects.Configs;
using InventoryMerge.SObjects.Databases;
using UnityEngine;

namespace InventoryMerge.SObjects {
    [CreateAssetMenu(fileName = "Game Config", menuName = "Configs/Game Config", order = 1)]
    public class GameConfigSO : ScriptableObject {
        [field: Header("Configs")]
        [field: SerializeField] public InventoryConfig Inventory { get; private set; }
        [field: Space]
        [field: SerializeField] public ItemsSpawnConfig ItemsSpawning { get; private set; }
        [field: Header("Databases")]
        [field: SerializeField] public InventoryItemsDatabaseSO ItemsDatabase { get; private set; }
    }
}
