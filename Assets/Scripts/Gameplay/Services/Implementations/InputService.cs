using System;
using UnityEngine;

namespace InventoryMerge.Gameplay.Services.Implementations {
    public class InputService : IInputService {
        public event Action<Vector2> OnTapStarted;
        public event Action<Vector2> OnTapEnded;
        public event Action OnTapCanceled;

        public InputService() {
            Debug.Log("Injected lmao");
        }
    }
}
