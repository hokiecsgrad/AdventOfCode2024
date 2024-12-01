using System;
using System.Linq;
using AdventOfCode.Common;

namespace AdventOfCode.Day01;

public class Solver
{
    public void SolvePart1(string[] data)
    {
        var lists = SplitArrayIntoTwo(data);
        int sum = OrderAndSumTheDiffs(lists.Item1, lists.Item2);

        Console.WriteLine($"Part 1: {sum}");
    }

    public (List<int>, List<int>) SplitArrayIntoTwo(string[] data)
    {
        List<int> firstNums = data
            .Select(
                x => int.Parse(
                    x.Split(' ', StringSplitOptions.TrimEntries |
                        StringSplitOptions.RemoveEmptyEntries)[0]))
            .ToList();

        List<int> secondNums = data
            .Select(
                x => int.Parse(
                    x.Split(' ', StringSplitOptions.TrimEntries |
                        StringSplitOptions.RemoveEmptyEntries)[1]))
            .ToList();

        return (firstNums, secondNums);
    }

    public int OrderAndSumTheDiffs(List<int> one, List<int> two)
    {
        List<int> first = one.OrderBy(x => x).ToList();
        List<int> second = two.OrderBy(x => x).ToList();

        List<(int, int)> diffs = first
            .Zip(second, (first, second) => (first, second))
            .ToList();

        int sum = diffs
            .Select(x => Math.Abs(x.Item1 - x.Item2))
            .Sum();

        return sum;
    }

    public void SolvePart2(string[] data)
    {
        var lists = SplitArrayIntoTwo(data);
        int sum = SumSimilarityScores(lists.Item1, lists.Item2);

        Console.WriteLine($"Part 2: {sum}");
    }

    public int SumSimilarityScores(List<int> one, List<int> two)
    {
        int sumOfSims = one
            .Select(x => x * two.Count(y => y == x))
            .Sum();

        return sumOfSims;
    }
}