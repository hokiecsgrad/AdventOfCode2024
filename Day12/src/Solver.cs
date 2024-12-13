using System.Drawing;
using AdventOfCode.Common;

namespace AdventOfCode.Day12;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        long total = 0;
        char[,] farm = ParseGrid(data);

        HashSet<Point> pointsInGraphs = new();
        for (int row = 0; row < farm.GetLength(0); row++)
        {
            for (int col = 0; col < farm.GetLength(1); col++)
            {
                Point currPos = new Point(row, col);
                if (!pointsInGraphs.Contains(currPos))
                {
                    (long, long, HashSet<Point>) cost;
                    cost = CreateGraphFromPoint(farm, currPos);
                    foreach (var point in cost.Item3)
                        pointsInGraphs.Add(point);
                    total += cost.Item1 * cost.Item2;
                }
            }
        }

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        return string.Empty;
    }

    public (long, long, HashSet<Point>) CreateGraphFromPoint(char[,] grid, Point start)
    {
        HashSet<Point> visited = new HashSet<Point>();
        (long, long) dims = FloodFillGrid(grid, start, null, "", grid[start.Row, start.Col], ref visited);
        return (dims.Item1, dims.Item2, visited);
    }

    public (long, long) FloodFillGrid(char[,] grid, Point pos, FarmGraphNode? from, string fromDir, char crop, ref HashSet<Point> visited)
    {
        if (pos.Row < 0 || pos.Row >= grid.GetLength(0) || pos.Col < 0 || pos.Col >= grid.GetLength(1))
            return (0, 0);
        if (grid[pos.Row, pos.Col] != crop) return (0, 0);
        if (visited.Contains(pos)) return (0, 0);

        visited.Add(pos);
        FarmGraphNode curr = new();
        curr.Position = pos;
        curr.Value = grid[pos.Row, pos.Col];
        switch (fromDir)
        {
            case "north":
                curr.North = from;
                from.South = curr;
                break;
            case "east":
                curr.East = from;
                from.West = curr;
                break;
            case "west":
                curr.West = from;
                from.East = curr;
                break;
            case "south":
                curr.South = from;
                from.North = curr;
                break;
        }

        (long, long) n = (0, 0);
        (long, long) e = (0, 0);
        (long, long) w = (0, 0);
        (long, long) s = (0, 0);
        if (curr.North is null) n = FloodFillGrid(grid, new Point(pos.Row - 1, pos.Col), curr, "south", crop, ref visited);
        else curr.North.South = curr;
        if (curr.East is null) e = FloodFillGrid(grid, new Point(pos.Row, pos.Col + 1), curr, "west", crop, ref visited);
        else curr.East.West = curr;
        if (curr.West is null) w = FloodFillGrid(grid, new Point(pos.Row, pos.Col - 1), curr, "east", crop, ref visited);
        else curr.West.East = curr;
        if (curr.South is null) s = FloodFillGrid(grid, new Point(pos.Row + 1, pos.Col), curr, "north", crop, ref visited);
        else curr.South.North = curr;

        long area = 1 + n.Item1 + e.Item1 + w.Item1 + s.Item1;

        long peri = 0;
        if (curr.North is null)
            if (!visited.Contains(new Point(pos.Row - 1, pos.Col)))
                peri += 1;
            else
                curr.North = new FarmGraphNode();
        if (curr.East is null)
            if (!visited.Contains(new Point(pos.Row, pos.Col + 1)))
                peri += 1;
            else
                curr.East = new FarmGraphNode();
        if (curr.West is null)
            if (!visited.Contains(new Point(pos.Row, pos.Col - 1)))
                peri += 1;
            else
                curr.West = new FarmGraphNode();
        if (curr.South is null)
            if (!visited.Contains(new Point(pos.Row + 1, pos.Col)))
                peri += 1;
            else
                curr.South = new FarmGraphNode();
        peri += n.Item2 + e.Item2 + w.Item2 + s.Item2;

        return (area, peri);
    }

    public char[,] ParseGrid(string[] data)
    {
        char[,] grid = new char[data.Length, data[0].Length];

        for (int row = 0; row < data.Length; row++)
            for (int col = 0; col < data[row].Length; col++)
                grid[row, col] = data[row][col];

        return grid;
    }
}

public class FarmGraph
{
    public HashSet<Point> UniquePoints { get; set; } = new();
    public FarmGraphNode? Root { get; set; }

    public bool Contains(Point curr)
        => UniquePoints.Contains(curr);
}

public class FarmGraphNode
{
    public Point Position { get; set; }
    public char Value { get; set; }
    public FarmGraphNode? North { get; set; } = null;
    public FarmGraphNode? East { get; set; } = null;
    public FarmGraphNode? South { get; set; } = null;
    public FarmGraphNode? West { get; set; } = null;
}

public class Point
{
    public int Row { get; set; } = -1;
    public int Col { get; set; } = -1;

    public Point(int y, int x)
    {
        Row = y;
        Col = x;
    }

    public override bool Equals(object? obj)
    {
        if (obj is Point other)
            return Row == other.Row && Col == other.Col;
        return false;
    }

    public override int GetHashCode()
        => HashCode.Combine(Row, Col);
}