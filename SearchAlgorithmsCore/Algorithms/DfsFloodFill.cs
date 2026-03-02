using SearchAlgorithmsCore.Interfaces;
using SearchAlgorithmsCore.Models;

namespace SearchAlgorithmsCore.Algorithms;

public class DfsFloodFill : ISearchAlgorithm
{
    public string Name => "DFS";

    public int[,] Execute(Grid grid, int startRow, int startCol, int newColor)
    {
        int originalColor = grid.Get(startRow, startCol);
        int[,] visitOrder = new int[grid.Rows, grid.Columns];

        if (originalColor == newColor)
            return visitOrder;

        var stack = new Stack<(int r, int c)>();
        stack.Push((startRow, startCol));

        int[] dr = { -1, 1, 0, 0 };
        int[] dc = { 0, 0, -1, 1 };

        int step = 1;

        while (stack.Count > 0)
        {
            var (r, c) = stack.Pop();

            if (!grid.IsInside(r, c))
                continue;

            if (grid.Get(r, c) != originalColor)
                continue;

            grid.Set(r, c, newColor);
            visitOrder[r, c] = step++;

            for (int i = 0; i < 4; i++)
                stack.Push((r + dr[i], c + dc[i]));
        }

        return visitOrder;
    }
}