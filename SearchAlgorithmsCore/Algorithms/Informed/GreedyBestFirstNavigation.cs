using SearchAlgorithmsCore.Interfaces;
using SearchAlgorithmsCore.Models;

namespace SearchAlgorithmsCore.Algorithms.Informed;

public class GreedyBestFirstNavigation
{
    private readonly IHeuristic _heuristic;

    public GreedyBestFirstNavigation(IHeuristic heuristic)
    {
        _heuristic = heuristic;
    }

    public (List<(int,int)>? path, double cost, int visitedNodes) FindPath(TerrainGrid grid)
    {
        var open = new PriorityQueue<Node, double>();
        var visited = new HashSet<(int,int)>();

        var start = new Node(grid.Start.r, grid.Start.c)
        {
            G = 0,
            H = _heuristic.Estimate(grid.Start.r, grid.Start.c, grid.Goal.r, grid.Goal.c)
        };

        open.Enqueue(start, start.H);

        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        int visitedCount = 0;

        while (open.Count > 0)
        {
            var current = open.Dequeue();

            if (visited.Contains((current.Row, current.Col)))
                continue;

            visited.Add((current.Row, current.Col));
            visitedCount++;

            if ((current.Row, current.Col) == grid.Goal)
            {
                return (current.ReconstructPath(), current.G, visitedCount);
            }

            for (int i = 0; i < 4; i++)
            {
                int nr = current.Row + dr[i];
                int nc = current.Col + dc[i];

                if (!grid.IsInside(nr, nc))
                    continue;

                int terrainCost = grid.GetCost(nr, nc);

                if (terrainCost >= 9)
                    continue;

                if (visited.Contains((nr, nc)))
                    continue;

                var neighbor = new Node(nr, nc)
                {
                    G = current.G + terrainCost,
                    H = _heuristic.Estimate(nr, nc, grid.Goal.r, grid.Goal.c),
                    Parent = current
                };

                open.Enqueue(neighbor, neighbor.H);
            }
        }

        return (null, double.PositiveInfinity, visitedCount);
    }
}