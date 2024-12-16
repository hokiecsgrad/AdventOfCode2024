using AdventOfCode.Common;
using AdventOfCode.Day15;
using Xunit;

namespace AdventOfCode.Day15.Tests;

public class Day15Tests
{
    public Day15Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries
            );
        dataSimp = simpleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries
            );
    }

    [Fact]
    public void ParseData_WithSimpleData_ShouldWork()
    {
        Solver solver = new();
        char[,] grid = solver.ParseGrid(dataSimp);
        List<char> instructions = solver.ParseInstructions(dataSimp);

        Assert.Equal(8, grid.GetLength(0));
        Assert.Equal(8, grid.GetLength(1));
        Assert.Equal(15, instructions.Count());
    }

    [Fact]
    public void FindChar_WithSimpleData_ShouldBe22()
    {
        Solver solver = new();
        char[,] grid = solver.ParseGrid(dataSimp);
        Point robot = solver.FindCharInGrid(grid, '@');

        Assert.Equal(new Point(2, 2), robot);
    }

    [Fact]
    public void MoveRobot_WithNoBlockers_ShouldWork()
    {
        Solver solver = new();
        char[,] grid = new char[2, 2] { { '.', '.' }, { '.', '@' } };
        List<char> instructions = new List<char> { '<', '^', '>', 'v' };
        Point newPos = new Point(-1, -1);

        newPos = solver.MoveRobot(ref grid, new Point(1, 1), instructions[0]);
        Assert.Equal('@', grid[newPos.Row, newPos.Col]);
        Assert.Equal('.', grid[1, 1]);
        newPos = solver.MoveRobot(ref grid, new Point(1, 0), instructions[1]);
        Assert.Equal('@', grid[newPos.Row, newPos.Col]);
        Assert.Equal('.', grid[1, 0]);
        newPos = solver.MoveRobot(ref grid, new Point(0, 0), instructions[2]);
        Assert.Equal('@', grid[newPos.Row, newPos.Col]);
        Assert.Equal('.', grid[0, 0]);
        newPos = solver.MoveRobot(ref grid, new Point(0, 1), instructions[3]);
        Assert.Equal('@', grid[newPos.Row, newPos.Col]);
        Assert.Equal('.', grid[0, 1]);
        newPos = solver.MoveRobot(ref grid, new Point(1, 1), '>');
        Assert.Equal('@', grid[newPos.Row, newPos.Col]);
        Assert.Equal('.', grid[0, 1]);
        newPos = solver.MoveRobot(ref grid, new Point(1, 1), 'v');
        Assert.Equal('@', grid[newPos.Row, newPos.Col]);
        Assert.Equal('.', grid[0, 1]);
    }

    [Fact]
    public void MoveRobot_WithBlockers_ShouldWork()
    {
        Solver solver = new();
        char[,] grid = solver.ParseGrid(dataSimp);
        List<char> instructions = new List<char> { '<', '^', '^', '^', '^', '^' };
        Point newPos = new Point(2, 2);

        foreach (char dir in instructions)
            newPos = solver.MoveRobot(ref grid, newPos, dir);

        Assert.Equal(new Point(1, 2), newPos);
    }

    [Fact]
    public void MoveRobot_WithOneSmallBoxNoBlocker_ShouldWork()
    {
        Solver solver = new();
        char[,] grid = solver.ParseGrid(data);
        List<char> instructions = new List<char> { '<' };
        Point newPos = new Point(4, 4);

        foreach (char dir in instructions)
            newPos = solver.MoveRobot(ref grid, newPos, dir);

        Assert.Equal(new Point(4, 3), newPos);
        Assert.Equal('O', grid[4, 2]);
        Assert.Equal('.', grid[4, 1]);
        Assert.Equal('.', grid[4, 4]);
    }

    [Fact]
    public void MoveRobot_WithOneBigBoxNoBlocker_ShouldWork()
    {
        Solver solver = new();
        Point newPos;
        char[,] smallGrid = solver.ParseGrid(data);
        char[,] grid = solver.ScaleUpGrid(smallGrid);

        Point robot = solver.FindCharInGrid(grid, '@');

        newPos = solver.MoveRobot(ref grid, robot, '<');

        Assert.Equal('[', grid[4, 5]);
        Assert.Equal(']', grid[4, 6]);
        Assert.Equal('@', grid[4, 7]);
    }

    [Fact]
    public void MoveRobotLeft_WithOneBigBoxOneBlocker_ShouldMoveToEdge()
    {
        Solver solver = new();
        Point newPos;
        char[,] smallGrid = solver.ParseGrid(data);
        char[,] grid = solver.ScaleUpGrid(smallGrid);

        Point robot = solver.FindCharInGrid(grid, '@');
        newPos = new Point(robot.Row, robot.Col);

        newPos = solver.MoveRobot(ref grid, newPos, '<');
        newPos = solver.MoveRobot(ref grid, newPos, '<');
        newPos = solver.MoveRobot(ref grid, newPos, '<');
        newPos = solver.MoveRobot(ref grid, newPos, '<');
        newPos = solver.MoveRobot(ref grid, newPos, '<');
        newPos = solver.MoveRobot(ref grid, newPos, '<');

        Assert.Equal('[', grid[4, 2]);
        Assert.Equal(']', grid[4, 3]);
        Assert.Equal('@', grid[4, 4]);
        Assert.Equal('.', grid[4, 5]);
        Assert.Equal('.', grid[4, 6]);
        Assert.Equal('.', grid[4, 7]);
        Assert.Equal('.', grid[4, 8]);
    }

    [Fact]
    public void MoveRobotUp_WithOneBigBoxNoBlocker_ShouldMoveUp()
    {
        Solver solver = new();
        char[,] grid = new char[4, 3] {
            { '#', '#', '#' },
            { '.', '.', '.' },
            { '.', '[', ']' },
            { '.', '@', '.' } };

        bool canMove = solver.CanMoveBigBoxUp(grid, new List<Point> { new Point(2, 1), new Point(2, 2) }, -1);

        Assert.True(canMove);
    }

    [Fact]
    public void MoveRobotUp_WithLotsBigBoxNoBlocker_ShouldMoveUp()
    {
        Solver solver = new();
        char[,] grid = new char[6, 3] {
            { '#', '#', '#' },
            { '.', '.', '.' },
            { '.', '[', ']' },
            { '[', ']', '.' },
            { '[', ']', '.' },
            { '.', '@', '.' },
            };

        bool canMove = solver.CanMoveBigBoxUp(grid, new List<Point> { new Point(4, 0), new Point(4, 1) }, -1);
        if (canMove)
        {
            solver.MoveBigBoxUp(ref grid, new List<Point> { new Point(4, 0), new Point(4, 1) }, -1);
            grid[4, 1] = '@';
            grid[5, 1] = '.';
        }

        char[,] expected = new char[6, 3] {
            { '#', '#', '#' },
            { '.', '[', ']' },
            { '[', ']', '.' },
            { '[', ']', '.' },
            { '.', '@', '.' },
            { '.', '.', '.' },
            };

        Assert.True(canMove);
        Assert.Equal(expected, grid);
    }

    [Fact]
    public void MoveRobotDown_WithLotsBigBoxNoBlocker_ShouldMoveUp()
    {
        Solver solver = new();
        char[,] grid = new char[6, 3] {
            { '#', '#', '#' },
            { '.', '@', '.' },
            { '.', '[', ']' },
            { '[', ']', '.' },
            { '[', ']', '.' },
            { '.', '.', '.' },
            };

        bool canMove = solver.CanMoveBigBoxUp(grid, new List<Point> { new Point(2, 1), new Point(2, 2) }, 1);
        if (canMove)
        {
            solver.MoveBigBoxUp(ref grid, new List<Point> { new Point(2, 1), new Point(2, 2) }, 1);
            grid[2, 1] = '@';
            grid[1, 1] = '.';
        }

        char[,] expected = new char[6, 3] {
            { '#', '#', '#' },
            { '.', '.', '.' },
            { '.', '@', '.' },
            { '.', '[', ']' },
            { '[', ']', '.' },
            { '[', ']', '.' },
            };

        Assert.True(canMove);
        Assert.Equal(expected, grid);
    }

    [Fact]
    public void MoveRobot_WithOneBoxAndBlockers_ShouldNotMove()
    {
        Solver solver = new();
        char[,] grid = solver.ParseGrid(data);
        List<char> instructions = new List<char> { '<', '<', '<' };
        Point newPos = new Point(4, 4);

        foreach (char dir in instructions)
            newPos = solver.MoveRobot(ref grid, newPos, dir);

        Assert.Equal(new Point(4, 2), newPos);
        Assert.Equal('O', grid[4, 1]);
        Assert.Equal('.', grid[4, 3]);
        Assert.Equal('.', grid[4, 4]);
    }

    [Fact]
    public void Part1_WithSimpleData_ShouldBe2028()
    {
        Solver solver = new();

        long gps = long.Parse(solver.SolvePart1(dataSimp));

        Assert.Equal(2028, gps);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe10092()
    {
        Solver solver = new();

        long gps = long.Parse(solver.SolvePart1(data));

        Assert.Equal(10092, gps);
    }

    [Fact]
    public void ScaleUp_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        char[,] smallGrid = solver.ParseGrid(data);
        char[,] grid = solver.ScaleUpGrid(smallGrid);

        Point robot = solver.FindCharInGrid(grid, '@');

        string[] expectedStr = sampleInputScaled.Split(
            '\n',
            StringSplitOptions.TrimEntries
            );
        char[,] expected = solver.ParseGrid(expectedStr);

        Assert.Equal(expected, grid);
        Assert.Equal(10, grid.GetLength(0));
        Assert.Equal(20, grid.GetLength(1));
        Assert.Equal(new Point(4, 8), robot);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldMatchExpectedGrid()
    {
        Solver solver = new();
        char[,] smallGrid = solver.ParseGrid(data);
        char[,] grid = solver.ScaleUpGrid(smallGrid);
        List<char> instructions = solver.ParseInstructions(data);

        Point robot = solver.FindCharInGrid(grid, '@');
        Point newPos = new Point(robot.Row, robot.Col);

        foreach (char dir in instructions)
            newPos = solver.MoveRobot(ref grid, newPos, dir);

        string[] expectedStr = sampleInputScaledExpected.Split(
            '\n',
            StringSplitOptions.TrimEntries
            );
        char[,] expected = solver.ParseGrid(expectedStr);

        Assert.Equal(expected, grid);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe9021()
    {
        Solver solver = new();

        long gps = long.Parse(solver.SolvePart2(data));

        Assert.Equal(9021, gps);
    }

    private string[] data;
    private string[] dataSimp;
    private string sampleInput = """
##########
#..O..O.O#
#......O.#
#.OO..O.O#
#..O@..O.#
#O#..O...#
#O..O..O.#
#.OO.O.OO#
#....O...#
##########

<vv>^<v^>v>^vv^v>v<>v^v<v<^vv<<<^><<><>>v<vvv<>^v^>^<<<><<v<<<v^vv^v>^
vvv<<^>^v^^><<>>><>^<<><^vv^^<>vvv<>><^^v>^>vv<>v<<<<v<^v>^<^^>>>^<v<v
><>vv>v^v^<>><>>>><^^>vv>v<^^^>>v^v^<^^>v^^>v^<^v>v<>>v^v^<v>v^^<^^vv<
<<v<^>>^^^^>>>v^<>vvv^><v<<<>^^^vv^<vvv>^>v<^^^^v<>^>vvvv><>>v^<<^^^^^
^><^><>>><>^^<<^^v>>><^<v>^<vv>>v>>>^v><>^v><<<<v>>v<v<v>vvv>^<><<>^><
^>><>^v<><^vvv<^^<><v<<<<<><^v<<<><<<^^<v<^^^><^>>^<v^><<<^>>^v<v^v<v^
>^>>^v>vv>^<<^v<>><<><<v<<v><>v<^vv<<<>^^v^>^^>>><<^v>>v^v><^^>>^<>vv^
<><^^>^^^<><vvvvv^v<v<<>^v<v>v<<^><<><<><<<^^<<<^<<>><<><^^^>^^<>^>v<>
^^>vv<^v^v<vv>^<><v<^v>^^^>>>^^vvv^>vvv<>>>^<^>>>>>^<<^v>^vvv<>^<><<v>
v^^>>><<^^<>>^v^<v^vv<>v^<<>^<^v^v><^<<<><<^<v><v<>vv>>v><v^<vv<>v^<<^
""";
    private string simpleInput = """
########
#..O.O.#
##@.O..#
#...O..#
#.#.O..#
#...O..#
#......#
########

<^^>>>vv<v>>v<<
""";
    private string sampleInputScaled = """
####################
##....[]....[]..[]##
##............[]..##
##..[][]....[]..[]##
##....[]@.....[]..##
##[]##....[]......##
##[]....[]....[]..##
##..[][]..[]..[][]##
##........[]......##
####################

""";
    private string sampleInputScaledExpected = """
####################
##[].......[].[][]##
##[]...........[].##
##[]........[][][]##
##[]......[]....[]##
##..##......[]....##
##..[]............##
##..@......[].[][]##
##......[][]..[]..##
####################

""";
}
