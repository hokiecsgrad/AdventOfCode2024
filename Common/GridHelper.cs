using System;
using System.Collections.Generic;

namespace AdventOfCode.Common;

public static class GridHelper
{
    public static T[,] CreateGrid<T>(int maxRows, int maxCols, T defaultValue = default(T))
    {
        T[,] grid = new T[maxRows, maxCols];

        for (int row = 0; row < maxRows; row++)
            for (int col = 0; col < maxCols; col++)
                grid[row, col] = defaultValue;

        return grid;
    }

    public static List<Point> GetAllNeighbors<T>(T[,] grid, Point currPosition)
        => GetAllNeighbors(grid, currPosition, x => true);

    public static List<Point> GetAllNeighbors<T>(T[,] grid, Point currPosition, Func<T, bool> comparer)
    {
        List<Point> neighbors = new();

        for (int row = -1; row < 1; row++)
        {
            for (int col = -1; col < 1; col++)
            {
                if (row == 0 && col == 0) { continue; }

                int newCol = currPosition.Col + col;
                int newRow = currPosition.Row + row;

                if (newRow >= 0 && newRow < grid.GetLength(0) &&
                    newCol >= 0 && newCol < grid.GetLength(1) &&
                    comparer(grid[newRow, newCol]))
                {
                    neighbors.Add(new Point(newRow, newCol));
                }
            }
        }

        return neighbors;
    }

    public static List<Node> GetOrthagonalNeighbors(Node[,] grid, Node currPosition)
        => GetOrthagonalNeighbors(grid, currPosition, x => true);

    public static List<Node> GetOrthagonalNeighbors(Node[,] grid, Node currPosition, Func<Node, bool> comparer)
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

    public static Node GetPositionOf(Node[,] grid, char target)
    {
        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
                if (grid[row, col].Value == target)
                    return grid[row, col];
        return new Node(new Point(-1, -1), '\0');
    }

    public static void PrintGrid<T>(T[,] grid)
    {
        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                Console.Write(grid[row, col]);
            }
            Console.WriteLine();
        }
    }
}