using UnityEngine;

//Got from https://discussions.unity.com/t/convert-recttransform-rect-to-rect-world/153391/3
// ReSharper disable once CheckNamespace
public static class RectTransformExtensions {
    public static Rect GetWorldRect(this RectTransform rectTransform) {
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        // Get the bottom left corner.
        var position = corners[0];
        
        var size = new Vector2(
            rectTransform.lossyScale.x * rectTransform.rect.size.x,
            rectTransform.lossyScale.y * rectTransform.rect.size.y);

        return new Rect(position, size);
    }
}