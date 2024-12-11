using System.Drawing;
using AdventOfCode.Common;
using AdventOfCode.Day10;
using Xunit;

namespace AdventOfCode.Day10.Tests;

public class Day10Tests
{
    public Day10Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void TraverseMap_WithSimpleExmpl_ShouldReturn1()
    {
        sampleInput = """
0123
1234
8765
9876
""";
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );

        Solver solver = new();

        int[,] map = solver.ParseMap(data);
        int numPaths = solver.TraverseMapFrom(map, new Point(0, 0)).Distinct().Count();

        Assert.Equal(1, numPaths);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe36()
    {
        Solver solver = new();

        int numPaths = int.Parse(solver.SolvePart1(data));

        Assert.Equal(36, numPaths);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe81()
    {
        Solver solver = new();

        int numPaths = int.Parse(solver.SolvePart2(data));

        Assert.Equal(81, numPaths);
    }

    private string[] data;
    private string sampleInput = """
89010123
78121874
87430965
96549874
45678903
32019012
01329801
10456732
""";
}
