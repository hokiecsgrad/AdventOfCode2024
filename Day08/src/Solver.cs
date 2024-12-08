using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Day08;

public class Solver
{
    private Regex AntPattern = new Regex(@"^[0-9a-zA-Z]{1}$");

    public string SolvePart1(string[] data)
    {
        int total = 0;
        char[,] antiGrid = new char[data.Length, data[0].Length];

        HashSet<char> ants = GetUniqueAntennas(data);
        char[,] currGrid;
        foreach (char antenna in ants)
        {
            currGrid = CreateGridWithAntenna(data, antenna);
            List<(Point, Point)> pairs = GetAntennaPairs(currGrid);
            foreach ((Point a, Point b) pair in pairs)
            {
                Vector antinode = (pair.a - pair.b) * 2;
                if (
                        pair.a.Row + antinode.Y >= 0 &&
                        pair.a.Col + antinode.X >= 0 &&
                        pair.a.Row + antinode.Y < antiGrid.GetLength(0) &&
                        pair.a.Col + antinode.X < antiGrid.GetLength(1))
                    antiGrid[pair.a.Row + antinode.Y, pair.a.Col + antinode.X] = '#';
            }
        }

        for (int row = 0; row < antiGrid.GetLength(0); row++)
            for (int col = 0; col < antiGrid.GetLength(1); col++)
                if (antiGrid[row, col] == '#')
                    total++;

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        int total = 0;
        char[,] antiGrid = new char[data.Length, data[0].Length];
        FillGrid(antiGrid, '.');

        HashSet<char> ants = GetUniqueAntennas(data);
        char[,] currGrid;
        foreach (char antenna in ants)
        {
            currGrid = CreateGridWithAntenna(data, antenna);
            List<(Point, Point)> pairs = GetAntennaPairs(currGrid);
            foreach ((Point a, Point b) pair in pairs)
            {
                int mult = 1;
                Vector antinode = (pair.a - pair.b);
                Point nextAntinodePoint = new Point()
                {
                    Row = pair.a.Row + (antinode * mult).Y,
                    Col = pair.a.Col + (antinode * mult).X
                };
                while (IsInBounds(antiGrid, nextAntinodePoint))
                {
                    antiGrid[nextAntinodePoint.Row, nextAntinodePoint.Col] = '#';
                    mult++;
                    nextAntinodePoint = new Point()
                    {
                        Row = pair.a.Row + (antinode * mult).Y,
                        Col = pair.a.Col + (antinode * mult).X
                    };
                }
            }
        }

        for (int row = 0; row < antiGrid.GetLength(0); row++)
            for (int col = 0; col < antiGrid.GetLength(1); col++)
                if (antiGrid[row, col] == '#')
                    total++;

        return total.ToString();
    }

    public bool IsInBounds(char[,] grid, Point point)
        => point.Row >= 0 &&
            point.Row < grid.GetLength(0) &&
            point.Col >= 0 &&
            point.Col < grid.GetLength(1);

    public List<(Point, Point)> GetAntennaPairs(char[,] currGrid)
    {
        List<(Point, Point)> pairs = new();
        List<Point> antenna = new();

        for (int row = 0; row < currGrid.GetLength(0); row++)
            for (int col = 0; col < currGrid.GetLength(1); col++)
                if (currGrid[row, col] != '.')
                    antenna.Add(new Point() { Row = row, Col = col });

        for (int i = 0; i < antenna.Count(); i++)
            for (int j = 0; j < antenna.Count(); j++)
                if (i != j) pairs.Add((antenna[i], antenna[j]));

        return pairs;
    }

    public HashSet<char> GetUniqueAntennas(string[] data)
    {
        HashSet<char> ants = new();
        for (int row = 0; row < data.Length; row++)
            for (int col = 0; col < data[row].Length; col++)
                if (AntPattern.IsMatch(data[row][col].ToString()))
                    if (!ants.Contains(data[row][col]))
                        ants.Add(data[row][col]);
        return ants;
    }

    public char[,] CreateGridWithAntenna(string[] data, char antenna)
    {
        char[,] grid = new char[data.Length, data[0].Length];

        for (int row = 0; row < data.Length; row++)
            for (int col = 0; col < data[row].Length; col++)
                if (data[row][col] == antenna)
                    grid[row, col] = data[row][col];
                else
                    grid[row, col] = '.';

        return grid;
    }

    public void FillGrid(char[,] grid, char fill)
    {
        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
                grid[row, col] = fill;
    }

    public void PrintGrid(char[,] grid)
    {
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
                Console.Write(grid[row, col]);
            Console.WriteLine();
        }
    }
}

public class Point
{
    public int Row { get; set; } = 0;
    public int Col { get; set; } = 0;

    public static Vector operator -(Point a, Point b)
        => new Vector() { Y = b.Row - a.Row, X = b.Col - a.Col };
}

public record Vector
{
    public int Y { get; set; } = 0;
    public int X { get; set; } = 0;

    public static Vector operator *(Vector a, int scale)
        => new Vector() { Y = a.Y * scale, X = a.X * scale };
}
