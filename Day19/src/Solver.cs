using System.Text;
using AdventOfCode.Common;

namespace AdventOfCode.Day19;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        int total = 0;

        List<string> patterns = ParsePatterns(data);
        List<string> designs = ParseDesigns(data);

        total = CountMatchingDesigns(designs, patterns);

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        long total = 0;

        List<string> patterns = ParsePatterns(data);
        List<string> designs = ParseDesigns(data);

        total = CountAllDesignCombos(designs, patterns);

        return total.ToString();
    }

    public int CountMatchingDesigns(List<string> designs, List<string> patterns)
    {
        int total = 0;

        List<string> sortedPats = patterns.OrderByDescending(x => x.Length).ToList();
        Dictionary<string, long> cache = new();

        foreach (string design in designs)
            if (CountNumDesigns(design, sortedPats, string.Empty, cache) > 0)
                total++;

        return total;
    }

    public long CountAllDesignCombos(List<string> designs, List<string> patterns)
    {
        long total = 0;

        List<string> sortedPats = patterns.OrderBy(x => x.Length).ToList();
        Dictionary<string, long> cache = new();

        foreach (string design in designs)
            total += CountNumDesigns(design, sortedPats, string.Empty, cache);

        return total;
    }

    public long CountNumDesigns(string design, List<string> patterns, string curr, Dictionary<string, long> cache)
    {
        if (curr == design) return 1;
        string remaining = design.Substring(curr.Length);
        if (cache.ContainsKey(remaining)) return cache[remaining];
        if (curr.Length > design.Length) return 0;

        long totalCombos = 0;

        int testIndex = curr.Length;
        for (int i = 0; i < patterns.Count; i++)
        {
            int patLen = patterns[i].Length;
            if (curr.Length + patLen <= design.Length &&
                design.Substring(testIndex, patLen) == patterns[i])
            {
                totalCombos += CountNumDesigns(design, patterns, curr + patterns[i], cache);
                if (cache.ContainsKey(remaining))
                    cache[remaining] = totalCombos;
                else
                    cache.Add(remaining, totalCombos);
            }
        }

        return totalCombos;
    }

    public List<string> ParsePatterns(string[] data)
        => data[0]
            .Split(',', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .ToList();

    public List<string> ParseDesigns(string[] data)
        => data.Skip(2).ToList();
}