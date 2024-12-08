using AdventOfCode.Common;
using AdventOfCode.Day08;
using Xunit;

namespace AdventOfCode.Day08.Tests;

public class Day08Tests
{
    public Day08Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void GetAntennas_WithSampleData_ShouldReturn0A()
    {
        Solver solver = new();
        HashSet<char> ants = solver.GetUniqueAntennas(data);
        Assert.Equal(2, ants.Count());
    }

    [Fact]
    public void CreateGrid_FromSampleData0_ShouldReturnGridWithOnly0()
    {
        Solver solver = new();
        char[,] grid = solver.CreateGridWithAntenna(data, '0');
        bool gridIsOnly0 = true;
        for (int row = 0; row < grid.GetLength(0); row++)
            for (int col = 0; col < grid.GetLength(1); col++)
                if (grid[row, col] == '.' || grid[row, col] == '0')
                    gridIsOnly0 = true;
                else
                    gridIsOnly0 = false;

        Assert.True(gridIsOnly0);
    }

    [Fact]
    public void GetPairs_FromSampleData0_ShouldReturn12()
    {
        Solver solver = new();
        char[,] grid = solver.CreateGridWithAntenna(data, '0');
        List<(Point, Point)> pairs = solver.GetAntennaPairs(grid);

        Assert.Equal(12, pairs.Count());
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe14()
    {
        int total = 0;
        Solver solver = new();

        total = int.Parse(solver.SolvePart1(data));

        Assert.Equal(14, total);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe34()
    {
        int total = 0;
        Solver solver = new();

        total = int.Parse(solver.SolvePart2(data));

        Assert.Equal(34, total);
    }

    private string[] data;
    private string sampleInput = """
............
........0...
.....0......
.......0....
....0.......
......A.....
............
............
........A...
.........A..
............
............
""";
}
