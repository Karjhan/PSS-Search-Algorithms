using System.Diagnostics;
using SearchAlgorithmsCore.Interfaces;
using SearchAlgorithmsCore.Models;

namespace SearchAlgorithmsCore.Algorithms;

public class BfsFloodFill : ISearchAlgorithm
{
    public string Name => "BFS";

    public SearchResult Execute(Grid grid, int startRow, int startCol, int newColor)
    {
        var stopwatch = Stopwatch.StartNew();

        int originalColor = grid.Get(startRow, startCol);
        int[,] visitOrder = new int[grid.Rows, grid.Columns];

        var queue = new Queue<(int r, int c)>();

        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        int step = 1;
        int visited = 0;
        int maxFrontier = 0;

        queue.Enqueue((startRow, startCol));

        while (queue.Count > 0)
        {
            maxFrontier = Math.Max(maxFrontier, queue.Count);

            var (r, c) = queue.Dequeue();

            if (!grid.IsInside(r, c))
                continue;

            if (grid.Get(r, c) != originalColor)
                continue;

            grid.Set(r, c, newColor);

            visitOrder[r, c] = step++;
            visited++;

            for (int i = 0; i < 4; i++)
                queue.Enqueue((r + dr[i], c + dc[i]));
        }

        stopwatch.Stop();

        return new SearchResult
        {
            VisitMap = visitOrder,
            NodesVisited = visited,
            MaxFrontierSize = maxFrontier,
            Steps = step - 1,
            ExecutionTimeMs = stopwatch.ElapsedMilliseconds
        };
    }
}   