using AdventOfCode.Common;

namespace AdventOfCode.Day25;

public class Program
{
    public static void Main(string[] args)
    {
        InputGetter input = new InputGetter("input.txt");

        ProgramFramework framework = new ProgramFramework();
        framework.InputHandler = input.GetStringsFromInput;
        framework.Part1Handler = new Solver().SolvePart1;
        framework.Part2Handler = new Solver().SolvePart2;
        framework.RunProgram();
    }
}