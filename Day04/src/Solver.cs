using AdventOfCode.Common;

namespace AdventOfCode.Day04;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        int answer = 0;

        char[,] grid = CreateGrid(data);

        for (int y = 0; y < grid.GetLength(0); y++)
            for (int x = 0; x < grid.GetLength(1); x++)
                if (grid[y, x] == 'X')
                {
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            if (IsMas(grid, (y + i, x + j), (i, j)))
                                answer++;
                }

        return answer.ToString();
    }

    public string SolvePart2(string[] data)
    {
        int answer = 0;

        char[,] grid = CreateGrid(data);

        for (int y = 0; y < grid.GetLength(0); y++)
            for (int x = 0; x < grid.GetLength(1); x++)
                if (grid[y, x] == 'M')
                {
                    if (IsMas(grid, (y, x), (1, 1)) &&
                        IsMas(grid, (y, x + 2), (1, -1)))
                        answer++;
                    else if (IsMas(grid, (y, x), (1, 1)) &&
                        IsSam(grid, (y, x + 2), (1, -1)))
                        answer++;
                }
                else if (grid[y, x] == 'S')
                {
                    if (IsSam(grid, (y, x), (1, 1)) &&
                        IsMas(grid, (y, x + 2), (1, -1)))
                        answer++;
                    else if (IsSam(grid, (y, x), (1, 1)) &&
                        IsSam(grid, (y, x + 2), (1, -1)))
                        answer++;
                }

        return answer.ToString();
    }

    public char[,] CreateGrid(string[] data)
    {
        int height = data.Length;
        int width = data[0].ToCharArray().Length;

        char[,] grid = new char[height, width];

        for (int y = 0; y < height; y++)
        {
            char[] row = data[y].ToCharArray();
            for (int x = 0; x < width; x++)
                grid[y, x] = row[x];
        }

        return grid;
    }

    public (int, int)[] GetNeighborsThatAre(char target, char[,] grid, int y, int x)
    {
        List<(int, int)> neighbors = new();
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (
                    y + i >= 0 &&
                    y + i < grid.GetLength(0) &&
                    x + j >= 0 &&
                    x + j < grid.GetLength(1) &&
                    grid[y + i, x + j] == target
                    )
                    neighbors.Add((i, j));

        return neighbors.ToArray();
    }

    public bool IsMas(char[,] grid, (int y, int x) pos, (int i, int j) dir)
    {
        if (pos.y + (dir.i * 2) >= 0 &&
            pos.x + (dir.j * 2) >= 0 &&
            pos.y + (dir.i * 2) < grid.GetLength(0) &&
            pos.x + (dir.j * 2) < grid.GetLength(1) &&
            grid[pos.y, pos.x] == 'M' &&
            grid[pos.y + dir.i, pos.x + dir.j] == 'A' &&
            grid[pos.y + (dir.i * 2), pos.x + (dir.j * 2)] == 'S'
        )
            return true;
        else
            return false;
    }

    public bool IsSam(char[,] grid, (int y, int x) pos, (int i, int j) dir)
    {
        if (pos.y + (dir.i * 2) >= 0 &&
            pos.x + (dir.j * 2) >= 0 &&
            pos.y + (dir.i * 2) < grid.GetLength(0) &&
            pos.x + (dir.j * 2) < grid.GetLength(1) &&
            grid[pos.y, pos.x] == 'S' &&
            grid[pos.y + dir.i, pos.x + dir.j] == 'A' &&
            grid[pos.y + (dir.i * 2), pos.x + (dir.j * 2)] == 'M'
        )
            return true;
        else
            return false;
    }
}