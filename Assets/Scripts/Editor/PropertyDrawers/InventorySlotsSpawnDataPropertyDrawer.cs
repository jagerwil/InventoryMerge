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
            
            var slotsProperty = property.FindPropertyRelative("_slots");
            var sizeProperty = property.FindPropertyRelative("_size");
            
            var amountField = new PropertyField(sizeProperty);
            var elementsField = new VisualElement();
            rootElement.Add(amountField);
            rootElement.Add(elementsField);

            rootElement.TrackPropertyValue(property, (_) => {
                RedrawPropertyDrawer(elementsField, slotsProperty, sizeProperty);
            });

            RedrawPropertyDrawer(elementsField, slotsProperty, sizeProperty);
            return rootElement;
        }

        private void RedrawPropertyDrawer(VisualElement rootElement, SerializedProperty slotsProperty, SerializedProperty sizeProperty) {
            rootElement.Clear();
            
            var size = sizeProperty.vector2IntValue;

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
            
            slotsProperty.serializedObject.ApplyModifiedProperties();
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
            
            slot.RegisterCallback<ClickEvent, SerializedProperty>(OnSlotClicked, valueProperty);
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
