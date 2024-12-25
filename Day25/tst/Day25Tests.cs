using AdventOfCode.Common;
using AdventOfCode.Day25;
using Xunit;

namespace AdventOfCode.Day25.Tests;

public class Day25Tests
{
    public Day25Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries
            );
    }

    [Fact]
    public void ParseLockAndKey_WithSampleData_ShouldWork()
    {
        Solver solver = new();

        (List<int[]> locks, List<int[]> keys) lockAndKey = solver.ParseLocksAndKeys(data);

        Assert.Equal(2, lockAndKey.locks.Count);
        Assert.Equal(3, lockAndKey.keys.Count);
    }

    [Fact]
    public void WillKeyFitInLock_WithSampleDataZeroAndZero_ShouldBeFalse()
    {
        Solver solver = new();

        (List<int[]> locks, List<int[]> keys) lockAndKey = solver.ParseLocksAndKeys(data);
        bool willFit = solver.WillKeyFitInLock(lockAndKey.keys[0], lockAndKey.locks[0]);

        Assert.False(willFit);
    }

    [Fact]
    public void WillKeyFitInLock_WithSampleDataTwoAndZero_ShouldBeTrue()
    {
        Solver solver = new();

        (List<int[]> locks, List<int[]> keys) lockAndKey = solver.ParseLocksAndKeys(data);
        bool willFit = solver.WillKeyFitInLock(lockAndKey.keys[2], lockAndKey.locks[0]);

        Assert.True(willFit);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe3()
    {
        Solver solver = new();

        int total = int.Parse(solver.SolvePart1(data));

        Assert.Equal(3, total);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBeXXX()
    {
        throw new NotImplementedException();
    }

    private string[] data;
    private string sampleInput = """
#####
.####
.####
.####
.#.#.
.#...
.....

#####
##.##
.#.##
...##
...#.
...#.
.....

.....
#....
#....
#...#
#.#.#
#.###
#####

.....
.....
#.#..
###..
###.#
###.#
#####

.....
.....
.....
#....
#.#..
#.#.#
#####
""";
}
