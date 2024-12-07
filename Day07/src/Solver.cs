using AdventOfCode.Common;

namespace AdventOfCode.Day07;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        long total = 0;
        List<(long, List<int>)> input = ParseData(data);

        foreach ((long target, List<int> nums) line in input)
        {
            Tree numTree = BuildTree(line.nums, new List<char> { '+', '*' });
            total += EvalTreeForTarget(numTree, line.target);
        }

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        long total = 0;
        List<(long, List<int>)> input = ParseData(data);

        foreach ((long target, List<int> nums) line in input)
        {
            Tree numTree = BuildTree(line.nums, new List<char> { '+', '*', '|' });
            total += EvalTreeForTarget(numTree, line.target);
        }

        return total.ToString();
    }

    public Tree BuildTree(List<int> nums, List<char> ops)
    {
        Tree opsTree = new();

        List<Node> currNodes = new();
        currNodes.Add(new Node() { Value = nums[0] });
        for (int i = 0; i < nums.Count() - 1; i++)
        {
            List<Node> processedNodes = new();
            for (int nIndex = 0; nIndex < currNodes.Count(); nIndex++)
            {
                Node currNode = currNodes[nIndex];
                foreach (char op in ops)
                {
                    Edge edge = new Edge()
                    {
                        Op = op,
                        Source = currNode,
                        Dest = new Node() { Value = nums[i + 1] }
                    };
                    currNode.Edges.Add(edge);
                    processedNodes.Add(edge.Dest);
                }
            }
            if (i == 0) opsTree.Root = currNodes[0];
            currNodes = processedNodes;
        }

        return opsTree;
    }

    public long EvalTreeForTarget(Tree tree, long target)
    {
        return EvalNode(tree.Root, target, 0);
    }

    private long EvalNode(Node node, long target, long accumulator)
    {
        if (node.Edges.Count == 0 && accumulator == target) return accumulator;
        if (node.Edges.Count == 0) return 0;

        if (accumulator == 0) accumulator = node.Value;

        foreach (Edge edge in node.Edges)
        {
            long total = 0;
            total = edge.Op switch
            {
                '+' => EvalNode(edge.Dest, target, accumulator + edge.Dest.Value),
                '*' => EvalNode(edge.Dest, target, accumulator * edge.Dest.Value),
                '|' => EvalNode(edge.Dest, target, Concat(accumulator, edge.Dest.Value)),
            };
            if (total == target) return total;
        }
        return 0;
    }

    public long Concat(long x, long y)
    {
        for (var z = y; z > 0; z /= 10)
            x *= 10;
        return x + y;
    }

    public List<(long, List<int>)> ParseData(string[] data)
    {
        List<(long, List<int>)> input = new();

        foreach (string line in data)
        {
            long total = long.Parse(line.Split(':')[0]);
            List<int> nums = line
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Skip(1)
                .Select(int.Parse)
                .ToList();
            input.Add((total, nums));
        }

        return input;
    }
}

public class Tree
{
    public Node? Root { get; set; }

}

public class Node
{
    public int Value { get; set; } = 0;
    public List<Edge> Edges { get; set; } = new();
}

public class Edge
{
    public char Op { get; set; }
    public Node Source { get; set; }
    public Node Dest { get; set; }
}