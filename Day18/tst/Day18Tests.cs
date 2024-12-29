using System.Drawing;
using AdventOfCode.Common;
using AdventOfCode.Day18;
using Xunit;

namespace AdventOfCode.Day18.Tests;

public class Day18Tests
{
    public Day18Tests()
    {
        data = sampleInput.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );
    }

    [Fact]
    public void CreateGrid_WithSampleSize_ShouldWork()
    {
        Solver solver = new();

        Node[,] maze = solver.CreateGrid(7, 7, new Node(new Point(0, 0), '.'));

        Assert.Equal(7, maze.GetLength(0));
        Assert.Equal(7, maze.GetLength(1));
        Assert.Equal('.', maze[4, 4].Value);
    }

    [Fact]
    public void ParseInput_WithSampleData_ShouldReturn25Rows()
    {
        Solver solver = new();

        List<Point> walls = solver.ParseData(data);

        Assert.Equal(25, walls.Count);
        Assert.Equal(new Point(5, 4), walls[0]);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe22()
    {
        Solver solver = new();

        Node[,] maze = solver.CreateGrid(7, 7, new Node(new Point(0, 0), '.'));
        List<Point> walls = solver.ParseData(data);

        solver.AddWallsToMaze(ref maze, walls.Take(12).ToList());

        maze[0, 0] = new Node(new Point(0, 0), 'S');
        maze[maze.GetLength(0) - 1, maze.GetLength(1) - 1] =
            new Node(new Point(maze.GetLength(0) - 1, maze.GetLength(0) - 1), 'E');

        Stack<Node> path = solver.AStar(maze, maze[0, 0], maze[6, 6]);

        Assert.Equal(22, path.Count);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBePoint61()
    {
        Solver solver = new();

        Node[,] maze = solver.CreateGrid(7, 7, new Node(new Point(0, 0), '.'));
        List<Point> walls = solver.ParseData(data);

        solver.AddWallsToMaze(ref maze, walls.Take(12).ToList());

        maze[0, 0] = new Node(new Point(0, 0), 'S');
        maze[maze.GetLength(0) - 1, maze.GetLength(1) - 1] =
            new Node(new Point(maze.GetLength(0) - 1, maze.GetLength(0) - 1), 'E');

        int wallIndex = 12;
        bool pathExists = true;
        while (pathExists)
        {
            Point currWall = walls.Skip(wallIndex).Take(1).First();
            maze[currWall.Row, currWall.Col] = new Node(new Point(currWall.Row, currWall.Col), '#');
            Stack<Node> path = solver.AStar(maze, maze[0, 0], maze[6, 6]);
            if (path is null) pathExists = false;
            else
                wallIndex++;
        }

        Assert.Equal(20, wallIndex);
        Assert.Equal(new Point(6, 1), walls[wallIndex]);
    }

    private string[] data;
    private string sampleInput = """
5,4
4,2
4,5
3,0
2,1
6,3
2,4
1,5
0,6
3,3
2,6
5,1
1,2
5,5
2,5
6,5
1,4
0,4
6,4
1,1
6,1
1,0
0,5
1,6
2,0
""";
}
