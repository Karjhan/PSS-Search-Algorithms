using System.Diagnostics;
using SearchAlgorithmsCore.Interfaces;
using SearchAlgorithmsCore.Models;

namespace SearchAlgorithmsCore.Algorithms;

public class DfsFloodFill : ISearchAlgorithm
{
    public string Name => "DFS";

    public SearchResult Execute(Grid grid, int startRow, int startCol, int newColor)
    {
        var stopwatch = Stopwatch.StartNew();

        int originalColor = grid.Get(startRow, startCol);
        int[,] visitOrder = new int[grid.Rows, grid.Columns];

        var stack = new Stack<(int r, int c)>();

        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        int step = 1;
        int visited = 0;
        int maxFrontier = 0;

        stack.Push((startRow, startCol));

        while (stack.Count > 0)
        {
            maxFrontier = Math.Max(maxFrontier, stack.Count);

            var (r, c) = stack.Pop();

            if (!grid.IsInside(r, c))
                continue;

            if (grid.Get(r, c) != originalColor)
                continue;

            grid.Set(r, c, newColor);

            visitOrder[r, c] = step++;
            visited++;

            for (int i = 0; i < 4; i++)
                stack.Push((r + dr[i], c + dc[i]));
        }

        stopwatch.Stop();

        return new SearchResult
        {
            VisitMap = visitOrder,
            NodesVisited = visited,
            MaxFrontierSize = maxFrontier,
            Steps = step - 1,
            ExecutionTimeMs = stopwatch.Elapsed.TotalMilliseconds 
        };
    }
}