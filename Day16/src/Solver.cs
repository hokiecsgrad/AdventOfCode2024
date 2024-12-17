using AdventOfCode.Common;

namespace AdventOfCode.Day16;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        Node[,] grid = ParseGrid(data);
        Node start = FindCharInGrid(grid, 'S');
        Node end = FindCharInGrid(grid, 'E');

        Stack<Node> path = AStar(grid, start, end);

        long total = path.Last().Cost;

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        Node[,] grid = ParseGrid(data);
        Node start = FindCharInGrid(grid, 'S');
        Node end = FindCharInGrid(grid, 'E');

        //var shortestPaths = null;

        int total = 0;

        return total.ToString();
    }

    public Stack<Node> AStar(Node[,] maze, Node start, Node goal)
    {
        Stack<Node> path = new();
        PriorityQueue<Node, int> openList = new();
        List<Node> closedList = new();

        start.Cost = 0;
        start.DistanceToTarget = Math.Abs(goal.Position.Row - start.Position.Row) + Math.Abs(goal.Position.Col - start.Position.Col);
        start.Dir = '>';
        openList.Enqueue(start, start.F);

        Node current = new Node(new Point(-1, -1), 'S');
        while (openList.Count > 0 && !closedList.Exists(x => x == goal))
        {
            current = openList.Dequeue();
            closedList.Add(current);

            foreach (Node neighbor in GetNeighbors(maze, current))
            {
                if (!closedList.Contains(neighbor))
                {
                    bool isFound = false;
                    foreach (var openListNode in openList.UnorderedItems)
                        if (openListNode.Element == neighbor)
                            isFound = true;

                    if (!isFound)
                    {
                        Point newDirVec = new Point(neighbor.Position.Row - current.Position.Row, neighbor.Position.Col - current.Position.Col);
                        char newDir = current.Dir;
                        if (newDirVec.Row == 0 && newDirVec.Col == 1) newDir = '>';
                        if (newDirVec.Row == 0 && newDirVec.Col == -1) newDir = '<';
                        if (newDirVec.Row == 1 && newDirVec.Col == 0) newDir = 'v';
                        if (newDirVec.Row == -1 && newDirVec.Col == 0) newDir = '^';

                        int weight = 0;
                        if (current.Dir != newDir) { weight = 1000; }
                        neighbor.Dir = newDir;
                        neighbor.Parent = current;
                        neighbor.Cost = weight + current.Cost + 1;
                        neighbor.DistanceToTarget = Math.Abs(goal.Position.Row - neighbor.Position.Row) + Math.Abs(goal.Position.Col - neighbor.Position.Col);
                        openList.Enqueue(neighbor, neighbor.F);
                    }
                }
            }
        }

        if (!closedList.Exists(x => x == goal))
            return null;

        Node temp = closedList[closedList.IndexOf(current)];
        if (temp == null) return null;
        do
        {
            path.Push(temp);
            temp = temp.Parent;
        } while (temp != start && temp != null);

        return path;
    }

    public List<Node> GetNeighbors(Node[,] maze, Node curr)
    {
        List<Node> neighbors = new();

        if (maze[curr.Position.Row, curr.Position.Col + 1].Value != '#')
            neighbors.Add(maze[curr.Position.Row, curr.Position.Col + 1]);
        if (maze[curr.Position.Row + 1, curr.Position.Col].Value != '#')
            neighbors.Add(maze[curr.Position.Row + 1, curr.Position.Col]);
        if (maze[curr.Position.Row, curr.Position.Col - 1].Value != '#')
            neighbors.Add(maze[curr.Position.Row, curr.Position.Col - 1]);
        if (maze[curr.Position.Row - 1, curr.Position.Col].Value != '#')
            neighbors.Add(maze[curr.Position.Row - 1, curr.Position.Col]);

        return neighbors;
    }

    public Node[,] ParseGrid(string[] data)
    {
        Node[,] grid = new Node[data.Length, data[0].Length];

        for (int row = 0; row < data.Length; row++)
            for (int col = 0; col < data[row].Length; col++)
                grid[row, col] = new Node(new Point(row, col), data[row][col]);

        return grid;
    }

    public Node FindCharInGrid(Node[,] grid, char target)
    {
        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
                if (grid[row, col].Value == target)
                    return grid[row, col];
        return null;
    }

    public void PrintGrid(Node[,] grid, List<Point> points)
    {
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (points.Contains(new Point(row, col)))
                    Console.Write('O');
                else
                    Console.Write(grid[row, col].Value);
            }
            Console.WriteLine();
        }
    }
}

public class Node
{
    public Node Parent { get; set; } = null;
    public Point Position { get; set; }
    public char Value { get; set; }
    public int Cost { get; set; }
    public int DistanceToTarget { get; set; }
    public int F { get { return Cost + DistanceToTarget; } }
    public char Dir { get; set; }

    public Node(Point pos, char val)
    {
        Position = pos;
        Value = val;
        Cost = int.MaxValue;
        DistanceToTarget = int.MaxValue;
    }
}

public class Point : IComparable<Point>
{
    public int Row { get; set; }
    public int Col { get; set; }

    public Point(int row, int col)
    {
        Row = row;
        Col = col;
    }

    public Point(Point other)
    {
        Row = other.Row;
        Col = other.Col;
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        if (obj is Point otherPoint)
            return CompareTo(otherPoint);
        else
            throw new ArgumentException("Object is not a Point");
    }

    public int CompareTo(Point other)
    {
        if (other == null) return 1;

        int rowComparison = this.Row.CompareTo(other.Row);
        if (rowComparison != 0)
        {
            return rowComparison;
        }
        else
        {
            return this.Col.CompareTo(other.Col);
        }
    }

    public static bool operator ==(Point p1, Point p2)
    {
        if (ReferenceEquals(p1, p2)) return true;
        if (p1 is null || p2 is null) return false;

        return p1.Row == p2.Row && p1.Col == p2.Col;
    }

    public static bool operator !=(Point p1, Point p2)
    {
        return !(p1 == p2);
    }

    public override bool Equals(object obj)
    {
        if (obj is Point point)
        {
            return this == point;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Row, Col);
    }
}