using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InventoryMerge.Utils.UI {
    public class UiRaycaster : MonoBehaviour {
        [CanBeNull]
        public static T RaycastFirst<T>(Vector2 position) where T : MonoBehaviour {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = position;
  
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            if (raycastResults.Count == 0) {
                return null;
            }
            return raycastResults[0].gameObject.GetComponent<T>();
        }
        
        [CanBeNull]
        public static T RaycastAny<T>(Vector2 position) where T : MonoBehaviour {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = position;
  
            var raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);

            if (raycastResults.Count == 0) {
                return null;
            }

            foreach (var result in raycastResults) {
                var component = result.gameObject.GetComponent<T>();
                if (component) {
                    return component;
                }
            }

            return null;
        }
    }
}
