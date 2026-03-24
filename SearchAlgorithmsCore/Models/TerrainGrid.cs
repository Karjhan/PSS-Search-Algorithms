namespace SearchAlgorithmsCore.Models;

public class TerrainGrid
{
    private readonly int[,] _cells;

    public int Rows => _cells.GetLength(0);
    public int Columns => _cells.GetLength(1);

    public (int r, int c) Start { get; set; }
    public (int r, int c) Goal { get; set; }

    public TerrainGrid(int[,] cells)
    {
        _cells = cells;
    }

    public int GetCost(int r, int c)
    {
        return _cells[r, c];
    }

    public bool IsInside(int r, int c)
    {
        return r >= 0 && r < Rows && c >= 0 && c < Columns;
    }

    public void Print()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
                Console.Write($"{_cells[i,j],2} ");
            Console.WriteLine();
        }
    }
}