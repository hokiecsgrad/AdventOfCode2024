using AdventOfCode.Common;
using AdventOfCode.Day01;
using Xunit;

namespace AdventOfCode.Day01.Tests;

public class Day01Tests
{
    public Day01Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void Part1List_WithSampleData_ShouldBeExpected()
    {
        Solver solver = new();
        var actuals = solver.SplitArrayIntoTwo(data);

        List<int> expected1 = [3, 4, 2, 1, 3, 3];
        List<int> expected2 = [4, 3, 5, 3, 9, 3];

        Assert.Equal(actuals.Item1, expected1);
        Assert.Equal(actuals.Item2, expected2);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe11()
    {
        Solver solver = new();
        var actuals = solver.SplitArrayIntoTwo(data);
        int sum = solver.OrderAndSumTheDiffs(actuals.Item1, actuals.Item2);

        Assert.Equal(11, sum);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe31()
    {
        Solver solver = new();
        var actuals = solver.SplitArrayIntoTwo(data);
        int sum = solver.SumSimilarityScores(actuals.Item1, actuals.Item2);

        Assert.Equal(31, sum);
    }

    private string[] data;
    private string sampleInput = """
3    4
4    3
2    5
1    3
3    9
3    3
""";
}
