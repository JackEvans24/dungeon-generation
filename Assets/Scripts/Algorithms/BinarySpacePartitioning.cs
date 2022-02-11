using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BinarySpacePartitioning
{
    public static List<BoundsInt> Generate(BoundsInt worldSpace, BinarySpacePartitionParameters parameters)
    {
        var roomsQueue = new Queue<BoundsInt>();
        var roomsList = new List<BoundsInt>();

        roomsQueue.Enqueue(worldSpace);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (parameters.RoomSmallerThanBoundary(room))
                continue;

            List<BoundsInt> newRooms;
            var shouldPrioritiseHorizontal = Random.value < 0.5f;
            var canSplitHorizontally = room.size.y >= parameters.MinimumHeight * 2;
            var canSplitVertically = room.size.x >= parameters.MinimumWidth * 2;

            if (shouldPrioritiseHorizontal && canSplitHorizontally)
                newRooms = SplitHorizontally(room, parameters);
            else if (canSplitVertically)
                newRooms = SplitVertically(room, parameters);
            else if (canSplitHorizontally)
                newRooms = SplitHorizontally(room, parameters);
            else
            {
                roomsList.Add(room);
                continue;
            }

            foreach (var newRoom in newRooms)
                roomsQueue.Enqueue(newRoom);
        }

        return roomsList;
    }

    private static List<BoundsInt> SplitHorizontally(BoundsInt room, BinarySpacePartitionParameters parameters)
    {
        if (room.size.y < parameters.MinimumHeight * 2)
            return new List<BoundsInt>();

        var ySplit = parameters.GetYSplit(room);

        var newRoom1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        var newRoom2 = new BoundsInt(
            new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z)
        );

        return new List<BoundsInt>(new BoundsInt[] { newRoom1, newRoom2 });
    }

    private static List<BoundsInt> SplitVertically(BoundsInt room, BinarySpacePartitionParameters parameters)
    {
        if (room.size.x < parameters.MinimumWidth * 2)
            return new List<BoundsInt>();

        var xSplit = parameters.GetXSplit(room);

        var newRoom1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        var newRoom2 = new BoundsInt(
            new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z)
        );

        return new List<BoundsInt>(new BoundsInt[] { newRoom1, newRoom2 });
    }
}
