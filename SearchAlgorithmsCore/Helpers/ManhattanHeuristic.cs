using SearchAlgorithmsCore.Interfaces;

namespace SearchAlgorithmsCore.Helpers;

public class ManhattanHeuristic : IHeuristic
{
    public double Estimate(int r, int c, int goalR, int goalC)
    {
        return Math.Abs(r - goalR) + Math.Abs(c - goalC);
    }
}