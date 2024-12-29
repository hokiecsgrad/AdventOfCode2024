using AdventOfCode.Common;

namespace AdventOfCode.Day20;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        int total = 0;
        List<Stack<Node>> allPaths = new();

        Node[,] maze = ParseGrid(data);
        Node start = GetPositionOf(maze, 'S');
        Node end = GetPositionOf(maze, 'E');
        Stack<Node> path = AStar(maze, start, end);
        int normalSolution = path.Count;
        Console.WriteLine($"Normal path is {normalSolution} points long.");

        List<Node> wholePath = new() { start };
        foreach (Node point in path)
            wholePath.Add(point);

        List<Node> pathSoFar = new();
        foreach (Node pos in wholePath)
        {
            Console.WriteLine($"Testing path # {pathSoFar.Count + 1}");
            List<Node> cheats = GetNeighbors(maze, pos, pathSoFar);
            foreach (Node cheat in cheats)
            {
                maze[cheat.Position.Row, cheat.Position.Col].Value = '.';
                Stack<Node> newPath = AStar(maze, start, end);
                allPaths.Add(newPath);
                maze[cheat.Position.Row, cheat.Position.Col].Value = '#';
            }

            pathSoFar.Add(pos);
        }

        total = allPaths.Where(p => p.Count <= normalSolution - 100).Count();

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        return String.Empty;
    }

    public Stack<Node> AStar(Node[,] maze, Node start, Node goal)
    {
        Stack<Node> path = new();
        PriorityQueue<Node, int> openList = new();
        List<Node> closedList = new();

        start.Cost = 0;
        start.DistanceToTarget = Math.Abs(goal.Position.Row - start.Position.Row) + Math.Abs(goal.Position.Col - start.Position.Col);
        openList.Enqueue(start, start.F);

        Node current = new Node(new Point(), 'S');
        while (openList.Count > 0 && !closedList.Exists(x => x == goal))
        {
            current = openList.Dequeue();
            closedList.Add(current);

            foreach (Node neighbor in GetOrthagonalNeighbors(maze, current, (x) => x.Value != '#'))
            {
                if (!closedList.Contains(neighbor))
                {
                    bool isFound = false;
                    foreach (var openListNode in openList.UnorderedItems)
                        if (openListNode.Element == neighbor)
                            isFound = true;

                    if (!isFound)
                    {
                        int weight = 0;
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

    public List<Node> GetNeighbors(Node[,] maze, Node curr, List<Node> pathSoFar)
    {
        List<Node> neighbors = new();

        int row = curr.Position.Row - 1;
        int col = curr.Position.Col;
        if (row >= 1 &&
            maze[row, col].Value == '#' &&
            !pathSoFar.Contains(maze[row - 1, col]) &&
            (maze[row - 1, col].Value == '.' ||
            maze[row - 1, col].Value == 'E')
            )
            neighbors.Add(maze[row, col]);

        row = curr.Position.Row + 1;
        if (row < maze.GetLength(0) - 1 &&
            maze[row, col].Value == '#' &&
            !pathSoFar.Contains(maze[row + 1, col]) &&
            (maze[row + 1, col].Value == '.' ||
            maze[row + 1, col].Value == 'E')
            )
            neighbors.Add(maze[row, col]);

        row = curr.Position.Row;
        col = curr.Position.Col - 1;
        if (col >= 1 &&
            maze[row, col].Value == '#' &&
            !pathSoFar.Contains(maze[row, col - 1]) &&
            (maze[row, col - 1].Value == '.' ||
            maze[row, col - 1].Value == 'E')
            )
            neighbors.Add(maze[row, col]);

        col = curr.Position.Col + 1;
        if (col < maze.GetLength(1) - 1 &&
            maze[row, col].Value == '#' &&
            !pathSoFar.Contains(maze[row, col + 1]) &&
            (maze[row, col + 1].Value == '.' ||
            maze[row, col + 1].Value == 'E')
            )
            neighbors.Add(maze[row, col]);

        return neighbors;
    }

    public List<Node> GetOrthagonalNeighbors(Node[,] grid, Node currPosition, Func<Node, bool> comparer)
    {
        List<Node> neighbors = new();

        int row = currPosition.Position.Row - 1;
        int col = currPosition.Position.Col;
        if (row >= 0 && comparer(grid[row, col]))
            neighbors.Add(grid[row, col]);

        row = currPosition.Position.Row + 1;
        if (row < grid.GetLength(0) && comparer(grid[row, col]))
            neighbors.Add(grid[row, col]);

        row = currPosition.Position.Row;
        col = currPosition.Position.Col - 1;
        if (col >= 0 && comparer(grid[row, col]))
            neighbors.Add(grid[row, col]);

        col = currPosition.Position.Col + 1;
        if (col < grid.GetLength(1) && comparer(grid[row, col]))
            neighbors.Add(grid[row, col]);

        return neighbors;
    }

    public Node GetPositionOf(Node[,] grid, char target)
    {
        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
                if (grid[row, col].Value == target)
                    return grid[row, col];
        return new Node(new Point(-1, -1), '\0');
    }

    public Node[,] ParseGrid(string[] data)
    {
        Node[,] grid = new Node[data.Length, data[0].Length];

        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                Node curr = new Node(new Point(row, col), data[row][col]);
                grid[row, col] = curr;
            }
        return grid;
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

    public Point()
    {
        Row = -1; Col = -1;
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

    public override string ToString()
    {
        return $"({Row}, {Col})";
    }
}

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