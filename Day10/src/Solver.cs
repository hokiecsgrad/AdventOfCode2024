using System.Drawing;
using AdventOfCode.Common;

namespace AdventOfCode.Day10;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        int numPaths = 0;
        int[,] map = ParseMap(data);

        for (int row = 0; row < map.GetLength(0); row++)
            for (int col = 0; col < map.GetLength(1); col++)
                if (map[row, col] == 0)
                    numPaths += TraverseMapFrom(map, new Point() { Y = row, X = col }).Distinct().Count();

        return numPaths.ToString();
    }

    public string SolvePart2(string[] data)
    {
        int numPaths = 0;
        int[,] map = ParseMap(data);

        for (int row = 0; row < map.GetLength(0); row++)
            for (int col = 0; col < map.GetLength(1); col++)
                if (map[row, col] == 0)
                    numPaths += TraverseMapFrom(map, new Point() { Y = row, X = col }).Count();

        return numPaths.ToString();
    }

    public List<Point> TraverseMapFrom(int[,] map, Point start)
    {
        List<Point> trailEnds = new();
        TraverseMap(map, start, trailEnds);
        return trailEnds;
    }

    public List<Point> TraverseMap(int[,] map, Point curr, List<Point> trailEnds)
    {
        if (map[curr.Y, curr.X] == 9) trailEnds.Add(curr);

        List<Point> nextSteps = GetValidNeighbors(map, curr);
        foreach (Point step in nextSteps)
            TraverseMap(map, step, trailEnds);

        return trailEnds;
    }

    public List<Point> GetValidNeighbors(int[,] map, Point curr)
    {
        List<Point> neighbors = new();

        if (curr.Y > 0 && map[curr.Y - 1, curr.X] == map[curr.Y, curr.X] + 1)
            neighbors.Add(new Point() { Y = curr.Y - 1, X = curr.X });
        if (curr.X + 1 < map.GetLength(1) && map[curr.Y, curr.X + 1] == map[curr.Y, curr.X] + 1)
            neighbors.Add(new Point() { Y = curr.Y, X = curr.X + 1 });
        if (curr.Y + 1 < map.GetLength(0) && map[curr.Y + 1, curr.X] == map[curr.Y, curr.X] + 1)
            neighbors.Add(new Point() { Y = curr.Y + 1, X = curr.X });
        if (curr.X - 1 >= 0 && map[curr.Y, curr.X - 1] == map[curr.Y, curr.X] + 1)
            neighbors.Add(new Point() { Y = curr.Y, X = curr.X - 1 });

        return neighbors;
    }

    public int[,] ParseMap(string[] data)
    {
        int[,] map = new int[data.Length, data[0].Length];

        for (int row = 0; row < data.Length; row++)
            for (int col = 0; col < data[row].Length; col++)
                if (data[row][col] != '.')
                    map[row, col] = int.Parse(data[row][col].ToString());
                else
                    map[row, col] = -1;

        return map;
    }
}