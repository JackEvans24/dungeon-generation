using System.Collections.Generic;
using UnityEngine;

public static class RandomWalk
{
    public static HashSet<Vector2Int> GenerateWalk(Vector2Int startPosition, int walkLength)
    {
        var path = new HashSet<Vector2Int>();
        
        var currentPosition = startPosition;
        path.Add(currentPosition);

        for (int i = 0; i < walkLength; i++)
        {
            currentPosition += Direction2D.GetRandomDirection();
            path.Add(currentPosition);
        }

        return path;
    }

    public static List<Vector2Int> GenerateCorridor(Vector2Int startPosition, int corridorLength)
    {
        var corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomDirection();

        var currentPosition = startPosition;
        corridor.Add(currentPosition);

        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);
        }

        return corridor;
    }
}
