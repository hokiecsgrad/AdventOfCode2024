using AdventOfCode.Common;

namespace AdventOfCode.Day02;

public class Solver
{
    public void SolvePart1(string[] data)
    {
        int numSafe = 0;
        foreach (string rowData in data)
        {
            List<int> row = rowData
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToList();

            if (IsSafe(row))
                numSafe++;
        }

        Console.WriteLine($"Part 1: {numSafe}");
    }

    public bool IsSafe(List<int> row)
    {
        HashSet<int> set = new();
        for (int i = 1; i < row.Count(); i++)
            set.Add(row[i] - row[i - 1]);

        if (set.Except(new HashSet<int> { 1, 2, 3 }).Count() == 0 ||
            set.Except(new HashSet<int> { -1, -2, -3 }).Count() == 0)
            return true;
        else
            return false;
    }

    public void SolvePart2(string[] data)
    {
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

                if (IsSafe(opRow))
                {
                    numSafe++;
                    break;
                }
            }
        }

        Console.WriteLine($"Part 2: {numSafe}");
    }
}