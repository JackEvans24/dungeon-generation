using UnityEngine;

[CreateAssetMenu(fileName = "RandomWalkParameters_", menuName = "Generation/Random Walk Parameters")]
public class RandomWalkParameters : ScriptableObject
{
    public int iterations = 10;
    public int walkLength = 10;
    public bool randomIterationStart = true;
}
