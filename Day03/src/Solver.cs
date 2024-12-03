using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Day03;

public class Solver
{
    public void SolvePart1(string[] data)
    {
        int total = 0;

        foreach (string line in data)
            total += SumMulOpsInLine(line);

        Console.WriteLine($"Part 1: {total}");
    }

    public int SumMulOpsInLine(string line)
    {
        Regex regex = new Regex(@"(mul\(([\d]+),([\d]+)\))");
        var matches = regex.Matches(line);

        int total = 0;
        foreach (Match match in matches)
            total += int.Parse(match.Groups[2].Value) *
                        int.Parse(match.Groups[3].Value);

        return total;
    }

    public void SolvePart2(string[] data)
    {
        Regex condPattern = new Regex(@"don't\(\).*?do\(\)");

        int total = 0;

        string line = String.Join("", data);
        line = condPattern.Replace(line, "");

        total += SumMulOpsInLine(line);

        Console.WriteLine($"Part 2: {total}");
    }
}