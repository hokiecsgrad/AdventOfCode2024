using AdventOfCode.Common;

namespace AdventOfCode.Day05;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        int total = 0;

        (Dictionary<int, HashSet<int>> before, Dictionary<int, HashSet<int>> after)
            rules = GetRules(data);
        List<List<int>> pageNums = GetPageNums(data);

        (List<int> validUpdates, List<int> invalidUpdates) =
            GetIndexesOfUpdates(
                rules.before,
                rules.after,
                pageNums);

        foreach (int index in validUpdates)
        {
            int len = pageNums[index].Count();
            total += pageNums[index][len / 2];
        }

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        int total = 0;

        (Dictionary<int, HashSet<int>> before, Dictionary<int, HashSet<int>> after)
            rules = GetRules(data);
        List<List<int>> pageNums = GetPageNums(data);

        (List<int> validUpdates, List<int> invalidUpdates) =
            GetIndexesOfUpdates(
                rules.before,
                rules.after,
                pageNums);

        foreach (int index in invalidUpdates)
            pageNums[index] = FixPageUpdates(pageNums[index], rules.before, rules.after);

        foreach (int index in invalidUpdates)
        {
            int len = pageNums[index].Count();
            total += pageNums[index][len / 2];
        }

        return total.ToString();
    }

    public (Dictionary<int, HashSet<int>>, Dictionary<int, HashSet<int>>) GetRules(string[] data)
    {
        Dictionary<int, HashSet<int>> beforeRules = new();
        Dictionary<int, HashSet<int>> afterRules = new();

        int index = 0;
        while (data[index] != "")
        {
            int[] currRule = data[index].Split('|').Select(int.Parse).ToArray();
            if (afterRules.ContainsKey(currRule[0]))
                afterRules[currRule[0]].Add(currRule[1]);
            else
                afterRules.Add(currRule[0], new HashSet<int> { currRule[1] });

            if (beforeRules.ContainsKey(currRule[1]))
                beforeRules[currRule[1]].Add(currRule[0]);
            else
                beforeRules.Add(currRule[1], new HashSet<int> { currRule[0] });

            index++;
        }

        return (beforeRules, afterRules);
    }

    public List<List<int>> GetPageNums(string[] data)
    {
        List<List<int>> pageNums = new();

        int index = 0;
        while (data[index] != "")
            index++;

        for (index++; index < data.Length; index++)
        {
            List<int> currUpdate = data[index].Split(',').Select(int.Parse).ToList();
            pageNums.Add(currUpdate);
        }

        return pageNums;
    }

    public (List<int>, List<int>) GetIndexesOfUpdates(
        Dictionary<int, HashSet<int>> beforeRules,
        Dictionary<int, HashSet<int>> afterRules,
        List<List<int>> pageNums)
    {
        List<int> validUpdates = new();
        List<int> invalidUpdates = new();

        for (int i = 0; i < pageNums.Count(); i++)
            if (GetInvalidNumAndRule(pageNums[i], beforeRules, afterRules) is null)
                validUpdates.Add(i);
            else
                invalidUpdates.Add(i);

        return (validUpdates, invalidUpdates);
    }

    public (int, HashSet<int>, HashSet<int>)? GetInvalidNumAndRule(
        List<int> pageNums,
        Dictionary<int, HashSet<int>> beforeRules,
        Dictionary<int, HashSet<int>> afterRules)
    {
        for (int j = 0; j < pageNums.Count(); j++)
        {
            int currNum = pageNums[j];
            // put all nums before current num in a "before set"
            HashSet<int> befores = new HashSet<int>(pageNums.Take(j));
            // put all nums after current num in an "after set"
            HashSet<int> afters = new HashSet<int>(pageNums.Skip(j + 1));
            // make sure curr num after set doesn't contain anything in the before set
            if (beforeRules.ContainsKey(currNum) && afters.Overlaps(beforeRules[currNum]))
            {
                return (currNum, beforeRules[currNum], new HashSet<int>());
            }
            // Make sure curr num before set doesn't contain anything in the after set
            if (afterRules.ContainsKey(currNum) && befores.Overlaps(afterRules[currNum]))
            {
                return (currNum, new HashSet<int>(), afterRules[currNum]);
            }
        }
        return null;
    }

    public List<int> FixPageUpdates(
        List<int> updates,
        Dictionary<int, HashSet<int>> beforeRules,
        Dictionary<int, HashSet<int>> afterRules)
    {
        List<int> fixedUpdates = updates;

        (int num, HashSet<int> beforeRule, HashSet<int> afterRule)? update =
            GetInvalidNumAndRule(updates, beforeRules, afterRules);

        while (update is not null)
        {
            int indexOfNum = updates.IndexOf(update.Value.num);
            int indexOfBadValue = -1;
            int badValue = -1;

            if (update is not null && update.Value.beforeRule.Count > 0)
            {
                HashSet<int> afters = new HashSet<int>(fixedUpdates.Skip(indexOfNum + 1));
                badValue = afters.Intersect(update.Value.beforeRule).ToList().First();
                indexOfBadValue = fixedUpdates.IndexOf(badValue);
            }
            else if (update is not null && update.Value.afterRule.Count > 0)
            {
                HashSet<int> befores = new HashSet<int>(fixedUpdates.Take(indexOfNum - 1));
                badValue = befores.Intersect(update.Value.afterRule).ToList().First();
                indexOfBadValue = fixedUpdates.IndexOf(badValue);
            }

            fixedUpdates[indexOfNum] = badValue;
            fixedUpdates[indexOfBadValue] = update.Value.num;

            update = GetInvalidNumAndRule(fixedUpdates, beforeRules, afterRules);
        }

        return fixedUpdates;
    }
}