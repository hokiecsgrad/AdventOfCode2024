using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Day24;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        long total = 0;

        Dictionary<string, int> wires = GetAllWires(data);
        SetInitialValues(ref wires, data);

        int start = 0;
        while (data[start] != "") start++;
        start++;

        while (wires.Where(w => w.Key.StartsWith("z")).Any(w => w.Value == -1))
            for (int i = start; i < data.Length; i++)
                CalculateResult(data[i], ref wires);

        total = CalculateValueFromBinary(wires);

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        return String.Empty;
    }

    public long CalculateValueFromBinary(Dictionary<string, int> wires)
    {
        List<string> binary = wires.Select(w => w.Key).Where(w => w.StartsWith("z")).OrderByDescending(w => w).ToList();
        StringBuilder binVal = new();
        foreach (string register in binary)
            binVal.Append(wires[register].ToString());

        return Convert.ToInt64(binVal.ToString(), 2);
    }

    public void CalculateResult(string operation, ref Dictionary<string, int> wires)
    {
        (string, string, string, string) values = BreakoutOperation(operation);
        string left = values.Item1;
        string op = values.Item2;
        string right = values.Item3;
        string result = values.Item4;

        if (wires[left] == -1 || wires[right] == -1) return;

        wires[result] = op switch
        {
            "AND" => wires[left] & wires[right],
            "OR" => wires[left] | wires[right],
            "XOR" => wires[left] ^ wires[right],
            _ => throw new InvalidOperationException("Unknown operation")
        };
    }

    public void SetInitialValues(ref Dictionary<string, int> wires, string[] data)
    {
        Regex initVal = new Regex(@"^([\w]{3}): ([10])$");

        int i = 0;
        while (data[i] != "")
        {
            var matches = initVal.Match(data[i]);
            wires[matches.Groups[1].Value] =
                int.Parse(matches.Groups[2].Value);
            i++;
        }
    }

    public Dictionary<string, int> GetAllWires(string[] data)
    {
        Dictionary<string, int> wires = new();

        int i = 0;
        while (data[i] != "") i++;
        i++;

        for (; i < data.Length; i++)
        {
            (string, string, string, string) values = BreakoutOperation(data[i]);

            if (!wires.ContainsKey(values.Item1))
                wires.Add(values.Item1, -1);
            if (!wires.ContainsKey(values.Item3))
                wires.Add(values.Item3, -1);
            if (!wires.ContainsKey(values.Item4))
                wires.Add(values.Item4, -1);
        }

        return wires;
    }

    private (string, string, string, string) BreakoutOperation(string op)
    {
        Regex pattern = new Regex(@"^([\w]{3}) (OR|AND|XOR) ([\w]{3}) -> ([\w]{3})$");
        var matches = pattern.Match(op);
        string first = matches.Groups[1].Value;
        string oper = matches.Groups[2].Value;
        string second = matches.Groups[3].Value;
        string third = matches.Groups[4].Value;

        return (first, oper, second, third);
    }
}