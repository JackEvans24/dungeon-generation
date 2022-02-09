using UnityEngine;

public static class Direction2D
{
    public static Vector2Int[] CardinalDirections = new Vector2Int[]
    {
        new Vector2Int(0, 1),       // Up
        new Vector2Int(1, 0),       // Right
        new Vector2Int(0, -1),      // Down
        new Vector2Int(-1, 0)       // Left
    };

    public static Vector2Int GetRandomDirection() => CardinalDirections[Random.Range(0, CardinalDirections.Length)];
}
