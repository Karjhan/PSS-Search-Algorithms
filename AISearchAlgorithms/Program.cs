using SearchAlgorithmsCore.Algorithms;
using SearchAlgorithmsCore.Interfaces;
using SearchAlgorithmsCore.Models;

static void Main()
{
    Console.WriteLine("Choose Algorithm Type:");
    Console.WriteLine("1 - Uninformed Search");
    Console.Write("Selection: ");
    var typeChoice = Console.ReadLine();

    if (typeChoice == "1")
    {
        RunUninformedFloodFill();
    }
    else
    {
        Console.WriteLine("Invalid selection.");
    }
}

static void RunUninformedFloodFill()
{
    Console.WriteLine("\nChoose Algorithm:");
    Console.WriteLine("1 - BFS");
    Console.WriteLine("2 - DFS");
    Console.Write("Selection: ");
    var algoChoice = Console.ReadLine();

    ISearchAlgorithm algorithm = algoChoice switch
    {
        "1" => new BfsFloodFill(),
        "2" => new DfsFloodFill(),
        _ => null
    };

    if (algorithm == null)
    {
        Console.WriteLine("Invalid algorithm.");
        return;
    }

    int[,] initialGrid =
    {
        {1,1,1,2,2,3,3,3,4,4},
        {1,0,1,2,2,3,0,3,4,4},
        {1,1,1,0,2,3,3,3,4,0},
        {5,5,0,0,2,2,2,0,4,4},
        {5,5,5,0,0,0,2,4,4,4},
        {6,6,5,5,5,0,2,2,2,4},
        {6,0,0,0,5,0,0,0,2,4},
        {6,6,6,0,5,5,5,0,2,4},
        {7,7,6,0,0,0,5,0,2,4},
        {7,7,6,6,6,0,5,5,2,4}
    };

    var grid = new Grid(initialGrid);

    Console.WriteLine("\nInitial Grid:");
    grid.Print();

    Console.Write("Enter start row: ");
    int row = int.Parse(Console.ReadLine());

    Console.Write("Enter start column: ");
    int col = int.Parse(Console.ReadLine());

    Console.Write("Enter new color: ");
    int newColor = int.Parse(Console.ReadLine());

    var visitMap = algorithm.Execute(grid, row, col, newColor);

    Console.WriteLine($"\nFinal Grid using {algorithm.Name}:");
    grid.Print();

    Console.WriteLine("Visit Order Map:");
    Grid.PrintVisitMap(visitMap);
}

Main();