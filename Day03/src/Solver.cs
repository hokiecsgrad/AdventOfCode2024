using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Day03;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        string line = String.Join("", data);
        return SumMulOpsInLine(line).ToString();
    }

    public string SolvePart2(string[] data)
    {
        string line = String.Join("", data);

        Regex condPattern = new Regex(@"don't\(\).*?do\(\)");
        line = condPattern.Replace(line, "");

        return SumMulOpsInLine(line).ToString();
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
}