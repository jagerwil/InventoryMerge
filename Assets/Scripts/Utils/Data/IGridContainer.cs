using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace InventoryMerge.Utils.Data {
    public interface IGridContainer<out T> where T : class {
        public Vector2Int Size { get; }

        [CanBeNull] public T GetElement(Vector2Int index);
        [CanBeNull] public T GetElement(int x, int y);
        
        public IEnumerable<T> GetElements();
    }
}
