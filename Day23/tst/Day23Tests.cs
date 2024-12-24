using AdventOfCode.Common;
using AdventOfCode.Day23;
using Xunit;

namespace AdventOfCode.Day23.Tests;

public class Day23Tests
{
    public Day23Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void CreateSetOfComputers_WithSampleData_ShouldWork()
    {
        Solver solver = new();

        (List<string> computers, Dictionary<string, HashSet<string>> pairs) parsed =
            solver.ParseData(data);
        List<string> comps = parsed.computers;
        Dictionary<string, HashSet<string>> pairs = parsed.pairs;

        Assert.Equal(16, comps.Count);
        Assert.Equal(16, pairs.Count);
    }

    [Fact]
    public void FindTriples_WithSampleData_ShouldFind12()
    {
        Solver solver = new();

        (List<string> computers, Dictionary<string, HashSet<string>> pairs) parsed =
            solver.ParseData(data);
        List<string> comps = parsed.computers;
        Dictionary<string, HashSet<string>> pairs = parsed.pairs;

        List<List<string>> trips = solver.GetTrips(comps, pairs);

        Assert.Equal(12, trips.Count);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe7()
    {
        Solver solver = new();

        long total = long.Parse(solver.SolvePart1(data));

        Assert.Equal(7, total);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBeXXX()
    {
        throw new NotImplementedException();
    }

    private string[] data;
    private string sampleInput = """
kh-tc
qp-kh
de-cg
ka-co
yn-aq
qp-ub
cg-tb
vc-aq
tb-ka
wh-tc
yn-cg
kh-ub
ta-co
de-co
tc-td
tb-wq
wh-td
ta-ka
td-qp
aq-cg
wq-ub
ub-vc
de-ta
wq-aq
wq-vc
wh-yn
ka-de
kh-ta
co-tc
wh-qp
tb-vc
td-yn
""";
}
