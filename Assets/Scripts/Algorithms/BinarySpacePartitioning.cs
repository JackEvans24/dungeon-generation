using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BinarySpacePartitioning
{
    public static List<BoundsInt> Generate(BoundsInt worldSpace, int minimumWidth, int minimumHeight)
    {
        var roomsQueue = new Queue<BoundsInt>();
        var roomsList = new List<BoundsInt>();

        roomsQueue.Enqueue(worldSpace);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.y < minimumHeight || room.size.x < minimumWidth)
                continue;

            List<BoundsInt> newRooms;
            var shouldPrioritiseHorizontal = Random.value < 0.5f;
            var canSplitHorizontally = room.size.y >= minimumHeight * 2;
            var canSplitVertically = room.size.x >= minimumWidth * 2;

            if (shouldPrioritiseHorizontal && canSplitHorizontally)
                newRooms = SplitHorizontally(room, minimumHeight);
            else if (canSplitVertically)
                newRooms = SplitVertically(room, minimumWidth);
            else if (canSplitHorizontally)
                newRooms = SplitHorizontally(room, minimumHeight);
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

    private static List<BoundsInt> SplitHorizontally(BoundsInt room, int minimumHeight)
    {
        if (room.size.y < minimumHeight * 2)
            return new List<BoundsInt>();

        //var ySplit = Random.Range(1, room.size.y);
        var ySplit = Random.Range(minimumHeight, room.size.y - minimumHeight);

        var newRoom1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        var newRoom2 = new BoundsInt(
            new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z)
        );

        return new List<BoundsInt>(new BoundsInt[] { newRoom1, newRoom2 });
    }

    private static List<BoundsInt> SplitVertically(BoundsInt room, int minimumWidth)
    {
        if (room.size.x < minimumWidth * 2)
            return new List<BoundsInt>();

        //var xSplit = Random.Range(1, room.size.x);
        var xSplit = Random.Range(minimumWidth, room.size.x - minimumWidth);

        var newRoom1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        var newRoom2 = new BoundsInt(
            new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z)
        );

        return new List<BoundsInt>(new BoundsInt[] { newRoom1, newRoom2 });
    }
}
