using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapVisualiser : MonoBehaviour
{
    [Header("References - Floor")]
    [SerializeField] private Tilemap floorTilemap;
    [SerializeField] private TileBase floorTile;

    [Header("References - Wall")]
    [SerializeField] private Tilemap wallTilemap;
    [SerializeField] private TileBase wallTile;

    [Header("Generation variables")]
    [SerializeField] private bool clearOnGenerate = true;

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorTiles)
    {
        if (this.clearOnGenerate)
            this.ClearAllTiles();

        this.PaintTiles(floorTiles, this.floorTilemap, this.floorTile);
    }

    internal void PaintBasicWallTiles(HashSet<Vector2Int> basicWallPositions)
    {
        this.PaintTiles(basicWallPositions, this.wallTilemap, this.wallTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {
        foreach (var position in positions)
            PaintSingleTile(position, tilemap, tile);
    }

    private void PaintSingleTile(Vector2Int position, Tilemap tilemap, TileBase tile)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void ClearAllTiles()
    {
        this.floorTilemap.ClearAllTiles();
        this.wallTilemap.ClearAllTiles();
    }
}
