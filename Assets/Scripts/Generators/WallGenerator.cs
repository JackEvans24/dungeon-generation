using System.Collections.Generic;
using UnityEngine;

public static class WallGenerator
{
    public static HashSet<Vector2Int> FindWallsInCardinalDirections(HashSet<Vector2Int> floorPositions)
    {
        var wallPositions = new HashSet<Vector2Int>();
        
        foreach (var position in floorPositions)
        {
            foreach (var direction in Direction2D.CardinalDirections)
            {
                var neighbour = position + direction;
                if (!floorPositions.Contains(neighbour))
                    wallPositions.Add(neighbour);
            }
        }

        return wallPositions;
    }
}
