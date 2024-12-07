using AdventOfCode.Common;
using AdventOfCode.Day07;
using Xunit;

namespace AdventOfCode.Day07.Tests;

public class Day07Tests
{
    public Day07Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void ParseData_WithSampleData_ShouldBeListOfLists()
    {
        Solver solver = new();
        List<(long, List<int>)> input = solver.ParseData(data);

        Assert.Equal(9, input.Count());
        Assert.Equal(7290, input[4].Item1);
        Assert.Equal(6, input[4].Item2[2]);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe3749()
    {
        Solver solver = new();
        long total = int.Parse(solver.SolvePart1(data));
        Assert.Equal(3749, total);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe11387()
    {
        Solver solver = new();
        long total = int.Parse(solver.SolvePart2(data));
        Assert.Equal(11387, total);
    }

    private string[] data;
    private string sampleInput = """
190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20
""";
}
