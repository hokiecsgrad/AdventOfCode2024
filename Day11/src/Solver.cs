using AdventOfCode.Common;

namespace AdventOfCode.Day11;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        long[] temp = ParseDataIntoArray(data[0]);

        Dictionary<long, long> stones = new();
        foreach (long num in temp) stones.Add(num, 1);

        SimulateBlinks(ref stones, 25);

        long total = 0;
        foreach (var item in stones)
            total += item.Value;

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        long[] temp = ParseDataIntoArray(data[0]);

        Dictionary<long, long> stones = new();
        foreach (long num in temp) stones.Add(num, 1);

        SimulateBlinks(ref stones, 75);

        long total = 0;
        foreach (var item in stones)
            total += item.Value;

        return total.ToString();
    }

    public void SimulateBlinks(ref Dictionary<long, long> stones, int numBlinks)
    {
        for (int i = 0; i < numBlinks; i++)
            Blink(ref stones);
    }

    public void Blink(ref Dictionary<long, long> stones)
    {
        Dictionary<long, long> currIter = new();
        foreach (KeyValuePair<long, long> currItem in stones)
        {
            if (currItem.Value == 0) continue;

            string number = currItem.Key.ToString();
            if (currItem.Key == 0)
            {
                AddValueToCache(ref currIter, 1, currItem.Value);
                stones[currItem.Key] = 0;
            }
            else if (number.Length % 2 == 0)
            {
                long newValLeft = long.Parse(number.Substring(0, number.Length / 2));
                AddValueToCache(ref currIter, newValLeft, currItem.Value);
                long newValRight = long.Parse(number.Substring(number.Length / 2));
                AddValueToCache(ref currIter, newValRight, currItem.Value);
                stones[currItem.Key] = 0;
            }
            else
            {
                long newVal = currItem.Key * 2024;
                AddValueToCache(ref currIter, newVal, currItem.Value);
                stones[currItem.Key] = 0;
            }
        }

        foreach (var item in currIter)
            if (!stones.ContainsKey(item.Key))
                stones.Add(item.Key, item.Value);
            else
                stones[item.Key] += item.Value;
    }

    private void AddValueToCache(ref Dictionary<long, long> cache, long key, long val)
    {
        if (!cache.ContainsKey(key))
            cache.Add(key, val);
        else
            cache[key] += val;

    }

    public long[] ParseDataIntoArray(string data)
    {
        return data.Split(
            ' ',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            )
            .Select(long.Parse)
            .ToArray<long>();
    }
}