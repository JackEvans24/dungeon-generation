using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CorridorFirstGenerator : RandomWalkDungeonGenerator
{
    [Header("Corridor variables")]
    [SerializeField] protected CorridorFirstParameters corridorParameters;

    protected override HashSet<Vector2Int> RunGenerationAlgorithm()
    {
        return this.CorridorFirstGeneration();
    }

    protected HashSet<Vector2Int> CorridorFirstGeneration()
    {
        var (floorPositions, potentialRoomPositions) = this.CreateCorridors();

        var roomPositions = this.CreateRooms(floorPositions, potentialRoomPositions);
        floorPositions.UnionWith(roomPositions);

        return floorPositions;
    }

    protected (HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions) CreateCorridors()
    {
        var floorPositions = new HashSet<Vector2Int>();
        var potentialRoomPositions = new HashSet<Vector2Int>();

        var currentPosition = this.startPosition;
        potentialRoomPositions.Add(currentPosition);

        for (int i = 0; i < this.corridorParameters.CorridorCount; i++)
        {
            var corridor = RandomWalk.GenerateCorridor(currentPosition, this.corridorParameters.CorridorLength);
            floorPositions.UnionWith(corridor);

            currentPosition = corridor[corridor.Count - 1];
            potentialRoomPositions.Add(currentPosition);
        }

        return (floorPositions, potentialRoomPositions);
    }

    protected HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var roomPositions = new HashSet<Vector2Int>();
        var roomToCreateCount = Mathf.RoundToInt(potentialRoomPositions.Count * this.corridorParameters.RoomPercent);

        var generateRoomPositions = this.FindAllDeadEnds(floorPositions, potentialRoomPositions);

        if (generateRoomPositions.Count < roomToCreateCount)
        {
            var otherRoomPositions = potentialRoomPositions
                .OrderBy(position => Guid.NewGuid())
                .Take(roomToCreateCount - generateRoomPositions.Count);
            generateRoomPositions = generateRoomPositions
                .Union(otherRoomPositions)
                .ToList();
        }

        foreach (var startPosition in generateRoomPositions)
        {
            var roomFloor = this.RunRandomWalk(this.roomParameters, startPosition);
            roomPositions.UnionWith(roomFloor);
        }

        return roomPositions;
    }

    protected List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomPositions)
    {
        var deadEnds = new List<Vector2Int>();

        foreach (var position in potentialRoomPositions)
        {
            int neighboursCount = 0;
            foreach (var direction in Direction2D.CardinalDirections)
            {
                if (floorPositions.Contains(position + direction))
                    neighboursCount++;
            }

            if (neighboursCount <= 1)
                deadEnds.Add(position);
        }

        return deadEnds;
    }
}
