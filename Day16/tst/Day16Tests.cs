using AdventOfCode.Common;
using AdventOfCode.Day16;
using Xunit;

namespace AdventOfCode.Day16.Tests;

public class Day16Tests
{
    public Day16Tests()
    {
        data1 = sampleInput1.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
        data2 = sampleInput2.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void ParseGrid_WithSampleData1_ShouldWork()
    {
        Solver solver = new();
        Node[,] grid = solver.ParseGrid(data1);
        Node start = solver.FindCharInGrid(grid, 'S');

        Assert.Equal(15, grid.GetLength(0));
        Assert.Equal(15, grid.GetLength(1));
        Assert.Equal(new Point(13, 1), start.Position);
    }

    [Fact]
    public void AStar_WithSampleData1_ShouldWork()
    {
        Solver solver = new();
        Node[,] grid = solver.ParseGrid(data1);
        Node start = solver.FindCharInGrid(grid, 'S');
        Node end = solver.FindCharInGrid(grid, 'E');

        Stack<Node> path = solver.AStar(grid, start, end);

        long total = path.Last().Cost;

        Assert.Equal(7036, total);
        Assert.Equal(15, grid.GetLength(0));
        Assert.Equal(15, grid.GetLength(1));
        Assert.Equal(new Point(13, 1), start.Position);
    }

    [Fact]
    public void Part1_WithSampleData1_ShouldBe7036()
    {
        Solver solver = new();

        long total = long.Parse(solver.SolvePart1(data1));

        Assert.Equal(7036, total);
    }

    [Fact]
    public void Part1_WithSampleData2_ShouldBe11048()
    {
        Solver solver = new();

        long total = long.Parse(solver.SolvePart1(data2));

        Assert.Equal(11048, total);
    }

    [Fact]
    public void Part2_WithSampleData1_ShouldBe45()
    {
        Solver solver = new();

        long total = long.Parse(solver.SolvePart2(data1));

        Assert.Equal(45, total);
    }

    [Fact]
    public void Part2_WithSampleData2_ShouldBe64()
    {
        Solver solver = new();

        long total = long.Parse(solver.SolvePart2(data2));

        Assert.Equal(64, total);
    }

    private string[] data1;
    private string sampleInput1 = """
###############
#.......#....E#
#.#.###.#.###.#
#.....#.#...#.#
#.###.#####.#.#
#.#.#.......#.#
#.#.#####.###.#
#...........#.#
###.#.#####.#.#
#...#.....#.#.#
#.#.#.###.#.#.#
#.....#...#.#.#
#.###.#.#.#.#.#
#S..#.....#...#
###############
""";
    private string[] data2;
    private string sampleInput2 = """
#################
#...#...#...#..E#
#.#.#.#.#.#.#.#.#
#.#.#.#...#...#.#
#.#.#.#.###.#.#.#
#...#.#.#.....#.#
#.#.#.#.#.#####.#
#.#...#.#.#.....#
#.#.#####.#.###.#
#.#.#.......#...#
#.#.###.#####.###
#.#.#...#.....#.#
#.#.#.#####.###.#
#.#.#.........#.#
#.#.#.#########.#
#S#.............#
#################
""";
}
