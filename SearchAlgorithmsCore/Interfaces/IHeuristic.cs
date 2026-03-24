namespace SearchAlgorithmsCore.Interfaces;

public interface IHeuristic
{
    double Estimate(int r, int c, int goalR, int goalC);
}