using System.Drawing;
using System.Numerics;
using System.Text.RegularExpressions;
using AdventOfCode.Common;
using AdventOfCode.Day14;
using Xunit;

namespace AdventOfCode.Day14.Tests;

public class Day14Tests
{
    public Day14Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void ParseLine_WithSampleData_ShouldWork()
    {
        Solver solver = new();
        (Point pos, Vector vel) robot;
        robot = solver.ParseLine(data[0]);

        Point expectedP = new Point(4, 0);
        Vector expectedV = new Vector(-3, 3);

        Assert.Equal(expectedP, robot.pos);
        Assert.Equal(expectedV, robot.vel);
    }

    [Fact]
    public void MoveRobot_WithoutWrapping_ShouldReturnPos()
    {
        Solver solver = new();
        List<(Point pos, Vector vel)> robots = new();
        robots = solver.ParseData(data);

        robots[0] = solver.MoveRobot(7, 11, robots[0]);

        Point newPos = new Point(1, 3);

        Assert.Equal(newPos, robots[0].pos);
    }

    [Fact]
    public void MoveRobot_WrappingSimpleDiag_ShouldReturnPos()
    {
        Solver solver = new();
        List<(Point pos, Vector vel)> robots = new();
        robots = solver.ParseData(data);

        robots[0] = solver.MoveRobot(7, 11, robots[0]);
        robots[0] = solver.MoveRobot(7, 11, robots[0]);

        Point newPos = new Point(5, 6);

        Assert.Equal(newPos, robots[0].pos);
    }

    [Fact]
    public void MoveRobot_WrappingBothXandY_ShouldReturnPos()
    {
        Solver solver = new();
        List<(Point pos, Vector vel)> robots = new();
        robots.Add((new Point(1, 10), new Vector(-3, 4)));

        robots[0] = solver.MoveRobot(7, 11, robots[0]);

        Point newPos = new Point(5, 3);

        Assert.Equal(newPos, robots[0].pos);
    }

    [Fact]
    public void MoveRobot_UsingExample_ShouldReturnPos()
    {
        Solver solver = new();
        List<(Point pos, Vector vel)> robots = new();
        robots.Add((new Point(4, 2), new Vector(-3, 2)));

        robots[0] = solver.MoveRobot(7, 11, robots[0]);
        Assert.Equal(new Point(1, 4), robots[0].pos);
        robots[0] = solver.MoveRobot(7, 11, robots[0]);
        Assert.Equal(new Point(5, 6), robots[0].pos);
        robots[0] = solver.MoveRobot(7, 11, robots[0]);
        Assert.Equal(new Point(2, 8), robots[0].pos);
        robots[0] = solver.MoveRobot(7, 11, robots[0]);
        Assert.Equal(new Point(6, 10), robots[0].pos);
        robots[0] = solver.MoveRobot(7, 11, robots[0]);
        Assert.Equal(new Point(3, 1), robots[0].pos);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe12()
    {
        Solver solver = new();
        long total = 0;

        List<(Point pos, Vector vel)> robots = new();
        robots = solver.ParseData(data);

        robots = solver.MoveRobotsNumTimes(100, 7, 11, robots);

        List<(Point pos, Vector vel)> q1 = robots.Where(rob => rob.pos.Row < 3 && rob.pos.Col < 5).ToList();
        List<(Point pos, Vector vel)> q2 = robots.Where(rob => rob.pos.Row < 3 && rob.pos.Col > 5).ToList();
        List<(Point pos, Vector vel)> q3 = robots.Where(rob => rob.pos.Row > 3 && rob.pos.Col < 5).ToList();
        List<(Point pos, Vector vel)> q4 = robots.Where(rob => rob.pos.Row > 3 && rob.pos.Col > 5).ToList();

        total = q1.Count() * q2.Count() * q3.Count() * q4.Count();

        Assert.Equal(12, total);
    }

    private string[] data;
    private string sampleInput = """
p=0,4 v=3,-3
p=6,3 v=-1,-3
p=10,3 v=-1,2
p=2,0 v=2,-1
p=0,0 v=1,3
p=3,0 v=-2,-2
p=7,6 v=-1,-3
p=3,0 v=-1,-2
p=9,3 v=2,3
p=7,3 v=-1,2
p=2,4 v=2,-3
p=9,5 v=-3,-3
""";
}
