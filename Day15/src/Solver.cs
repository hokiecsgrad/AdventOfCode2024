using AdventOfCode.Common;

namespace AdventOfCode.Day15;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        char[,] grid = ParseGrid(data);
        List<char> instructions = ParseInstructions(data);
        Point robot = FindCharInGrid(grid, '@');
        Point newPos = new Point(robot.Row, robot.Col);

        foreach (char dir in instructions)
            newPos = MoveRobot(ref grid, newPos, dir);

        long gps = CalculateGps(grid);

        return gps.ToString();
    }

    public string SolvePart2(string[] data)
    {
        char[,] smallGrid = ParseGrid(data);
        char[,] grid = ScaleUpGrid(smallGrid);
        List<char> instructions = ParseInstructions(data);
        Point robot = FindCharInGrid(grid, '@');
        Point newPos = new Point(robot.Row, robot.Col);

        foreach (char dir in instructions)
            newPos = MoveRobot(ref grid, newPos, dir);

        long gps = CalculateLargeGps(grid);

        return gps.ToString();
    }

    public Point MoveRobot(ref char[,] grid, Point robot, char dir)
    {
        Point moveDir = dir switch
        {
            '<' => new Point(0, -1),
            '^' => new Point(-1, 0),
            '>' => new Point(0, 1),
            'v' => new Point(1, 0),
            _ => new Point(-1, -1)
        };

        Point newPos = new Point(robot.Row + moveDir.Row, robot.Col + moveDir.Col);

        if (!PointIsInBounds(grid, newPos) || IsWall(grid, newPos))
            return robot;

        if (IsSmallBox(grid, newPos) || (IsBigBox(grid, newPos) && (dir == '<' || dir == '>')))
        {
            Point testPoint = new Point(newPos.Row, newPos.Col);
            while (PointIsInBounds(grid, testPoint) && IsBox(grid, testPoint))
                testPoint = new Point(testPoint.Row + moveDir.Row, testPoint.Col + moveDir.Col);

            if (IsWall(grid, testPoint))
            {
                return robot;
            }
            else if (IsEmpty(grid, testPoint))
            {
                Point revDir = new Point(moveDir.Row, moveDir.Col);
                revDir.Row = moveDir.Row * -1;
                revDir.Col = moveDir.Col * -1;
                while (!IsRobot(grid, testPoint))
                {
                    grid[testPoint.Row, testPoint.Col] =
                        grid[testPoint.Row + revDir.Row, testPoint.Col + revDir.Col];
                    testPoint = new Point(testPoint.Row + revDir.Row, testPoint.Col + revDir.Col);
                }
                grid[testPoint.Row, testPoint.Col] = '.';
                newPos = new Point(testPoint.Row + moveDir.Row, testPoint.Col + moveDir.Col);
            }
        }
        else if (IsBigBox(grid, newPos))
        {
            List<Point> box = new();
            if (grid[newPos.Row, newPos.Col] == '[')
            {
                box.Add(new Point(newPos.Row, newPos.Col));
                box.Add(new Point(newPos.Row, newPos.Col + 1));
            }
            else
            {
                box.Add(new Point(newPos.Row, newPos.Col - 1));
                box.Add(new Point(newPos.Row, newPos.Col));
            }
            if (CanMoveBigBoxUp(grid, box, dir == '^' ? -1 : 1))
            {
                MoveBigBoxUp(ref grid, box, dir == '^' ? -1 : 1);
                grid[newPos.Row, newPos.Col] = '@';
                grid[robot.Row, robot.Col] = '.';
            }
            else
                return robot;
        }
        else
        {
            grid[newPos.Row, newPos.Col] = '@';
            grid[robot.Row, robot.Col] = '.';
        }

        return newPos;
    }

    public bool CanMoveBigBoxUp(char[,] grid, List<Point> boxCoords, int dir)
    {
        if (IsWall(grid, new Point(boxCoords[0].Row + dir, boxCoords[0].Col)) || IsWall(grid, new Point(boxCoords[1].Row + dir, boxCoords[1].Col)))
            return false;
        if (IsEmpty(grid, new Point(boxCoords[0].Row + dir, boxCoords[0].Col)) && IsEmpty(grid, new Point(boxCoords[1].Row + dir, boxCoords[1].Col)))
            return true;

        bool canMoveUp = true;

        if (grid[boxCoords[0].Row + dir, boxCoords[0].Col] == '[')
            canMoveUp = canMoveUp && CanMoveBigBoxUp(grid, new List<Point> { new Point(boxCoords[0].Row + dir, boxCoords[0].Col), new Point(boxCoords[1].Row + dir, boxCoords[1].Col) }, dir);
        else if (grid[boxCoords[0].Row + dir, boxCoords[0].Col] == ']' && IsEmpty(grid, new Point(boxCoords[1].Row + dir, boxCoords[1].Col)))
            canMoveUp = canMoveUp && CanMoveBigBoxUp(grid, new List<Point> { new Point(boxCoords[0].Row + dir, boxCoords[0].Col - 1), new Point(boxCoords[0].Row + dir, boxCoords[0].Col) }, dir);
        else if (IsEmpty(grid, new Point(boxCoords[0].Row + dir, boxCoords[0].Col)) && grid[boxCoords[1].Row + dir, boxCoords[1].Col] == '[')
            canMoveUp = canMoveUp && CanMoveBigBoxUp(grid, new List<Point> { new Point(boxCoords[1].Row + dir, boxCoords[1].Col), new Point(boxCoords[1].Row + dir, boxCoords[1].Col + 1) }, dir);
        else if (grid[boxCoords[0].Row + dir, boxCoords[0].Col] == ']' && grid[boxCoords[1].Row + dir, boxCoords[1].Col] == '[')
        {
            canMoveUp = canMoveUp && CanMoveBigBoxUp(grid, new List<Point> { new Point(boxCoords[0].Row + dir, boxCoords[0].Col - 1), new Point(boxCoords[0].Row + dir, boxCoords[0].Col) }, dir);
            canMoveUp = canMoveUp && CanMoveBigBoxUp(grid, new List<Point> { new Point(boxCoords[1].Row + dir, boxCoords[1].Col), new Point(boxCoords[1].Row + dir, boxCoords[1].Col + 1) }, dir);
        }

        return canMoveUp;
    }

    public void MoveBigBoxUp(ref char[,] grid, List<Point> boxCoords, int dir)
    {
        if (IsWall(grid, new Point(boxCoords[0].Row + dir, boxCoords[0].Col)) || IsWall(grid, new Point(boxCoords[1].Row + dir, boxCoords[1].Col)))
            return;

        if (grid[boxCoords[0].Row + dir, boxCoords[0].Col] == '[')
            MoveBigBoxUp(ref grid, new List<Point> { new Point(boxCoords[0].Row + dir, boxCoords[0].Col), new Point(boxCoords[1].Row + dir, boxCoords[1].Col) }, dir);
        else if (grid[boxCoords[0].Row + dir, boxCoords[0].Col] == ']' && IsEmpty(grid, new Point(boxCoords[1].Row + dir, boxCoords[1].Col)))
            MoveBigBoxUp(ref grid, new List<Point> { new Point(boxCoords[0].Row + dir, boxCoords[0].Col - 1), new Point(boxCoords[0].Row + dir, boxCoords[0].Col) }, dir);
        else if (IsEmpty(grid, new Point(boxCoords[0].Row + dir, boxCoords[0].Col)) && grid[boxCoords[1].Row + dir, boxCoords[1].Col] == '[')
            MoveBigBoxUp(ref grid, new List<Point> { new Point(boxCoords[1].Row + dir, boxCoords[1].Col), new Point(boxCoords[1].Row + dir, boxCoords[1].Col + 1) }, dir);
        else if (grid[boxCoords[0].Row + dir, boxCoords[0].Col] == ']' && grid[boxCoords[1].Row + dir, boxCoords[1].Col] == '[')
        {
            MoveBigBoxUp(ref grid, new List<Point> { new Point(boxCoords[0].Row + dir, boxCoords[0].Col - 1), new Point(boxCoords[0].Row + dir, boxCoords[0].Col) }, dir);
            MoveBigBoxUp(ref grid, new List<Point> { new Point(boxCoords[1].Row + dir, boxCoords[1].Col), new Point(boxCoords[1].Row + dir, boxCoords[1].Col + 1) }, dir);
        }

        if (IsEmpty(grid, new Point(boxCoords[0].Row + dir, boxCoords[0].Col)) && IsEmpty(grid, new Point(boxCoords[1].Row + dir, boxCoords[1].Col)))
        {
            grid[boxCoords[0].Row + dir, boxCoords[0].Col] = '[';
            grid[boxCoords[1].Row + dir, boxCoords[1].Col] = ']';
            grid[boxCoords[0].Row, boxCoords[0].Col] = '.';
            grid[boxCoords[1].Row, boxCoords[1].Col] = '.';
            return;
        }
    }

    public long CalculateGps(char[,] grid)
    {
        long total = 0;
        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
                if (grid[row, col] == 'O')
                    total += (100 * row) + col;
        return total;
    }

    public long CalculateLargeGps(char[,] grid)
    {
        long total = 0;
        int midRow = grid.GetLength(0) / 2;
        int midCol = grid.GetLength(1) / 2;
        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
                if (grid[row, col] == '[')
                    total += (100 * row) + col;
        return total;
    }

    public char[,] ScaleUpGrid(char[,] grid)
    {
        char[,] newGrid = new char[grid.GetLength(0), grid.GetLength(1) * 2];

        for (int row = 0; row < grid.GetLength(0); row++)
        {
            for (int col = 0; col < grid.GetLength(1); col++)
            {
                if (grid[row, col] == '#' || grid[row, col] == '.')
                {
                    newGrid[row, col * 2] = grid[row, col];
                    newGrid[row, col * 2 + 1] = grid[row, col];
                }
                else if (grid[row, col] == 'O')
                {
                    newGrid[row, col * 2] = '[';
                    newGrid[row, col * 2 + 1] = ']';
                }
                else if (grid[row, col] == '@')
                {
                    newGrid[row, col * 2] = '@';
                    newGrid[row, col * 2 + 1] = '.';
                }
            }
        }

        return newGrid;
    }

    public char[,] ParseGrid(string[] data)
    {
        int maxRow = 0;
        while (data[maxRow] != string.Empty) maxRow++;
        char[,] grid = new char[maxRow, data[0].Length];

        int row = 0;
        while (row < maxRow)
        {
            for (int col = 0; col < data[row].Length; col++)
                grid[row, col] = data[row][col];
            row++;
        }

        return grid;
    }

    public List<char> ParseInstructions(string[] data)
    {
        List<char> instructions = new();
        int row = 0;
        while (data[row] != string.Empty) row++;
        row++;

        while (row < data.Length)
        {
            for (int col = 0; col < data[row].Length; col++)
                instructions.Add(data[row][col]);
            row++;
        }

        return instructions;
    }

    public Point FindCharInGrid(char[,] grid, char target)
    {
        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
                if (grid[row, col] == target)
                    return new Point(row, col);
        return new Point(-1, -1);
    }

    public bool IsWall(char[,] grid, Point pos)
    {
        return grid[pos.Row, pos.Col] == '#';
    }

    public bool IsBox(char[,] grid, Point pos)
    {
        return grid[pos.Row, pos.Col] == 'O' ||
            grid[pos.Row, pos.Col] == '[' ||
            grid[pos.Row, pos.Col] == ']';
    }

    public bool IsSmallBox(char[,] grid, Point pos)
    {
        return grid[pos.Row, pos.Col] == 'O';
    }

    public bool IsBigBox(char[,] grid, Point pos)
    {
        return grid[pos.Row, pos.Col] == '[' ||
            grid[pos.Row, pos.Col] == ']';
    }

    public bool IsRobot(char[,] grid, Point pos)
    {
        return grid[pos.Row, pos.Col] == '@';
    }

    public bool IsEmpty(char[,] grid, Point pos)
    {
        return grid[pos.Row, pos.Col] == '.';
    }

    public bool PointIsInBounds(char[,] grid, Point pos)
    {
        return pos.Row >= 0 && pos.Row < grid.GetLength(0) &&
                pos.Col >= 0 && pos.Col < grid.GetLength(1);
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