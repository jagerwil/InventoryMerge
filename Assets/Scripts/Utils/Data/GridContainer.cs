using System;
using System.Collections.Generic;
using UnityEngine;

namespace InventoryMerge.Utils.Data {
    public abstract class GridContainer<T> : IGridContainer<T> where T : class {
        protected List<T> _elements = new();
        
        public Vector2Int Size { get; private set; }

        protected void Initialize(Vector2Int size, Func<Vector2Int, T> createElementFunc) {
            Size = size;
            Debug.Log($"Initialize {GetType().Name}: Size {size}");
            
            for (int y = 0; y < Size.y; y++) {
                for (int x = 0; x < Size.x; x++) {
                    _elements.Add(createElementFunc(new Vector2Int(x, y)));
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
            return _elements[GetInternalIndex(x, y)];
        }

        public IEnumerable<T> GetElements() => _elements;

        protected int GetInternalIndex(int x, int y) {
            return x + y * Size.x;
        }
    }
}
