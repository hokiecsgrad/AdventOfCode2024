using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Day14;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        long total = 0;

        List<(Point pos, Vector vel)> robots = new();
        robots = ParseData(data);

        robots = MoveRobotsNumTimes(100, 103, 101, robots);

        List<(Point pos, Vector vel)> q1 = robots.Where(rob => rob.pos.Row < 51 && rob.pos.Col < 50).ToList();
        List<(Point pos, Vector vel)> q2 = robots.Where(rob => rob.pos.Row < 51 && rob.pos.Col > 50).ToList();
        List<(Point pos, Vector vel)> q3 = robots.Where(rob => rob.pos.Row > 51 && rob.pos.Col < 50).ToList();
        List<(Point pos, Vector vel)> q4 = robots.Where(rob => rob.pos.Row > 51 && rob.pos.Col > 50).ToList();

        total = q1.Count() * q2.Count() * q3.Count() * q4.Count();

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        int dangerLevel = int.MaxValue;
        int dangerIndex = 0;

        List<(Point pos, Vector vel)> robots = new();
        robots = ParseData(data);

        int numTimes = 10_000;
        int i = 0;
        for (i = 0; i < numTimes; i++)
        {
            for (int robIndex = 0; robIndex < robots.Count(); robIndex++)
                robots[robIndex] = MoveRobot(103, 101, robots[robIndex]);

            List<(Point pos, Vector vel)> q1 = robots.Where(rob => rob.pos.Row < 51 && rob.pos.Col < 50).ToList();
            List<(Point pos, Vector vel)> q2 = robots.Where(rob => rob.pos.Row < 51 && rob.pos.Col > 50).ToList();
            List<(Point pos, Vector vel)> q3 = robots.Where(rob => rob.pos.Row > 51 && rob.pos.Col < 50).ToList();
            List<(Point pos, Vector vel)> q4 = robots.Where(rob => rob.pos.Row > 51 && rob.pos.Col > 50).ToList();

            int currDanger = q1.Count() * q2.Count() * q3.Count() * q4.Count();
            if (currDanger < dangerLevel)
            {
                dangerLevel = currDanger;
                dangerIndex = i;
            }

            /*
            if (i == 8158)
            {
                PrintGrid(103, 101, robots);
                Console.WriteLine($"Testing iteration {i}");
                while (Console.ReadKey(true).Key != ConsoleKey.Spacebar) { }
                //System.Threading.Thread.Sleep(100);
            }
            else
            {
                Console.WriteLine($"Testing iteration {i}");
            }
            */
        }

        return dangerIndex.ToString();
    }

    public void PrintGrid(int maxRows, int maxCols, List<(Point pos, Vector vel)> robots)
    {
        Console.Clear();
        robots.Sort((a, b) => a.pos.CompareTo(b.pos));
        int currRobot = 0;
        for (int row = 0; row < maxRows; row++)
        {
            for (int col = 0; col < maxCols; col++)
                if (currRobot < robots.Count() && robots[currRobot].pos == new Point(row, col))
                { Console.Write("*"); while (currRobot < robots.Count() && robots[currRobot].pos == new Point(row, col)) currRobot++; }
                else
                    Console.Write(".");
            Console.WriteLine();
        }
    }

    public List<(Point, Vector)> MoveRobotsNumTimes(int numTimes, int maxRows, int maxCols, List<(Point, Vector)> robots)
    {
        for (int i = 0; i < numTimes; i++)
            for (int robIndex = 0; robIndex < robots.Count(); robIndex++)
                robots[robIndex] = MoveRobot(maxRows, maxCols, robots[robIndex]);

        return robots;
    }

    public (Point pos, Vector vel) MoveRobot(int maxRows, int maxCols, (Point pos, Vector vel) robot)
    {
        (Point pos, Vector vel) newPos = robot;
        newPos.pos = robot.pos + robot.vel;
        if (newPos.pos.Row < 0)
            newPos.pos.Row = newPos.pos.Row + maxRows;
        if (newPos.pos.Row >= maxRows)
            newPos.pos.Row = newPos.pos.Row - maxRows;
        if (newPos.pos.Col < 0)
            newPos.pos.Col = newPos.pos.Col + maxCols;
        if (newPos.pos.Col >= maxCols)
            newPos.pos.Col = newPos.pos.Col - maxCols;
        return newPos;
    }

    public List<(Point, Vector)> ParseData(string[] data)
    {
        List<(Point pos, Vector vel)> robots = new();

        foreach (string line in data)
            robots.Add(ParseLine(line));

        return robots;
    }

    public (Point, Vector) ParseLine(string line)
    {
        Regex pointPattern = new Regex(@"^p=([\d]+),([\d]+) v=([-]{0,1}[\d]+),([-]{0,1}[\d]+)$");

        var matches = pointPattern.Match(line);

        Point pos = new Point(int.Parse(matches.Groups[2].Value), int.Parse(matches.Groups[1].Value));
        Vector vel = new Vector(int.Parse(matches.Groups[4].Value), int.Parse(matches.Groups[3].Value));

        return (pos, vel);
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

    public static Point operator +(Point p, Vector v)
    {
        return new Point(p.Row + v.Rows, p.Col + v.Cols);
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

public class Vector
{
    public int Rows { get; set; }
    public int Cols { get; set; }

    public Vector(int rows, int cols)
    {
        Rows = rows;
        Cols = cols;
    }

    public static Point operator +(Vector v, Point p)
    {
        return new Point(p.Row + v.Rows, p.Col + v.Cols);
    }

    public static bool operator ==(Vector v1, Vector v2)
    {
        return v1.Rows == v2.Rows && v1.Cols == v2.Cols;
    }

    public static bool operator !=(Vector v1, Vector v2)
    {
        return !(v1 == v2);
    }

    public override bool Equals(object obj)
    {
        if (obj is Vector vector)
        {
            return this == vector;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Rows, Cols);
    }
}