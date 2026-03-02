using SearchAlgorithmsCore.Models;

namespace SearchAlgorithmsCore.Interfaces;

public interface ISearchAlgorithm
{
    string Name { get; }
    int[,] Execute(Grid grid, int startRow, int startCol, int newColor);
}