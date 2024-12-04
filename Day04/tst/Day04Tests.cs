using AdventOfCode.Common;
using AdventOfCode.Day04;
using Xunit;

namespace AdventOfCode.Day04.Tests;

public class Day04Tests
{
    public Day04Tests()
    {
    }

    [Fact]
    public void Part1_WithSimpleData_ShouldBe4()
    {
        data = simpleInputPart1.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );

        Solver solver = new();
        int total = 0;

        char[,] grid = solver.CreateGrid(data);

        for (int y = 0; y < grid.GetLength(0); y++)
            for (int x = 0; x < grid.GetLength(1); x++)
                if (grid[y, x] == 'X')
                {
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            if (solver.IsMas(grid, (y + i, x + j), (i, j)))
                                total++;
                }

        Assert.Equal(4, total);
    }

    [Fact]
    public void Part1_WithSampleData_ShouldBe18()
    {
        data = sampleInputPart1.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );

        Solver solver = new();
        int total = 0;

        char[,] grid = solver.CreateGrid(data);

        for (int y = 0; y < grid.GetLength(0); y++)
            for (int x = 0; x < grid.GetLength(1); x++)
                if (grid[y, x] == 'X')
                {
                    for (int i = -1; i <= 1; i++)
                        for (int j = -1; j <= 1; j++)
                            if (solver.IsMas(grid, (y + i, x + j), (i, j)))
                                total++;
                }

        Assert.Equal(18, total);
    }

    [Fact]
    public void Part2_WithSampleData_ShouldBe9()
    {
        data = sampleInputPart2.Split(
            '\n',
            StringSplitOptions.TrimEntries |
            StringSplitOptions.RemoveEmptyEntries
            );

        Solver solver = new();
        int total = 0;

        char[,] grid = solver.CreateGrid(data);

        for (int y = 0; y < grid.GetLength(0); y++)
            for (int x = 0; x < grid.GetLength(1); x++)
                if (grid[y, x] == 'M')
                {
                    if (solver.IsMas(grid, (y, x), (1, 1)) &&
                        solver.IsMas(grid, (y, x + 2), (1, -1)))
                        total++;
                    else if (solver.IsMas(grid, (y, x), (1, 1)) &&
                        solver.IsSam(grid, (y, x + 2), (1, -1)))
                        total++;
                }
                else if (grid[y, x] == 'S')
                {
                    if (solver.IsSam(grid, (y, x), (1, 1)) &&
                        solver.IsMas(grid, (y, x + 2), (1, -1)))
                        total++;
                    else if (solver.IsSam(grid, (y, x), (1, 1)) &&
                        solver.IsSam(grid, (y, x + 2), (1, -1)))
                        total++;
                }

        Assert.Equal(9, total);
    }

    private string[] data;
    private string simpleInputPart1 = """
..X...
.SAMX.
.A..A.
XMAS.S
.X....
""";
    private string sampleInputPart1 = """
MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX
""";
    private string sampleInputPart2 = """
.M.S......
..A..MSMS.
.M.S.MAA..
..A.ASMSM.
.M.S.M....
..........
S.S.S.S.S.
.A.A.A.A..
M.M.M.M.M.
..........
""";
}
