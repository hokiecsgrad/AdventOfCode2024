using AdventOfCode.Common;

namespace AdventOfCode.Day25;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        int total = 0;

        (List<int[]> locks, List<int[]> keys) lockAndKey = ParseLocksAndKeys(data);
        List<int[]> locks = lockAndKey.locks
            .OrderBy(l => l[0])
            .ThenBy(l => l[1])
            .ThenBy(l => l[2])
            .ThenBy(l => l[3])
            .ThenBy(l => l[4])
            .ToList();
        List<int[]> keys = lockAndKey.keys
            .OrderByDescending(k => k[0])
            .ThenByDescending(k => k[1])
            .ThenByDescending(k => k[2])
            .ThenByDescending(k => k[3])
            .ThenByDescending(k => k[4])
            .ToList();

        total = CountKeyLockCombos(keys, locks);

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        return String.Empty;
    }

    public int CountKeyLockCombos(List<int[]> keys, List<int[]> locks)
    {
        int total = 0;

        foreach (int[] currLock in locks)
        {
            foreach (int[] currKey in keys)
            {
                if (WillKeyFitInLock(currKey, currLock))
                    total++;
            }
        }

        return total;
    }

    public bool WillKeyFitInLock(int[] currKey, int[] currLock)
        => currKey.Zip(currLock, (k, l) => k + l).All(c => c <= 5);

    public (List<int[]>, List<int[]>) ParseLocksAndKeys(string[] data)
    {
        List<int[]> locks = [];
        List<int[]> keys = [];

        int i = 0;
        while (i < data.Length)
        {
            bool isLock = false;
            if (data[i][0] == '#') isLock = true;
            if (isLock)
            {
                int[] currLock = [0, 0, 0, 0, 0];
                int maxRow = i + 7;
                for (i += 1; i < maxRow; i++)
                    for (int j = 0; j < 5; j++)
                        if (data[i][j] == '#') currLock[j]++;
                locks.Add(currLock);
            }
            else
            {
                int[] currKey = [5, 5, 5, 5, 5];
                int maxRow = i + 7;
                for (i += 1; i < maxRow; i++)
                    for (int j = 0; j < 5; j++)
                        if (data[i][j] == '.') currKey[j]--;
                keys.Add(currKey);
            }
            i += 1;
        }

        return (locks, keys);
    }
}