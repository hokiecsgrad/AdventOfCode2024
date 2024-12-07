using AdventOfCode.Common;
using AdventOfCode.Day06;
using Xunit;

namespace AdventOfCode.Day06.Tests;

public class Day06Tests
{
    public Day06Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe41()
    {
        Solver solver = new();
        int total = 0;

        total = int.Parse(solver.SolvePart1(data));

        Assert.Equal(41, total);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe6()
    {
        Solver solver = new();
        int total = 0;

        total = int.Parse(solver.SolvePart2(data));

        Assert.Equal(6, total);
    }

    private string[] data;
    private string sampleInput = """
....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...
""";
}
