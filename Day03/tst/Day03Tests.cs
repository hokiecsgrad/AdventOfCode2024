using System.Text.RegularExpressions;
using AdventOfCode.Common;
using AdventOfCode.Day03;
using Xunit;

namespace AdventOfCode.Day03.Tests;

public class Day03Tests
{
    public Day03Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe161()
    {
        Solver solver = new();

        Assert.Equal(161, solver.SumMulOpsInLine(data[0]));
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe48()
    {
        Solver solver = new();

        Regex condPattern = new Regex(@"don't\(\).*?do\(\)");
        string line = condPattern.Replace(data[1], "");

        Assert.Equal(48, solver.SumMulOpsInLine(line));
    }

    private string[] data;
    private string sampleInput = """
xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))
xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))
""";
}
