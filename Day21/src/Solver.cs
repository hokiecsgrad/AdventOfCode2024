using System.Text;
using AdventOfCode.Common;

namespace AdventOfCode.Day21;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        long total = 0;

        foreach (string code in data)
        {
            string path = GetPathForCodepad(code);

            path = GetPathForDirpad(path);

            path = GetPathForDirpad(path);
            total += int.Parse(code.Substring(0, 3)) * path.Length;
        }

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        long total = 0;

        foreach (string code in data)
        {
            string path = GetPathForCodepad(code);

            for (int i = 0; i < 24; i++)
            {
                path = GetPathForDirpad(path);
                Console.WriteLine($"Completed path {i}.");
            }

            path = GetPathForDirpad(path);

            total += int.Parse(code.Substring(0, 3)) * path.Length;
        }

        return total.ToString();
    }

    public string GetPathForCodepad(string code)
    {
        StringBuilder currPath = new();

        char[,] codepad = CreateCodepad();

        Point start = GetPointWithValue(codepad, 'A');
        Point goal = new Point();
        for (int i = 0; i < code.Length; i++)
        {
            goal = GetPointWithValue(codepad, code[i]);

            List<char> path = GetCodepadPath(start, goal);

            foreach (char step in path)
                currPath.Append(step);
            currPath.Append('A');

            start = goal;
        }

        return currPath.ToString();
    }

    public string GetPathForDirpad(string code)
    {
        StringBuilder currPath = new();

        char[,] dirpad = CreateDirectionpad();

        Point start = GetPointWithValue(dirpad, 'A');
        Point goal = new Point();
        for (int i = 0; i < code.Length; i++)
        {
            goal = GetPointWithValue(dirpad, code[i]);

            List<char> path = GetDirpadPath(start, goal);

            foreach (char step in path)
                currPath.Append(step);
            currPath.Append('A');

            start = goal;
        }

        return currPath.ToString();
    }

    public List<char> GetCodepadPath(Point start, Point goal)
    {
        List<char> path = new();

        Point curr = start;
        Point diff = goal - curr;

        if (curr.Col == 0 && goal.Row == 3)
        {
            while (diff.Col > 0)
            {
                curr.Col++;
                if (curr.Row == 3 && curr.Col == 0) throw new Exception();
                path.Add('>');
                diff = goal - curr;
            }
            while (diff.Row > 0)
            {
                curr.Row++;
                if (curr.Row == 3 && curr.Col == 0) throw new Exception();
                path.Add('v');
                diff = goal - curr;
            }
        }
        else if (curr.Row == 3 && goal.Col == 0)
        {
            while (diff.Row < 0)
            {
                curr.Row--;
                if (curr.Row == 3 && curr.Col == 0) throw new Exception();
                path.Add('^');
                diff = goal - curr;
            }
            while (diff.Col < 0)
            {
                curr.Col--;
                if (curr.Row == 3 && curr.Col == 0) throw new Exception();
                path.Add('<');
                diff = goal - curr;
            }
        }
        else
        {
            while (diff.Col < 0)
            {
                curr.Col--;
                if (curr.Row == 3 && curr.Col == 0) throw new Exception();
                path.Add('<');
                diff = goal - curr;
            }
            while (diff.Row > 0)
            {
                curr.Row++;
                if (curr.Row == 3 && curr.Col == 0) throw new Exception();
                path.Add('v');
                diff = goal - curr;
            }
            while (diff.Row < 0)
            {
                curr.Row--;
                if (curr.Row == 3 && curr.Col == 0) throw new Exception();
                path.Add('^');
                diff = goal - curr;
            }
            while (diff.Col > 0)
            {
                curr.Col++;
                if (curr.Row == 3 && curr.Col == 0) throw new Exception();
                path.Add('>');
                diff = goal - curr;
            }
        }

        return path;
    }

    public List<char> GetDirpadPath(Point start, Point goal)
    {
        List<char> path = new();

        Point curr = start;
        Point diff = goal - curr;

        while (diff.Row > 0)
        {
            curr.Row++;
            path.Add('v');
            diff = goal - curr;
        }
        while (diff.Col > 0)
        {
            curr.Col++;
            path.Add('>');
            diff = goal - curr;
        }
        while (diff.Col < 0)
        {
            curr.Col--;
            path.Add('<');
            diff = goal - curr;
        }
        while (diff.Row < 0)
        {
            curr.Row--;
            path.Add('^');
            diff = goal - curr;
        }

        return path;
    }

    public Point GetPointWithValue(char[,] grid, char val)
    {
        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
                if (grid[row, col] == val) return new Point(row, col);
        return new Point();
    }

    public char[,] CreateCodepad()
    {
        char[,] codepad = new char[4, 3];
        string[] codepadMap = new string[4] {
            "789",
            "456",
            "123",
            "#0A"};

        for (int row = 0; row < 4; row++)
            for (int col = 0; col < 3; col++)
                codepad[row, col] = codepadMap[row][col];

        return codepad;
    }

    public char[,] CreateDirectionpad()
    {
        char[,] dirpad = new char[2, 3];
        string[] dirpadMap = new string[2] {
            "#^A",
            "<v>"};

        for (int row = 0; row < 2; row++)
            for (int col = 0; col < 3; col++)
                dirpad[row, col] = dirpadMap[row][col];

        return dirpad;
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

    public static Point operator -(Point p1, Point p2)
    {
        return new Point(p1.Row - p2.Row, p1.Col - p2.Col);
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