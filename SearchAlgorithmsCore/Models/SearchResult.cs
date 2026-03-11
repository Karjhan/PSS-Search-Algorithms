namespace SearchAlgorithmsCore.Models;

public class SearchResult
{
    public int[,] VisitMap { get; set; }
    public int NodesVisited { get; set; }
    public int MaxFrontierSize { get; set; }
    public int Steps { get; set; }
    public long ExecutionTimeMs { get; set; }
}