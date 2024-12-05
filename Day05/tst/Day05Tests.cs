using AdventOfCode.Common;
using AdventOfCode.Day05;
using Xunit;

namespace AdventOfCode.Day05.Tests;

public class Day05Tests
{
    public Day05Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries
            );
    }

    [Fact]
    public void GetRules_WithSampleData_ShouldReturnDictionariesOfBeforeAndAfterRules()
    {
        Solver solver = new();

        (Dictionary<int, HashSet<int>> before, Dictionary<int, HashSet<int>> after)
            rules = solver.GetRules(data);

        Assert.Equal(rules.before[47], new HashSet<int> { 97, 75 });
        Assert.Equal(rules.after[61], new HashSet<int> { 13, 53, 29 });
    }

    [Fact]
    public void GetPageNums_WithSampleData_ShouldReturn6Updates()
    {
        Solver solver = new();

        List<List<int>> pageNums = solver.GetPageNums(data);

        Assert.Equal(6, pageNums.Count());
        Assert.Equal(3, pageNums[2].Count());
        Assert.Equal(5, pageNums[5].Count());
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe143()
    {
        Solver solver = new();

        int total = int.Parse(solver.SolvePart1(data));

        Assert.Equal(143, total);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe123()
    {
        Solver solver = new();

        int total = int.Parse(solver.SolvePart2(data));

        Assert.Equal(123, total);
    }

    private string[] data;
    private string sampleInput = """
47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47
""";
}
