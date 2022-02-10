using UnityEngine;

[CreateAssetMenu(menuName = "Generation/Corridor Parameters")]
public class CorridorFirstParameters : ScriptableObject
{
    public int CorridorLength = 14;
    public int CorridorCount = 5;
    public float RoomPercent = 0.8f;
}
