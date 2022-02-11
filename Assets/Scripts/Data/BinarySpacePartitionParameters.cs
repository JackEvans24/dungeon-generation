using UnityEngine;

[CreateAssetMenu(menuName = "Generation/Binary Partition Parameters")]
public class BinarySpacePartitionParameters : ScriptableObject
{
    public Vector2Int MinimumRoomDimensions = Vector2Int.one * 8;
    public Vector2Int WorldSpace = Vector2Int.one * 50;
    public int Offset = 1;
    public bool GuaranteeSplitRooms = false;

    public int MinimumWidth { get => this.MinimumRoomDimensions.x; }
    public int MinimumHeight { get => this.MinimumRoomDimensions.y; }

    public bool RoomSmallerThanBoundary(BoundsInt room) => room.size.y < this.MinimumHeight || room.size.x < this.MinimumWidth;
    public int GetXSplit(BoundsInt room) => this.GuaranteeSplitRooms ?
            Random.Range(this.MinimumWidth, room.size.x - this.MinimumWidth) :
            Random.Range(1, room.size.x);
    public int GetYSplit(BoundsInt room) => this.GuaranteeSplitRooms ?
            Random.Range(this.MinimumHeight, room.size.y - this.MinimumHeight) :
            Random.Range(1, room.size.y);
}
