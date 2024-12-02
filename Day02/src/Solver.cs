using AdventOfCode.Common;

namespace AdventOfCode.Day02;

public class Solver
{
    public void SolvePart1(string[] data)
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

        Console.WriteLine($"Part 1: {numSafe}");
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

        Console.WriteLine($"Part 2: {numSafe}");
    }
}