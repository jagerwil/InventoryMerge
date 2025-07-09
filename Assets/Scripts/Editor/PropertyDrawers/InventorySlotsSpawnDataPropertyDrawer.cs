using System;
using InventoryMerge.Gameplay.Data;
using InventoryMerge.SObjects.Configs;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Color = UnityEngine.Color;

namespace InventoryMerge.Editor.PropertyDrawers {
    [CustomPropertyDrawer(typeof(InventorySlotsDataContainerSpawnInfo))]
    public class InventorySlotsSpawnDataPropertyDrawer : PropertyDrawer {
        private static StyleColor AvailableSlotColor = new StyleColor(Color.green); 
        private static StyleColor UnavailableSlotColor = new StyleColor(Color.red); 
        
        private static Length ElementSize = new Length(50f);
        private static Length Margin = new Length(5f);
        
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            var rootElement = new VisualElement();

            rootElement.TrackPropertyValue(property, (trackedProperty) => {
                RedrawPropertyDrawer(rootElement, trackedProperty);
            });
            
            RedrawPropertyDrawer(rootElement, property);

            return rootElement;
        }

        private void RedrawPropertyDrawer(VisualElement rootElement, SerializedProperty property) {
            rootElement.Clear();
            
            var sizeProperty = property.FindPropertyRelative("_size");
            var slotsProperty = property.FindPropertyRelative("_slots");
            
            var size = sizeProperty.vector2IntValue;
            var amountField = new PropertyField(sizeProperty);
            
            rootElement.Add(amountField);
            
            //EditorGUI.BeginChangeCheck();
            slotsProperty.arraySize = size.x * size.y;
            try {
                for (var y = size.y - 1; y >= 0; y--) {
                    var row = new VisualElement();
                    row.style.flexDirection = FlexDirection.Row;

                    for (var x = 0; x < size.x; x++) {
                        var slot = CreateSlot(new Vector2Int(x, y), size, slotsProperty);
                        row.Add(slot);
                    }

                    rootElement.Add(row);
                }
            }
            catch (Exception e) {
                Debug.LogException(e);
            }
            
            property.serializedObject.ApplyModifiedProperties();
        }

        private VisualElement CreateSlot(Vector2Int index, Vector2Int size, SerializedProperty slotsProperty) {
            var slot = new VisualElement();
            var valueProperty = slotsProperty.GetArrayElementAtIndex(index.x + index.y * size.x);

            slot.style.width = ElementSize;
            slot.style.height = ElementSize;
            slot.style.marginTop = Margin;
            slot.style.marginBottom = Margin;
            slot.style.marginLeft = Margin;
            slot.style.marginRight = Margin;

            if (valueProperty == null) {
                Debug.LogError("SLOT VALUE PROP IS NULL!");
                return slot;
            }
            
            slot.RegisterCallback<ClickEvent>((evt) => OnSlotClicked(evt, valueProperty));
            var value = valueProperty.boxedValue as InventorySlotDataSpawnInfo;
            var isAvailable = value.State == InventorySlotState.Available;
            
            slot.style.backgroundColor = isAvailable ? AvailableSlotColor : UnavailableSlotColor;
            return slot;
        }

        private void OnSlotClicked(ClickEvent evt, SerializedProperty elementProp) {
            var stateProp = elementProp.FindPropertyRelative("<State>k__BackingField");
            var value = (InventorySlotState)stateProp.enumValueIndex;
            var oppositeValue = value == InventorySlotState.Available ? InventorySlotState.Disabled : InventorySlotState.Available;
            stateProp.enumValueIndex = (int)oppositeValue;
            elementProp.serializedObject.ApplyModifiedProperties();
        }
    }
}
