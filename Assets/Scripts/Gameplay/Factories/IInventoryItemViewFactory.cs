using InventoryMerge.Gameplay.Data;
using InventoryMerge.Gameplay.Views.Inventory;
using UnityEngine;
using VContainer;

namespace InventoryMerge.Gameplay.Factories {
    public interface IInventoryItemViewFactory {
        public void Setup(Transform defaultParent, IObjectResolver objectResolver);
        
        public InventoryItemView Spawn(InventoryItemId id, int level);
        public void Despawn(InventoryItemView itemView);
    }
}
