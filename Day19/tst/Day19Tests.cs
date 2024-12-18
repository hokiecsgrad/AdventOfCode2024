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
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBeXXX()
    {
        throw new NotImplementedException();
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBeXXX()
    {
        throw new NotImplementedException();
    }

    private string[] data;
    private string sampleInput = """
SAMPLE INPUT HERE
""";
}
