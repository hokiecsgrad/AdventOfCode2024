using AdventOfCode.Common;
using AdventOfCode.Day17;
using Xunit;

namespace AdventOfCode.Day17.Tests;

public class Day17Tests
{
    public Day17Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries
            );
    }

    [Fact]
    public void ParseRegisters_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        Dictionary<char, long> registers = new();

        registers = solver.ParseRegisters(data);

        Assert.Equal(729, registers['A']);
        Assert.Equal(0, registers['B']);
        Assert.Equal(0, registers['C']);
    }

    [Fact]
    public void ParseProgram_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        List<int> program = solver.ParseProgram(data);

        Assert.Equal(new List<int> { 0, 1, 5, 4, 3, 0 }, program);
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
Register A: 729
Register B: 0
Register C: 0

Program: 0,1,5,4,3,0
""";
}
