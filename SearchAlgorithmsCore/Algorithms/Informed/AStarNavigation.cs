using System.Diagnostics;
using SearchAlgorithmsCore.Interfaces;
using SearchAlgorithmsCore.Models;

namespace SearchAlgorithmsCore.Algorithms.Informed;

public class AStarNavigation
{
    private readonly IHeuristic _heuristic;

    public AStarNavigation(IHeuristic heuristic)
    {
        _heuristic = heuristic;
    }

    public (List<(int,int)>? path, double cost, int visitedNodes) FindPath(TerrainGrid grid)
    {
        var stopwatch = Stopwatch.StartNew();

        var open = new PriorityQueue<Node, double>();
        var visited = new Dictionary<(int,int), double>();

        var start = new Node(grid.Start.r, grid.Start.c)
        {
            G = 0,
            H = _heuristic.Estimate(grid.Start.r, grid.Start.c, grid.Goal.r, grid.Goal.c)
        };

        open.Enqueue(start, start.F);
        visited[(start.Row, start.Col)] = 0;

        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        while (open.Count > 0)
        {
            var current = open.Dequeue();
            
            if ((current.Row, current.Col) == grid.Goal)
            {
                stopwatch.Stop();
                return (current.ReconstructPath(), current.G, visited.Count);
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

                double newG = current.G + terrainCost;
                if (visited.ContainsKey((nr, nc)) && visited[(nr, nc)] <= newG)
                    continue;

                var neighbor = new Node(nr, nc)
                {
                    G = (int)newG,
                    H = _heuristic.Estimate(nr, nc, grid.Goal.r, grid.Goal.c),
                    Parent = current
                };
                visited[(nr, nc)] = newG;
                open.Enqueue(neighbor, neighbor.F);
            }
        }

        stopwatch.Stop();
        return (null, double.PositiveInfinity, visited.Count);
    }
}