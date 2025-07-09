using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace InventoryMerge.Utils.Data {
    public class LookupTable<TKey, TElement> {
        private readonly Dictionary<TKey, TElement> _lookupTable;

        public IEnumerable<KeyValuePair<TKey, TElement>> IterableCollection => _lookupTable;

        public LookupTable(ICollection<TElement> elements, Func<TElement, TKey> elementToKey) {
            _lookupTable = new(elements.Count);
            foreach (var elem in elements) {
                _lookupTable.TryAdd(elementToKey.Invoke(elem), elem);
            }
        }

        [CanBeNull]
        public TElement GetElement(TKey key) {
            if (_lookupTable.TryGetValue(key, out var elem)) {
                return elem;
            }

            Debug.LogWarning($"{GetType().Name}.{nameof(GetElement)}(): {typeof(TKey).Name} \"{key}\" not found");
            return default;
        }
    }
}