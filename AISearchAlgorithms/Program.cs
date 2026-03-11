using SearchAlgorithmsCore.Algorithms;
using SearchAlgorithmsCore.Helpers;
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
    Console.WriteLine("3 - Compare BFS vs DFS");
    Console.Write("Selection: ");
    var algoChoice = Console.ReadLine();
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

    switch (algoChoice)
    {
        case "1":
            ISearchAlgorithm bfs = new BfsFloodFill();
            var bfsResult = bfs.Execute(grid, row, col, newColor);
            Console.WriteLine($"\nFinal Grid using {bfs.Name}:");
            grid.Print();
            Console.WriteLine("Visit Order Map:");
            Grid.PrintVisitMap(bfsResult.VisitMap);
            break;
        case "2":
            ISearchAlgorithm dfs = new DfsFloodFill();
            var dfsResult = dfs.Execute(grid, row, col, newColor);
            Console.WriteLine($"\nFinal Grid using {dfs.Name}:");
            grid.Print();
            Console.WriteLine("Visit Order Map:");
            Grid.PrintVisitMap(dfsResult.VisitMap);
            break;
        case "3":
            RunUninformedComparison(grid, row, col, newColor);
            break;
        default:
            Console.WriteLine("Invalid selection.");
            break;
    }
}
static void RunUninformedComparison(Grid grid, int row, int col, int newColor)
{
    ISearchAlgorithm bfs = new BfsFloodFill();
    ISearchAlgorithm dfs = new DfsFloodFill();

    var bfsGrid = new Grid((int[,])grid.Cells.Clone());
    var dfsGrid = new Grid((int[,])grid.Cells.Clone());

    var bfsResult = bfs.Execute(bfsGrid, row, col, newColor);
    var dfsResult = dfs.Execute(dfsGrid, row, col, newColor);

    Console.WriteLine("\nAlgorithm: BFS");
    DisplayHelpers.PrintStats(bfsResult);

    Console.WriteLine("\nAlgorithm: DFS");
    DisplayHelpers.PrintStats(dfsResult);

    Console.WriteLine("\nBFS Visit Map:");
    Grid.PrintVisitMap(bfsResult.VisitMap);

    Console.WriteLine("\nDFS Visit Map:");
    Grid.PrintVisitMap(dfsResult.VisitMap);
}

Main();