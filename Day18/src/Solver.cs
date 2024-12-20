using System.Drawing;
using AdventOfCode.Common;

namespace AdventOfCode.Day18;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        Node[,] maze = GridHelper.CreateGrid(71, 71, new Node(new Point(), '.'));
        List<Point> walls = ParseData(data);

        AddWallsToMaze(ref maze, walls.Take(1024).ToList());

        maze[0, 0] = new Node(new Point(0, 0), 'S');
        maze[maze.GetLength(0) - 1, maze.GetLength(1) - 1] =
            new Node(new Point(maze.GetLength(0) - 1, maze.GetLength(0) - 1), 'E');

        Stack<Node> path = AStar(maze, maze[0, 0], maze[70, 70]);

        return path.Count.ToString();
    }

    public string SolvePart2(string[] data)
    {
        Node[,] maze = GridHelper.CreateGrid(71, 71, new Node(new Point(), '.'));
        List<Point> walls = ParseData(data);

        AddWallsToMaze(ref maze, walls.Take(1024).ToList());

        maze[0, 0] = new Node(new Point(0, 0), 'S');
        maze[maze.GetLength(0) - 1, maze.GetLength(1) - 1] =
            new Node(new Point(maze.GetLength(0) - 1, maze.GetLength(0) - 1), 'E');

        int wallIndex = 1024;
        bool pathExists = true;
        while (pathExists)
        {
            Point currWall = walls.Skip(wallIndex).Take(1).First();
            maze[currWall.Row, currWall.Col] = new Node(new Point(currWall.Row, currWall.Col), '#');
            Stack<Node> path = AStar(maze, maze[0, 0], maze[70, 70]);
            if (path is null)
                pathExists = false;
            else
                wallIndex++;
        }

        return walls[wallIndex].ToString();
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

    public List<Node> GetNeighbors(Node[,] maze, Node curr)
    {
        List<Node> neighbors = new();

        int maxRow = maze.GetLength(0);
        int maxCol = maze.GetLength(1);

        if (curr.Position.Col + 1 < maxCol && maze[curr.Position.Row, curr.Position.Col + 1].Value != '#')
            neighbors.Add(maze[curr.Position.Row, curr.Position.Col + 1]);
        if (curr.Position.Row + 1 < maxRow && maze[curr.Position.Row + 1, curr.Position.Col].Value != '#')
            neighbors.Add(maze[curr.Position.Row + 1, curr.Position.Col]);
        if (curr.Position.Col - 1 >= 0 && maze[curr.Position.Row, curr.Position.Col - 1].Value != '#')
            neighbors.Add(maze[curr.Position.Row, curr.Position.Col - 1]);
        if (curr.Position.Row - 1 >= 0 && maze[curr.Position.Row - 1, curr.Position.Col].Value != '#')
            neighbors.Add(maze[curr.Position.Row - 1, curr.Position.Col]);

        return neighbors;
    }

    public void AddWallsToMaze(ref Node[,] maze, List<Point> walls)
    {
        for (int row = 0; row < maze.GetLength(0); row++)
            for (int col = 0; col < maze.GetLength(1); col++)
                if (walls.Contains(new Point(row, col)))
                    maze[row, col] = new Node(new Point(row, col), '#');
    }

    public List<Point> ParseData(string[] data)
    {
        List<Point> points = new();

        foreach (string line in data)
        {
            Point curr = new Point();
            curr.Row = int.Parse(line.Substring(0, line.IndexOf(',')));
            curr.Col = int.Parse(line.Substring(line.IndexOf(',') + 1));
            points.Add(curr);
        }

        return points;
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