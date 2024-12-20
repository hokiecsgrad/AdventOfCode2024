using AdventOfCode.Common;
using AdventOfCode.Day19;
using Xunit;

namespace AdventOfCode.Day19.Tests;

public class Day19Tests
{
    public Day19Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries
            );
    }

    [Fact]
    public void ParsePatterns_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        List<string> patterns = solver.ParsePatterns(data);
        List<string> expected = new List<string> { "r", "wr", "b", "g", "bwu", "rb", "gb", "br" };
        Assert.Equal(expected, patterns);
    }

    [Fact]
    public void ParseDesigns_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        List<string> designs = solver.ParseDesigns(data);

        Assert.Equal(8, designs.Count);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe6()
    {
        Solver solver = new();
        int total = 0;

        total = int.Parse(solver.SolvePart1(data));

        Assert.Equal(6, total);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe16()
    {
        Solver solver = new();
        int total = 0;

        total = int.Parse(solver.SolvePart2(data));

        Assert.Equal(16, total);
    }

    private string[] data;
    private string sampleInput = """
r, wr, b, g, bwu, rb, gb, br

brwrr
bggr
gbbr
rrbgbr
ubwu
bwurrg
brgr
bbrgwb
""";
}
