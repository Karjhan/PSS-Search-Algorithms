using SearchAlgorithmsCore.Interfaces;

namespace SearchAlgorithmsCore.Helpers;

public class EuclideanHeuristic : IHeuristic
{
    public double Estimate(int r, int c, int goalR, int goalC)
    {
        return Math.Sqrt(Math.Pow(r - goalR, 2) + Math.Pow(c - goalC, 2));
    }
}