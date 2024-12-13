using AdventOfCode.Common;
using AdventOfCode.Day12;
using Xunit;

namespace AdventOfCode.Day12.Tests;

public class Day12Tests
{
    public Day12Tests()
    {
        dataSimp = simpleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void ParseGrid_WithSimpleInput_ShouldWork()
    {
        Solver solver = new();
        char[,] farm = solver.ParseGrid(dataSimp);

        Assert.Equal(4, farm.GetLength(0));
        Assert.Equal(4, farm.GetLength(1));
        Assert.Equal('A', farm[0, 0]);
        Assert.Equal('B', farm[2, 1]);
        Assert.Equal('E', farm[3, 2]);
    }

    [Fact]
    public void FloodFill_WithSimpleData_ShouldWork()
    {
        Solver solver = new();
        char[,] farm = solver.ParseGrid(dataSimp);
        (long, long, HashSet<Point>) cost = solver.CreateGraphFromPoint(farm, new Point(0, 0));

        Assert.NotNull(cost.Item3);
        Assert.Equal(4, cost.Item1);
        Assert.Equal(10, cost.Item2);
    }

    [Fact]
    public void Part1_WithSimpleData_ShouldBe140()
    {
        Solver solver = new();
        long total = long.Parse(solver.SolvePart1(dataSimp));

        Assert.Equal(140, total);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe1930()
    {
        Solver solver = new();
        long total = long.Parse(solver.SolvePart1(data));

        Assert.Equal(1930, total);
    }

    [Fact]
    public void Part2_WithSimpleData_ShouldBe80()
    {
        Solver solver = new();
        long total = long.Parse(solver.SolvePart2(dataSimp));

        Assert.Equal(80, total);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe1206()
    {
        Solver solver = new();
        long total = long.Parse(solver.SolvePart2(data));

        Assert.Equal(1206, total);
    }

    private string[] data;
    private string[] dataSimp;
    private string simpleInput = """
AAAA
BBCD
BBCC
EEEC
""";
    private string sampleInput = """
RRRRIICCFF
RRRRIICCCF
VVRRRCCFFF
VVRCCCJFFF
VVVVCJJCFE
VVIVCCJJEE
VVIIICJJEE
MIIIIIJJEE
MIIISIJEEE
MMMISSJEEE
""";
}
