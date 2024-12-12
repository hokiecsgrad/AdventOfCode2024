using AdventOfCode.Common;
using AdventOfCode.Day11;
using Xunit;

namespace AdventOfCode.Day11.Tests;

public class Day11Tests
{
    public Day11Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void Part1_WithSampleDataUsingMemo_ShouldBe22()
    {
        Solver solver = new();
        long[] temp = solver.ParseDataIntoArray(data[0]);
        Dictionary<long, long> stones = new();
        foreach (long num in temp) stones.Add(num, 1);

        solver.SimulateBlinks(ref stones, 6);

        long total = 0;
        foreach (var item in stones)
            total += item.Value;

        Assert.Equal(22, total);
    }

    private string[] data;
    private string sampleInput = """
125 17
""";
}
