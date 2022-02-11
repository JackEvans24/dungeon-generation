using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoomFirstGenerator : RandomWalkGenerator
{
    [Header("Room variables")]
    [SerializeField] private BinarySpacePartitionParameters binarySpaceParameters;

    [SerializeField] private bool randomWalkRooms = false;

    protected override HashSet<Vector2Int> RunGenerationAlgorithm()
    {
        return GenerateRooms();
    }

    private HashSet<Vector2Int> GenerateRooms()
    {
        var worldSpace = new BoundsInt((Vector3Int)this.startPosition, (Vector3Int)this.binarySpaceParameters.WorldSpace);
        var roomsList = BinarySpacePartitioning.Generate(worldSpace, this.binarySpaceParameters);

        var floorPositions = RoomAreasToPositions(roomsList);

        var roomCenterPoints = GetRoomCenterPoints(roomsList);
        var corridorPositions = ConnectRooms(roomCenterPoints);

        floorPositions.UnionWith(corridorPositions);

        return floorPositions;
    }

    private HashSet<Vector2Int> RoomAreasToPositions(List<BoundsInt> roomsList)
    {
        var floorPositions = new HashSet<Vector2Int>();
        var offset = this.binarySpaceParameters.Offset;

        foreach (var room in roomsList)
        {
            for (int column = offset; column < room.size.x - offset; column++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    var position = (Vector2Int)room.min + new Vector2Int(column, row);
                    floorPositions.Add(position);
                }
            }
        }

        return floorPositions;
    }

    private List<Vector2Int> GetRoomCenterPoints(List<BoundsInt> roomsList) =>
        roomsList
            .Select(room => new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.y)))
            .ToList();

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        var corridors = new HashSet<Vector2Int>();

        var currentCenterPoint = roomCenters[Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentCenterPoint);

        while (roomCenters.Count > 0)
        {
            var closestCenterPoint = this.FindClosestPoint(currentCenterPoint, roomCenters);
            roomCenters.Remove(closestCenterPoint);

            var newCorridor = this.CreateCorridor(currentCenterPoint, closestCenterPoint);
            corridors.UnionWith(newCorridor);

            currentCenterPoint = closestCenterPoint;
        }

        return corridors;
    }

    private Vector2Int FindClosestPoint(Vector2Int currentPoint, List<Vector2Int> pointsList)
    {
        var closest = Vector2Int.zero;
        float distance = float.MaxValue;

        foreach (var point in pointsList)
        {
            var newDistance = Vector2.Distance(currentPoint, point);
            if (newDistance >= distance)
                continue;

            distance = newDistance;
            closest = point;
        }

        return closest;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int start, Vector2Int destination)
    {
        var corridor = new HashSet<Vector2Int>();
        var currentPosition = start;
        corridor.Add(currentPosition);

        while (currentPosition.y != destination.y)
        {
            currentPosition += Vector2Int.up * (int)Mathf.Sign(destination.y - currentPosition.y);
            corridor.Add(currentPosition);
        }

        while (currentPosition.x != destination.x)
        {
            currentPosition += Vector2Int.right * (int)Mathf.Sign(destination.x - currentPosition.x);
            corridor.Add(currentPosition);
        }

        return corridor;
    }
}
