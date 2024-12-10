using AdventOfCode.Common;
using AdventOfCode.Day09;
using Xunit;

namespace AdventOfCode.Day09.Tests;

public class Day09Tests
{
    public Day09Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void ExplodeMap_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        List<long> expected = "00...111...2...333.44.5555.6666.777.888899"
            .Select(c => c == '.' ? -1 : (long)char.GetNumericValue(c))
            .ToList();
        List<long> hdd = solver.ExplodeMap(data[0]);
        Assert.Equal(expected, hdd);
    }

    [Fact]
    public void Defrag_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        List<long> hdd = "00...111...2...333.44.5555.6666.777.888899"
            .Select(c => c == '.' ? -1 : (long)char.GetNumericValue(c))
            .ToList();
        List<long> expected = "0099811188827773336446555566.............."
            .Select(c => c == '.' ? -1 : (long)char.GetNumericValue(c))
            .ToList();
        hdd = solver.Defrag(hdd);

        Assert.Equal(expected, hdd);
    }

    [Fact]
    public void CalcChecksum_WithSampleData_ShouldReturn1928()
    {
        Solver solver = new();
        List<long> hdd = "0099811188827773336446555566.............."
            .Select(c => c == '.' ? -1 : (long)char.GetNumericValue(c))
            .ToList();
        long checksum = solver.CalcChecksum(hdd);

        Assert.Equal(1928, checksum);
    }

    [Fact]
    public void CalcChecksum_WithSampleDataFromPart2_ShouldReturn2858()
    {
        Solver solver = new();
        List<long> hdd = "00992111777.44.333....5555.6666.....8888.."
            .Select(c => c == '.' ? -1 : (long)char.GetNumericValue(c))
            .ToList();
        long checksum = solver.CalcChecksum(hdd);

        Assert.Equal(2858, checksum);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe1928()
    {
        long checksum = 0;
        Solver solver = new();

        checksum = long.Parse(solver.SolvePart1(data));

        Assert.Equal(1928, checksum);
    }

    [Fact]
    public void DefragContiguous_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        List<long> hdd = "00...111...2...333.44.5555.6666.777.888899"
            .Select(c => c == '.' ? -1 : (long)char.GetNumericValue(c))
            .ToList();
        List<long> expected = "00992111777.44.333....5555.6666.....8888.."
            .Select(c => c == '.' ? -1 : (long)char.GetNumericValue(c))
            .ToList();
        hdd = solver.DefragContiguous(hdd);

        Assert.Equal(expected, hdd);
    }

    [Fact]
    public void DefragContiguous_WithEdgeCase_ShouldWork()
    {
        Solver solver = new();
        List<long> hdd = "00...1112......333.44.5555.6666.777.888899."
            .Select(c => c == '.' ? -1 : (long)char.GetNumericValue(c))
            .ToList();
        List<long> expected = "00992111.888844333....5555.6666.777........"
            .Select(c => c == '.' ? -1 : (long)char.GetNumericValue(c))
            .ToList();
        hdd = solver.DefragContiguous(hdd);

        Assert.Equal(expected, hdd);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe2858()
    {
        long checksum = 0;
        Solver solver = new();

        checksum = long.Parse(solver.SolvePart2(data));

        Assert.Equal(2858, checksum);
    }

    private string[] data;
    private string sampleInput = """
2333133121414131402
""";
}
