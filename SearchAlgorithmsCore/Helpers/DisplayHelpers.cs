using SearchAlgorithmsCore.Models;

namespace SearchAlgorithmsCore.Helpers;

public static class DisplayHelpers
{
    public static void PrintStats(SearchResult result)
    {
        Console.WriteLine($"Nodes Visited: {result.NodesVisited}");
        Console.WriteLine($"Steps: {result.Steps}");
        Console.WriteLine($"Max Frontier Size: {result.MaxFrontierSize}");
        Console.WriteLine($"Execution Time: {result.ExecutionTimeMs} ms");
    }
}