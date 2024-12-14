using System.Text.RegularExpressions;
using AdventOfCode.Common;
using AdventOfCode.Day13;
using Xunit;

namespace AdventOfCode.Day13.Tests;

public class Day13Tests
{
    public Day13Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries
            );
    }

    [Fact]
    public void MatrixSolution_WithSampleData_ShouldBe280()
    {
        Solver solver = new();
        long[] a = new long[2] { 94, 34 };
        long[] b = new long[2] { 22, 67 };
        long[] prize = new long[2] { 8400, 5400 };
        (double a, double b) buttonPresses = solver.SolveMatrix(a, b, prize);

        double total = buttonPresses.a * 3 + buttonPresses.b;

        Assert.Equal(80.0, buttonPresses.a, 1);
        Assert.Equal(40.0, buttonPresses.b, 1);
        Assert.Equal(280.0, total, 1);
    }

    [Fact]
    public void MatrixSolution_WithBadData_ShouldNotWork()
    {
        double total = 0.0;
        Solver solver = new();

        long[] a = new long[2] { 26, 66 };
        long[] b = new long[2] { 67, 21 };
        long[] prize = new long[2] { 12748, 12176 };

        (double a, double b) buttonPresses = solver.SolveMatrix(a, b, prize);

        bool hasSolution = false;
        if (Math.Abs(buttonPresses.a % 1) <= (Double.Epsilon * 100) &&
            Math.Abs(buttonPresses.b % 1) <= (Double.Epsilon * 100))
        {
            hasSolution = true;
            total = buttonPresses.a * 3 + buttonPresses.b;
        }

        Assert.False(hasSolution);
        Assert.Equal(0.0, total, 1);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe480()
    {
        Solver solver = new();

        double total = double.Parse(solver.SolvePart1(data));

        Assert.Equal(480, total, 1);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe875318608908()
    {
        Solver solver = new();

        double total = double.Parse(solver.SolvePart2(data));

        Assert.Equal(875318608908, total, 1);
    }

    private string[] data;
    private string sampleInput = """
Button A: X+94, Y+34
Button B: X+22, Y+67
Prize: X=8400, Y=5400

Button A: X+26, Y+66
Button B: X+67, Y+21
Prize: X=12748, Y=12176

Button A: X+17, Y+86
Button B: X+84, Y+37
Prize: X=7870, Y=6450

Button A: X+69, Y+23
Button B: X+27, Y+71
Prize: X=18641, Y=10279
""";
}
