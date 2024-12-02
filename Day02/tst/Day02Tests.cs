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
        int numSafe = 0;
        foreach (string rowData in data)
        {
            bool stopProcessing = false;

            List<int> row = rowData
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            bool isAsc = false;
            bool isDes = false;
            for (int i = 1; i < row.Count(); i++)
            {
                if (Math.Abs(row[i] - row[i - 1]) > 3) stopProcessing = true;
                if (row[i] == row[i - 1]) stopProcessing = true;
                if (row[i] > row[i - 1]) isAsc = true;
                if (row[i] < row[i - 1]) isDes = true;
                if (isAsc && isDes) stopProcessing = true;
            }
            if (stopProcessing) continue;

            numSafe++;
        }

        Assert.Equal(2, numSafe);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe4()
    {
        int numSafe = 0;
        foreach (string rowData in data)
        {
            List<int> row = rowData
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            bool rowIsSafe = false;
            for (int level = -1; level < row.Count() && !rowIsSafe; level++)
            {
                bool stopProcessing = false;

                List<int> opRow = rowData
                    .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();

                if (level >= 0) opRow.RemoveAt(level);

                bool isAsc = false;
                bool isDes = false;
                for (int i = 1; i < opRow.Count(); i++)
                {
                    if (Math.Abs(opRow[i] - opRow[i - 1]) > 3) stopProcessing = true;
                    if (opRow[i] == opRow[i - 1]) stopProcessing = true;
                    if (opRow[i] > opRow[i - 1]) isAsc = true;
                    if (opRow[i] < opRow[i - 1]) isDes = true;
                    if (isAsc && isDes) stopProcessing = true;
                }
                if (stopProcessing) continue;

                rowIsSafe = true;
                numSafe++;
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
