
namespace AdventOfCode.Reference;

public class Node
{
    public Node? Parent { get; set; } = null;
    public char Value { get; set; }
    public Point Position { get; set; }
    public int Cost { get; set; }
    public int DistanceToTarget { get; set; }
    public int F { get { return Cost + DistanceToTarget; } }

    public Node(Point pos, char val)
    {
        Position = pos;
        Value = val;
        Cost = int.MaxValue;
    }

    public Node()
    {
        Position = new Point(-1, -1);
        Value = '\0';
        Cost = int.MaxValue;
    }
}