using UnityEngine;

// ReSharper disable once CheckNamespace
public static class VectorExtensions {
    public static Vector2Int RoundToInt(this Vector2 vector) {
        return new Vector2Int(Mathf.RoundToInt(vector.x), Mathf.RoundToInt(vector.y));
    }
    
    public static bool IsInRange(this Vector2Int value, Vector2Int min, Vector2Int max) {
        return value.x >= min.x && value.x <= max.x
            && value.y >= min.y && value.y <= max.y;
    }
}
