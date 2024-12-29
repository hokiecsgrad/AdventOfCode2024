using AdventOfCode.Common;
using AdventOfCode.Day20;
using Xunit;

namespace AdventOfCode.Day20.Tests;

public class Day20Tests
{
    public Day20Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void ParseGrid_WithSampleData_ShouldWork()
    {
        Solver solver = new();

        Node[,] maze = solver.ParseGrid(data);

        Assert.Equal('S', maze[3, 1].Value);
        Assert.Equal('E', maze[7, 5].Value);
    }

    [Fact]
    public void Maze_WithSampleData_ShouldBe84()
    {
        Solver solver = new();

        Node[,] maze = solver.ParseGrid(data);
        Node start = solver.GetPositionOf(maze, 'S');
        Node end = solver.GetPositionOf(maze, 'E');
        Stack<Node> path = solver.AStar(maze, start, end);

        Assert.Equal(84, path.Count);
    }

    [Fact]
    public void Part1_WithSampleDataSavingAtLeastTenSeconds_ShouldBe10()
    {
        Solver solver = new();
        int total = 0;
        List<Stack<Node>> allPaths = new();

        Node[,] maze = solver.ParseGrid(data);
        Node start = solver.GetPositionOf(maze, 'S');
        Node end = solver.GetPositionOf(maze, 'E');
        Stack<Node> path = solver.AStar(maze, start, end);
        int normalSolution = path.Count;

        List<Node> wholePath = new() { start };
        foreach (Node point in path)
            wholePath.Add(point);

        List<Node> pathSoFar = new();
        foreach (Node pos in wholePath)
        {
            List<Node> cheats = solver.GetNeighbors(maze, pos, pathSoFar);
            foreach (Node cheat in cheats)
            {
                maze[cheat.Position.Row, cheat.Position.Col].Value = '.';
                Stack<Node> newPath = solver.AStar(maze, start, end);
                allPaths.Add(newPath);
                maze[cheat.Position.Row, cheat.Position.Col].Value = '#';
            }

            pathSoFar.Add(pos);
        }

        total = allPaths.Where(p => p.Count <= normalSolution - 10).Count();

        Assert.Equal(10, total);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBeXXX()
    {
        throw new NotImplementedException();
    }

    private string[] data;
    private string sampleInput = """
###############
#...#...#.....#
#.#.#.#.#.###.#
#S#...#.#.#...#
#######.#.#.###
#######.#.#...#
#######.#.###.#
###..E#...#...#
###.#######.###
#...###...#...#
#.#####.#.###.#
#.#...#.#.#...#
#.#.#.#.#.#.###
#...#...#...###
###############
""";
}
