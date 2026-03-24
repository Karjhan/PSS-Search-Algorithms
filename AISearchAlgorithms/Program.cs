using SearchAlgorithmsCore.Algorithms;
using SearchAlgorithmsCore.Algorithms.Informed;
using SearchAlgorithmsCore.Helpers;
using SearchAlgorithmsCore.Interfaces;
using SearchAlgorithmsCore.Models;

static void Main()
{
    Console.WriteLine("Choose Algorithm Type:");
    Console.WriteLine("1 - Uninformed Search");
    Console.WriteLine("2 - Informed Search");
    Console.Write("Selection: ");
    var typeChoice = Console.ReadLine();

    if (typeChoice == "1")
    {
        RunUninformedFloodFill();
    }
    else if (typeChoice == "2")
    {
        RunInformedSearch();
    }
    else
    {
        Console.WriteLine("Invalid selection.");
    }
}

static void RunInformedSearch()
{
    Console.WriteLine("\nChoose Heuristic:");
    Console.WriteLine("1 - Manhattan");
    Console.WriteLine("2 - Euclidean");
    Console.Write("Selection: ");
    var choice = Console.ReadLine();

    IHeuristic? heuristic = choice switch
    {
        "1" => new ManhattanHeuristic(),
        "2" => new EuclideanHeuristic(),
        _ => null
    };
    if (heuristic == null)
    {
        Console.WriteLine("Invalid heuristic.");
        return;
    }

    Console.WriteLine("\nChoose Mode:");
    Console.WriteLine("1 - A*");
    Console.WriteLine("2 - Greedy Best First");
    Console.WriteLine("3 - Compare A* vs Greedy");
    Console.Write("Selection: ");
    var mode = Console.ReadLine();

    int[,] terrain =
    {
        {1,1,1,5,5,5},
        {1,9,1,9,9,1},
        {1,9,1,1,1,1},
        {1,9,9,9,1,9},
        {1,1,1,1,1,1},
        {5,5,5,5,1,1}
    };

    var grid = new TerrainGrid(terrain)
    {
        Start = (0, 0),
        Goal = (5, 5)
    };

    Console.WriteLine("\nTerrain Grid:");
    grid.Print();

    Console.WriteLine($"\nStart: {grid.Start}");
    Console.WriteLine($"Goal: {grid.Goal}");

    var astar = new AStarNavigation(heuristic);
    var greedy = new GreedyBestFirstNavigation(heuristic);

    switch (mode)
    {
        case "1":
        {
            var (path, cost, visited) = astar.FindPath(grid);
            Console.WriteLine("\n=== A* Result ===");
            if (path == null)
            {
                Console.WriteLine("No path found.");
                return;
            }
            foreach (var step in path)
                Console.Write($"{step} -> ");
            Console.WriteLine($"Cost: {cost}");
            Console.WriteLine($"Visited Nodes: {visited}");
            break;
        }
        case "2":
        {
            var (path, cost, visited) = greedy.FindPath(grid);
            Console.WriteLine("\n=== Greedy Result ===");
            if (path == null)
            {
                Console.WriteLine("No path found.");
                return;
            }
            foreach (var step in path)
                Console.Write($"{step} -> ");
            Console.WriteLine($"Cost: {cost}");
            Console.WriteLine($"Visited Nodes: {visited}");
            break;
        }
        case "3":
        {
            RunInformedComparison(heuristic, grid);
            break;
        }
        default:
            Console.WriteLine("Invalid mode.");
            break;
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
        {1,1,1,0,2,2,2,0,3,3,3,0,4,4,4},
        {1,0,1,0,2,0,2,0,3,0,3,0,4,0,4},
        {1,1,1,0,2,2,2,0,3,3,3,0,4,4,4},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {5,5,0,6,6,6,0,7,7,7,0,8,8,8,0},
        {5,0,0,6,0,6,0,7,0,7,0,8,0,8,0},
        {5,5,0,6,6,6,0,7,7,7,0,8,8,8,0},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {9,9,9,0,10,10,10,0,11,11,11,0,12,12,12},
        {9,0,9,0,10,0,10,0,11,0,11,0,12,0,12},
        {9,9,9,0,10,10,10,0,11,11,11,0,12,12,12},
        {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0},
        {13,13,14,14,0,15,15,16,16,0,17,17,18,18,0},
        {13,0,14,0,0,15,0,16,0,0,17,0,18,0,0},
        {13,13,14,14,0,15,15,16,16,0,17,17,18,18,0}
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

static void RunInformedComparison(IHeuristic heuristic, TerrainGrid grid)
{
    var astar = new AStarNavigation(heuristic);
    var greedy = new GreedyBestFirstNavigation(heuristic);

    var (aPath, aCost, aVisited) = astar.FindPath(grid);
    var (gPath, gCost, gVisited) = greedy.FindPath(grid);

    Console.WriteLine("\n=== A* Result ===");
    if (aPath == null)
    {
        Console.WriteLine("No path found.");
        return;
    }
    foreach (var step in aPath)
        Console.Write($"{step} -> ");
    Console.WriteLine($"Cost: {aCost}");
    Console.WriteLine($"Visited Nodes: {aVisited}");

    Console.WriteLine("\n=== Greedy Result ===");
    if (gPath == null)
    {
        Console.WriteLine("No path found.");
        return;
    }
    foreach (var step in gPath)
        Console.Write($"{step} -> ");
    Console.WriteLine($"Cost: {gCost}");
    Console.WriteLine($"Visited Nodes: {gVisited}");
}

Main();