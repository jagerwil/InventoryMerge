using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryMerge.Utils.Data {
    [Serializable]
    public abstract class GridContainer<T> : IGridContainer<T> where T : class {
        protected List<T> _elements = new();
        
        public Vector2Int Size { get; private set; }

        protected void Initialize(Vector2Int size) {
            Size = size;
            Debug.Log($"Initialize {GetType().Name}: Size {size}");
            
            for (int x = 0; x < Size.x; x++) {
                for (int y = 0; y < Size.y; y++) {
                    _elements.Add(CreateElement(new Vector2Int(x, y)));
                }
            }
        }

        public T GetElement(Vector2Int index) {
            return GetElement(index.x, index.y);
        }

        public T GetElement(int x, int y) {
            if (_elements.Count < Size.x * Size.y) {
                return null;
            }
            return _elements[x + y * Size.x];
        }

        public IEnumerable<T> GetElements() => _elements;

        protected abstract T CreateElement(Vector2Int index);
    }
}
