using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCode.Common;

namespace AdventOfCode.Day13;

public class Solver
{
    public string SolvePart1(string[] data)
    {
        long total = 0;

        long[] a = new long[2];
        long[] b = new long[2];
        long[] prize = new long[2];

        (double a, double b) buttonPresses;
        bool aValid;
        bool bValid;

        foreach (string line in data)
        {
            if (line == string.Empty)
            {
                buttonPresses = SolveMatrix(a, b, prize);

                aValid = buttonPresses.a >= 0 &&
                    Math.Abs(Math.Round(buttonPresses.a) - buttonPresses.a) <= 0.000001;
                bValid = buttonPresses.b >= 0 &&
                    Math.Abs(Math.Round(buttonPresses.b) - buttonPresses.b) <= 0.000001;

                if (aValid && bValid)
                    total += ((long)Math.Round(buttonPresses.a) * 3) + (long)Math.Round(buttonPresses.b);

                a = new long[2];
                b = new long[2];
                prize = new long[2];
            }
            else
            {
                (string, long[]) value = ParseInputLine(line);
                if (value.Item1 == "a") a = value.Item2;
                if (value.Item1 == "b") b = value.Item2;
                if (value.Item1 == "prize") prize = value.Item2;
            }
        }

        buttonPresses = SolveMatrix(a, b, prize);

        aValid = buttonPresses.a >= 0 &&
            Math.Abs(Math.Round(buttonPresses.a) - buttonPresses.a) <= 0.000001;
        bValid = buttonPresses.b >= 0 &&
            Math.Abs(Math.Round(buttonPresses.b) - buttonPresses.b) <= 0.000001;

        if (aValid && bValid)
            total += ((long)Math.Round(buttonPresses.a) * 3) + (long)Math.Round(buttonPresses.b);

        return total.ToString();
    }

    public string SolvePart2(string[] data)
    {
        long total = 0;

        long[] a = new long[2];
        long[] b = new long[2];
        long[] prize = new long[2];

        (double a, double b) buttonPresses;
        bool aValid;
        bool bValid;

        foreach (string line in data)
        {
            if (line == string.Empty)
            {
                prize[0] += 10_000_000_000_000;
                prize[1] += 10_000_000_000_000;
                buttonPresses = SolveMatrix(a, b, prize);

                aValid = buttonPresses.a >= 0 &&
                    Math.Abs(Math.Round(buttonPresses.a) - buttonPresses.a) <= 0.01;
                bValid = buttonPresses.b >= 0 &&
                    Math.Abs(Math.Round(buttonPresses.b) - buttonPresses.b) <= 0.01;

                if (aValid && bValid)
                    total += ((long)Math.Round(buttonPresses.a) * 3) + (long)Math.Round(buttonPresses.b);

                a = new long[2];
                b = new long[2];
                prize = new long[2];
            }
            else
            {
                (string, long[]) value = ParseInputLine(line);
                if (value.Item1 == "a") a = value.Item2;
                if (value.Item1 == "b") b = value.Item2;
                if (value.Item1 == "prize") prize = value.Item2;
            }
        }

        prize[0] += 10_000_000_000_000;
        prize[1] += 10_000_000_000_000;
        buttonPresses = SolveMatrix(a, b, prize);

        aValid = buttonPresses.a >= 0 &&
            Math.Abs(Math.Round(buttonPresses.a) - buttonPresses.a) <= 0.01;
        bValid = buttonPresses.b >= 0 &&
            Math.Abs(Math.Round(buttonPresses.b) - buttonPresses.b) <= 0.01;

        if (aValid && bValid)
            total += ((long)Math.Round(buttonPresses.a) * 3) + (long)Math.Round(buttonPresses.b);

        return total.ToString();
    }

    public (double, double) SolveMatrix(long[] a, long[] b, long[] prize)
    {
        double[,] matrix = new double[2, 3] {
            { a[0], b[0], prize[0] },
            { a[1], b[1], prize[1] }
        };
        // get 1 in matrix[0,0]
        matrix[0, 1] = matrix[0, 1] / matrix[0, 0];
        matrix[0, 2] = matrix[0, 2] / matrix[0, 0];
        matrix[0, 0] = matrix[0, 0] / matrix[0, 0];
        // get 0 in matrix[1,0]
        matrix[1, 1] = -matrix[1, 0] * matrix[0, 1] + matrix[1, 1];
        matrix[1, 2] = -matrix[1, 0] * matrix[0, 2] + matrix[1, 2];
        matrix[1, 0] = -matrix[1, 0] * matrix[0, 0] + matrix[1, 0];
        // get 1 in matrix[1,1]
        matrix[1, 0] = matrix[1, 0] / matrix[1, 1];
        matrix[1, 2] = matrix[1, 2] / matrix[1, 1];
        matrix[1, 1] = matrix[1, 1] / matrix[1, 1];
        // get 0 in matrix[0,1] 
        matrix[0, 0] = -matrix[0, 1] * matrix[1, 0] + matrix[0, 0];
        matrix[0, 2] = -matrix[0, 1] * matrix[1, 2] + matrix[0, 2];
        matrix[0, 1] = -matrix[0, 1] * matrix[1, 1] + matrix[0, 1];

        return (matrix[0, 2], matrix[1, 2]);
    }

    public (string, long[]) ParseInputLine(string line)
    {
        string value = string.Empty;
        long[] temp = new long[2];

        Regex xpat = new Regex(@"X\+([\d]+),");
        Regex ypat = new Regex(@"Y\+([\d]+)$");
        Regex xpatPrize = new Regex(@"X=([\d]+),");
        Regex ypatPrize = new Regex(@"Y=([\d]+)$");

        if (line.Substring(0, 8) == "Button A")
        {
            value = "a";
            temp[0] = long.Parse(xpat.Match(line).Groups[1].Value);
            temp[1] = long.Parse(ypat.Match(line).Groups[1].Value);
        }
        else if (line.Substring(0, 8) == "Button B")
        {
            value = "b";
            temp[0] = long.Parse(xpat.Match(line).Groups[1].Value);
            temp[1] = long.Parse(ypat.Match(line).Groups[1].Value);
        }
        else if (line.Substring(0, 5) == "Prize")
        {
            value = "prize";
            temp[0] = long.Parse(xpatPrize.Match(line).Groups[1].Value);
            temp[1] = long.Parse(ypatPrize.Match(line).Groups[1].Value);
        }

        return (value, temp);
    }
}