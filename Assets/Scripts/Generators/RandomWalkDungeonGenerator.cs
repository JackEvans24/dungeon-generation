using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomWalkDungeonGenerator : DungeonGenerator
{
    [SerializeField] protected RandomWalkParameters parameters;

    protected override HashSet<Vector2Int> RunGenerationAlgorithm(Vector2Int startPosition)
    {
        var currentPosition = this.startPosition;
        var floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < this.parameters.iterations; i++)
        {
            var path = RandomWalk.Generate(currentPosition, this.parameters.walkLength);
            floorPositions.UnionWith(path);

            if (this.parameters.randomIterationStart)
                currentPosition = floorPositions.ElementAt(Random.Range(0, floorPositions.Count));
        }

        return floorPositions;
    }
}
