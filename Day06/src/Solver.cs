using AdventOfCode.Common;

namespace AdventOfCode.Day06;

public class Solver
{
    private double MAX_PATH_LENGTH = 10_000;

    public string SolvePart1(string[] data)
    {
        char[,] grid = CreateGrid(data);
        List<(int, int)> path = RunPatrolPath(grid);
        int total = CountCharsInGrid('X', grid);

        // to save time on the solution to part 2, 
        // set the max path length to 1.5 the 
        // expected value
        MAX_PATH_LENGTH = Math.Floor(path.Count() * 1.5);

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        int total = 0;
        char[,] grid = CreateGrid(data);
        char[,] obstacleGrid = CreateGrid(data);
        (int row, int col) startPos = GetGuardPos(grid);

        List<(int, int)> path = RunPatrolPath(grid);

        HashSet<(int, int)> uniquePositions = new(path);
        foreach ((int row, int col) pos in uniquePositions)
        {
            if (pos == startPos) continue;

            List<(int, int)> currPath = new();

            grid = CreateGrid(data);
            grid[pos.row, pos.col] = 'O';

            currPath = RunPatrolPath(grid);
            if (currPath.Count() >= MAX_PATH_LENGTH)
                obstacleGrid[pos.row, pos.col] = 'O';
        }

        total = CountCharsInGrid('O', obstacleGrid);

        return total.ToString();
    }

    public char[,] CreateGrid(string[] data)
    {
        char[,] grid = new char[data.Length, data[0].Length];

        for (int row = 0; row < data.Length; row++)
            for (int col = 0; col < data[row].Length; col++)
                grid[row, col] = data[row][col];

        return grid;
    }

    public List<(int, int)> RunPatrolPath(char[,] grid)
    {
        List<(int, int)> path = new();
        (int row, int col) guardPos = GetGuardPos(grid);
        (int row, int col) guardDir = GetGuardDir(grid[guardPos.row, guardPos.col]);

        while (IsNexPosInGrid(guardPos, guardDir, grid.GetLength(0), grid.GetLength(1)))
        {
            if (IsNextPosIsObstacle(grid, guardPos, guardDir))
            {
                grid[guardPos.row, guardPos.col] = TurnRight(grid[guardPos.row, guardPos.col]);
                guardDir = GetGuardDir(grid[guardPos.row, guardPos.col]);
            }
            else
            {
                grid[guardPos.row + guardDir.row, guardPos.col + guardDir.col] =
                    grid[guardPos.row, guardPos.col];

                path.Add(guardPos);
                // logic to stop an infinite loop
                if (path.Count() > MAX_PATH_LENGTH)
                    return path;

                grid[guardPos.row, guardPos.col] = 'X';

                guardPos = (guardPos.row + guardDir.row, guardPos.col + guardDir.col);
            }
        }

        path.Add(guardPos);
        grid[guardPos.row, guardPos.col] = 'X';

        return path;
    }

    public (int, int) GetGuardPos(char[,] grid)
    {
        (int, int) pos = (-1, -1);

        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
                if (grid[row, col] == '^') return (row, col);
                else if (grid[row, col] == '<') return (row, col);
                else if (grid[row, col] == '>') return (row, col);
                else if (grid[row, col] == 'v') return (row, col);

        return pos;
    }

    public (int, int) GetGuardDir(char guard)
        => guard switch
        {
            '^' => (-1, 0),
            '>' => (0, 1),
            '<' => (0, -1),
            'v' => (1, 0),
            _ => (0, 0)
        };

    public bool IsNexPosInGrid(
            (int row, int col) guardPos,
            (int row, int col) guardDir,
            int gridRows, int gridCols)
        => guardPos.row + guardDir.row >= 0 &&
            guardPos.row + guardDir.row < gridRows &&
            guardPos.col + guardDir.col >= 0 &&
            guardPos.col + guardDir.col < gridCols;

    public bool IsNextPosIsObstacle(
            char[,] grid,
            (int row, int col) guardPos,
            (int row, int col) guardDir)
        => grid[guardPos.row + guardDir.row, guardPos.col + guardDir.col] == '#' ||
            grid[guardPos.row + guardDir.row, guardPos.col + guardDir.col] == 'O';

    public char TurnRight(char guard)
        => guard switch
        {
            '^' => '>',
            '>' => 'v',
            'v' => '<',
            '<' => '^',
            _ => '*'
        };

    public int CountCharsInGrid(char target, char[,] grid)
    {
        int total = 0;

        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
                if (grid[row, col] == target) total++;

        return total;
    }
}