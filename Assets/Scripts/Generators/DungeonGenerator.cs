using System.Collections.Generic;
using UnityEngine;

public abstract class DungeonGenerator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] protected TilemapVisualiser visualiser;

    [Header("Generation variables")]
    [SerializeField] protected Vector2Int startPosition = Vector2Int.zero;

    public void GenerateDungeon()
    {
        var floorPositions = this.RunGenerationAlgorithm();
        this.PaintDungeonTiles(floorPositions);
    }

    protected void PaintDungeonTiles(HashSet<Vector2Int> floorPositions)
    {
        this.visualiser.PaintFloorTiles(floorPositions);
        this.visualiser.PaintBasicWallTiles(WallGenerator.FindWallsInCardinalDirections(floorPositions));
    }

    protected void LogPositions(HashSet<Vector2Int> floorPositions)
    {
        foreach (var position in floorPositions)
            Debug.Log(position);
    }

    protected abstract HashSet<Vector2Int> RunGenerationAlgorithm();
}
