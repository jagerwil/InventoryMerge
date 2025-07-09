using System;
using InventoryMerge.Gameplay.Data.Implementations;
using UnityEngine;

namespace InventoryMerge.SObjects.Configs {
    [Serializable]
    public class InventoryConfig {
        [SerializeField] private InventorySlotsDataContainerSpawnInfo _spawnInfo;
        
        public InventorySlotsDataContainer DataContainer => _spawnInfo.GetOrCreateInstance();
    }
}
