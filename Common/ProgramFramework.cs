using System;

namespace AdventOfCode.Common;

public delegate string[] InputFunc();
public delegate void SolvePart1Func(string[] data);
public delegate void SolvePart2Func(string[] data);

public class ProgramFramework
{
    public InputFunc InputHandler { get; set; } = null;
    public SolvePart1Func Part1Handler { get; set; } = null;
    public SolvePart2Func Part2Handler { get; set; } = null;

    public void RunProgram()
    {
        var watch = new System.Diagnostics.Stopwatch();
        long totalTime = 0;
        long timeToLoadInput = 0;
        long timeToSolvePart1 = 0;
        long timeToSolvePart2 = 0;

        watch.Start();
        Console.WriteLine("Loading data...");
        string[] data = InputHandler();
        watch.Stop();
        timeToLoadInput = watch.ElapsedMilliseconds;
        Console.WriteLine($"Data loaded in {timeToLoadInput} ms");
        Console.WriteLine();

        if (Part1Handler is not null)
        {
            watch.Start();
            Console.WriteLine("Solving part 1...");
            Part1Handler(data);
            watch.Stop();
            timeToSolvePart1 = watch.ElapsedMilliseconds - timeToLoadInput;
            Console.WriteLine($"Part 1 solved in {timeToSolvePart1} ms.");
            Console.WriteLine();
        }

        if (Part2Handler is not null)
        {
            watch.Start();
            Console.WriteLine("Solving part 2...");
            Part2Handler(data);
            watch.Stop();
            timeToSolvePart2 = watch.ElapsedMilliseconds - timeToSolvePart1 - timeToLoadInput;
            Console.WriteLine($"Part 2 solved in {timeToSolvePart2} ms.");
            Console.WriteLine();
        }

        totalTime = watch.ElapsedMilliseconds;
        Console.WriteLine($"Total execution time: {totalTime} ms.");
    }
}
