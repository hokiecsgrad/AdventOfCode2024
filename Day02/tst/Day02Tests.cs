using AdventOfCode.Common;
using AdventOfCode.Day02;
using Xunit;

namespace AdventOfCode.Day02.Tests;

public class Day02Tests
{
    public Day02Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe2()
    {
        Solver solver = new();
        int numSafe = 0;

        foreach (string rowData in data)
        {
            List<int> row = rowData
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            if (solver.IsSafe(row))
                numSafe++;
        }

        Assert.Equal(2, numSafe);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe4()
    {
        Solver solver = new();
        int numSafe = 0;

        foreach (string rowData in data)
        {
            List<int> row = rowData
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            for (int level = -1; level < row.Count(); level++)
            {
                List<int> opRow = rowData
                    .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

                if (level >= 0) opRow.RemoveAt(level);

                if (solver.IsSafe(opRow))
                {
                    numSafe++;
                    break;
                }
            }
        }

        Assert.Equal(4, numSafe);
    }

    private string[] data;
    private string sampleInput = """
7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9
""";
}