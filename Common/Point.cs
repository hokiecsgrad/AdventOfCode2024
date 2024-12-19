using System;

namespace AdventOfCode.Common;

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