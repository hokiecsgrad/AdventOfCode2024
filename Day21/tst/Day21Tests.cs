using System.Text;
using AdventOfCode.Common;
using AdventOfCode.Day21;
using Xunit;

namespace AdventOfCode.Day21.Tests;

public class Day21Tests
{
    public Day21Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void SolveCodepad_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        StringBuilder currPath = new();

        char[,] codepad = solver.CreateCodepad();

        string currCode = "029A";
        Point start = solver.GetPointWithValue(codepad, 'A');
        Point goal = new Point();
        for (int i = 0; i < currCode.Length; i++)
        {
            goal = solver.GetPointWithValue(codepad, currCode[i]);

            List<char> path = solver.GetCodepadPath(start, goal);

            foreach (char step in path)
                currPath.Append(step);
            currPath.Append('A');

            start = goal;
        }

        Assert.Equal("<A^A^^>AvvvA", currPath.ToString());
    }

    [Fact]
    public void SolveDirectionPad_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        StringBuilder currPath = new();

        char[,] dirpad = solver.CreateDirectionpad();

        string currCode = "<A^A^^>AvvvA";
        Point start = solver.GetPointWithValue(dirpad, 'A');
        Point goal = new Point();
        for (int i = 0; i < currCode.Length; i++)
        {
            goal = solver.GetPointWithValue(dirpad, currCode[i]);

            List<char> path = solver.GetDirpadPath(start, goal);

            foreach (char step in path)
                currPath.Append(step);
            currPath.Append('A');

            start = goal;
        }

        // # ^ A
        // < v >

        // <    A    ^  A  ^  ^ >   A  v   v v A
        // v<<A >>^A <A >A <A A >vA ^A v<A A A >^A
        // v<<A>>^A<A>A<AA>vA^Av<AAA>^A
        // v<<A>>^A<A>AvA^<AA>Av<AAA>^A - match
        //
        Assert.Equal("v<<A>>^A<A>A<AAv>A^Av<AAA>^A", currPath.ToString());
    }

    [Fact]
    public void SolveDirectionPad2_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        StringBuilder currPath = new();

        char[,] dirpad = solver.CreateDirectionpad();

        string currCode = "v<<A>>^A<A>A<AAv>A^Av<AAA>^A";
        Point start = solver.GetPointWithValue(dirpad, 'A');
        Point goal = new Point();
        for (int i = 0; i < currCode.Length; i++)
        {
            goal = solver.GetPointWithValue(dirpad, currCode[i]);

            List<char> path = solver.GetDirpadPath(start, goal);

            foreach (char step in path)
                currPath.Append(step);
            currPath.Append('A');

            start = goal;
        }

        // # ^ A
        // < v >

        // v   <  < A    >  > ^   A  <    A    >  A  <    A    A v   >  A  ^  A  v   <  A    A A >  ^   A
        // v<A <A A >>^A vA A <^A >A v<<A >>^A vA ^A v<<A >>^A A v<A >A ^A <A >A v<A <A >>^A A A vA <^A >A
        // v<A<AA>>^AvAA<^A>Av<<A>>^AvA^Av<<A>>^AAv<A>A^A<A>Av<A<A>>^AAAvA<^A>A

        Assert.Equal("v<A<AA>>^AvAA<^A>Av<<A>>^AvA^Av<<A>>^AAv<A>A^A<A>Av<A<A>>^AAAvA<^A>A", currPath.ToString());
    }

    [Fact]
    public void SolveCodepad_WithRealInput_ShouldWork()
    {
        Solver solver = new();

        string code = "805A";
        string path = solver.GetPathForCodepad(code);
        Assert.Equal("^^^<AvvvA^^Avv>A", path);
        code = "170A";
        path = solver.GetPathForCodepad(code);
        Assert.Equal("^<<A^^A>vvvA>A", path);
        code = "129A";
        path = solver.GetPathForCodepad(code);
        Assert.Equal("^<<A>A^^>AvvvA", path);
        code = "283A";
        path = solver.GetPathForCodepad(code);
        Assert.Equal("^<A^^Avv>AvA", path);
        code = "540A";
        path = solver.GetPathForCodepad(code);
        Assert.Equal("^^<A<A>vvA>A", path);
    }

    [Fact]
    public void Solve_WithSampleCase980A_ShouldBe58800()
    {
        Solver solver = new();
        int total = 0;
        string code = "980A";

        string path = solver.GetPathForCodepad(code);
        path = solver.GetPathForDirpad(path);
        path = solver.GetPathForDirpad(path);
        total += int.Parse(code.Substring(0, 3)) * path.Length;

        Assert.Equal(58800, total);
    }

    [Fact]
    public void Solve_WithSampleCase179A_ShouldBe12172()
    {
        Solver solver = new();
        int total = 0;
        string code = "179A";

        string path = solver.GetPathForCodepad(code);
        path = solver.GetPathForDirpad(path);
        path = solver.GetPathForDirpad(path);
        total += int.Parse(code.Substring(0, 3)) * path.Length;

        Assert.Equal(12172, total);
    }

    [Fact]
    public void Solve_WithSampleCase456A_ShouldBe29184()
    {
        Solver solver = new();
        int total = 0;
        string code = "456A";

        string path = solver.GetPathForCodepad(code);
        path = solver.GetPathForDirpad(path);
        path = solver.GetPathForDirpad(path);
        total += int.Parse(code.Substring(0, 3)) * path.Length;

        Assert.Equal(29184, total);
    }

    [Fact]
    public void Solve_WithSampleCase379A_ShouldBe24256()
    {
        Solver solver = new();
        int total = 0;
        string code = "379A";

        string path = solver.GetPathForCodepad(code);
        Assert.Equal("^A<<^^A>>AvvvA", path);
        path = solver.GetPathForDirpad(path);
        Assert.Equal("<A>Av<<AA>^AA>AvAA^Av<AAA>^A", path);
        path = solver.GetPathForDirpad(path);
        Assert.Equal("v<<A>>^AvA^Av<A<AA>>^AAvA<^A>AAvA^Av<A>^AA<A>Av<A<A>>^AAAvA<^A>A", path);
        total += int.Parse(code.Substring(0, 3)) * path.Length;

        // 3  7     9   A
        // ^A <<^^A >>A vvvA
        // <A >A v<<A A >^A A >A vA A ^A v<A A A >^A
        // <    A    >  A  v   <  < A    A >  ^   A  A >  A  v   A   A ^  A  v   <  A    A A >  ^A
        // v<<A >>^A vA ^A v<A <A A >>^A A vA <^A >A A vA ^A v<A >^A A <A >A v<A <A >>^A A A vA <^A >A
        // v<<A>>^AvA^Av<A<AA>>^AAvA<^A>AAvA^Av<A>^AA<A>Av<A<A>>^AAAvA<^A>A

        Assert.Equal(24256, total);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe126384()
    {
        Solver solver = new();

        long total = long.Parse(solver.SolvePart1(data));

        Assert.Equal(126384, total);
    }

    /*
    [Fact]
    public void Part2_WithSampleData_ShouldBeXXX()
    {
        throw new NotImplementedException();
    }
    */
    private string[] data;
    private string sampleInput = """
029A
980A
179A
456A
379A
""";
}
