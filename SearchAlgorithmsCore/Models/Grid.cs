namespace SearchAlgorithmsCore.Models;

public class Grid
{
    private readonly int[,] _cells;

    public int Rows => _cells.GetLength(0);
    public int Columns => _cells.GetLength(1);

    public Grid(int[,] cells)
    {
        _cells = cells;
    }

    public int Get(int row, int col) => _cells[row, col];
    public void Set(int row, int col, int value) => _cells[row, col] = value;

    public bool IsInside(int row, int col)
    {
        return row >= 0 && row < Rows && col >= 0 && col < Columns;
    }

    public void Print()
    {
        Console.WriteLine();
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
                Console.Write(_cells[i, j] + " ");
            Console.WriteLine();
        }
        Console.WriteLine();
    }
    
    public static void PrintVisitMap(int[,] visitMap)
    {
        Console.WriteLine();
        for (int i = 0; i < visitMap.GetLength(0); i++)
        {
            for (int j = 0; j < visitMap.GetLength(1); j++)
            {
                Console.Write($"{visitMap[i, j],3} ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}