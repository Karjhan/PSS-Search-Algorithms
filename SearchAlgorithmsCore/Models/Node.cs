namespace SearchAlgorithmsCore.Models;

public class Node
{
    public int Row { get; set; }
    public int Col { get; set; }

    public double G { get; set; }
    public double H { get; set; } 
    public double F => G + H;

    public Node Parent { get; set; }

    public Node(int r, int c)
    {
        Row = r;
        Col = c;
    }
    
    public List<(int,int)> ReconstructPath()
    {
        var path = new List<(int,int)>();
        var current = this;

        while (current != null)
        {
            path.Add((current.Row, current.Col));
            current = current.Parent;
        }

        path.Reverse();
        return path;
    }
}