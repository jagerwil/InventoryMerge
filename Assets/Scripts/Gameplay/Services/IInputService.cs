using System;
using UnityEngine;

namespace InventoryMerge.Gameplay.Services {
    public interface IInputService {
        public event Action<Vector2> OnTapStarted;
        public event Action<Vector2> OnTapEnded;
        public event Action OnTapCanceled;

        public event Action<Vector2> OnTouchPositionChanged;
    }
}
