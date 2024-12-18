using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Day17;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        return String.Empty;
    }

    public string SolvePart2(string[] data)
    {
        return String.Empty;
    }

    public Dictionary<char, long> ParseRegisters(string[] data)
    {
        Dictionary<char, long> registers = new();

        int i = 0;
        while (data[i] != string.Empty)
        {
            char currReg = data[i].Substring(0, 10)[^1];
            registers[currReg] = long.Parse(data[i].Substring(data[i].IndexOf(':') + 2));
            i++;
        }

        return registers;
    }

    public List<int> ParseProgram(string[] data)
    {
        List<int> program = new();

        int i = 0;
        while (data[i] != string.Empty)
            i++;
        i++;

        program = data[i].Substring(data[i].IndexOf(' ') + 1).Split(',').Select(int.Parse).ToList();

        return program;
    }
}