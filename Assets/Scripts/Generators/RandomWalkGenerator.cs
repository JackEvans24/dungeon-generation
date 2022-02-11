using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomWalkGenerator : DungeonGenerator
{
    [SerializeField] protected RandomWalkParameters roomParameters;

    protected override HashSet<Vector2Int> RunGenerationAlgorithm() => RunRandomWalk(this.roomParameters, this.startPosition);

    protected HashSet<Vector2Int> RunRandomWalk(RandomWalkParameters parameters, Vector2Int startPosition)
    {
        var currentPosition = startPosition;
        var floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < parameters.iterations; i++)
        {
            var path = RandomWalk.GenerateWalk(currentPosition, parameters.walkLength);
            floorPositions.UnionWith(path);

            if (parameters.randomIterationStart)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }

        return floorPositions;
    }
}
