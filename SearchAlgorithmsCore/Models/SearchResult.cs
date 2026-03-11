namespace SearchAlgorithmsCore.Models;

public class SearchResult
{
    public int[,] VisitMap { get; set; }
    public int NodesVisited { get; set; }
    public int MaxFrontierSize { get; set; }
    public int Steps { get; set; }
    public double ExecutionTimeMs { get; set; }
}