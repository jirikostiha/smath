using System;
using System.Linq;
using SMath.Grid2D;
using SMath.Pathfinding;
using Xunit;

namespace SMath;

public class GridPathfinderTests
{
    /// <summary> Map of '.' passable and '#' impassable cells given by rows from the bottom one. </summary>
    private readonly struct Map : IGridPredicate
    {
        private readonly string[] _rows;

        public Map(params string[] rows)
            => _rows = rows;

        public int Width
            => _rows[0].Length;

        public int Height
            => _rows.Length;

        public bool Test(int x, int y)
            => _rows[y][x] == '.';
    }

    [Fact]
    public void FindPath_OverEmptyGrid_IsStraight()
    {
        var map = new Map(
            "....",
            "....",
            "....");
        var pathfinder = new GridPathfinder(map.Width, map.Height);

        Assert.True(pathfinder.FindPath((0, 0), (3, 0), map));
        Assert.Equal(4, pathfinder.PathCellCount);
        Assert.Equal(3 * GridStep.StraightCost, pathfinder.PathCost);
        Assert.Equal(new[] { (0, 0), (1, 0), (2, 0), (3, 0) }, pathfinder.Path());
    }

    [Fact]
    public void FindPath_AroundWall_GoesAround()
    {
        // the wall spans the rows 1 and 2, the only way is over the open row 0
        var map = new Map(
            ".....",
            "..#..",
            "..#..");
        var pathfinder = new GridPathfinder(map.Width, map.Height);

        Assert.True(pathfinder.FindPath((0, 2), (4, 2), map));
        Assert.Equal(new[] { (0, 2), (4, 2) }, new[] { pathfinder.Path().First(), pathfinder.Path().Last() });
        Assert.Contains((2, 0), pathfinder.Path());
    }

    [Fact]
    public void FindPath_BehindWall_IsNotFound()
    {
        var map = new Map(
            "..#..",
            "..#..",
            "..#..");
        var pathfinder = new GridPathfinder(map.Width, map.Height);

        Assert.False(pathfinder.FindPath((0, 0), (4, 0), map));
        Assert.Equal(0, pathfinder.PathCellCount);
        Assert.Equal(-1, pathfinder.PathCost);
    }

    [Fact]
    public void FindPath_ToImpassableCell_IsNotFound()
    {
        var map = new Map(
            "..#",
            "...");
        var pathfinder = new GridPathfinder(map.Width, map.Height);

        Assert.False(pathfinder.FindPath((0, 1), (2, 0), map));
    }

    [Fact]
    public void FindPath_OutsideOfGrid_IsNotFound()
    {
        var map = new Map("...", "...");
        var pathfinder = new GridPathfinder(map.Width, map.Height);

        Assert.False(pathfinder.FindPath((0, 0), (9, 9), map));
        Assert.False(pathfinder.FindPath((-1, 0), (1, 1), map));
    }

    [Fact]
    public void FindPath_Diagonally_IsShorterThanAxially()
    {
        var map = new Map(
            "....",
            "....",
            "....",
            "....");
        var pathfinder = new GridPathfinder(map.Width, map.Height);

        pathfinder.FindPath((0, 0), (3, 3), map, GridConnectivity.Axial);
        var axialCount = pathfinder.PathCellCount;

        pathfinder.FindPath((0, 0), (3, 3), map, GridConnectivity.Diagonal);
        var diagonalCount = pathfinder.PathCellCount;

        Assert.Equal(7, axialCount);
        Assert.Equal(4, diagonalCount);
    }

    [Fact]
    public void FindPath_Diagonally_DoesNotCutCorner()
    {
        // both cells shared by the start and the target diagonal are walls
        var map = new Map(
            ".#",
            "#.");
        var pathfinder = new GridPathfinder(map.Width, map.Height);

        Assert.False(pathfinder.FindPath((0, 0), (1, 1), map, GridConnectivity.Diagonal));
        Assert.True(pathfinder.FindPath((0, 0), (1, 1), map, GridConnectivity.DiagonalCornerCutting));
    }

    [Fact]
    public void FindPath_IsRepeatable()
    {
        var map = new Map(
            ".....",
            ".###.",
            ".....");
        var pathfinder = new GridPathfinder(map.Width, map.Height);

        var first = pathfinder.FindPath((0, 0), (4, 2), map) ? pathfinder.Path().ToArray() : Array.Empty<(int, int)>();

        for (var i = 0; i < 3; i++)
        {
            Assert.True(pathfinder.FindPath((0, 0), (4, 2), map));
            Assert.Equal(first, pathfinder.Path());
        }
    }

    [Fact]
    public void FindPath_ReversePath_IsReverseOfPath()
    {
        var map = new Map("....", "....");
        var pathfinder = new GridPathfinder(map.Width, map.Height);

        pathfinder.FindPath((0, 0), (3, 1), map);

        Assert.Equal(pathfinder.Path().Reverse(), pathfinder.ReversePath());
    }

    [Fact]
    public void FindPath_WithWeightedCells_AvoidsExpensiveOnes()
    {
        // the middle row is ten times more expensive than the top one
        var weights = new[]
        {
            1, 1, 1, 1,
            10, 10, 10, 10,
            1, 1, 1, 1,
        };
        var pathfinder = new GridPathfinder(4, 3);

        Assert.True(pathfinder.FindPath((0, 1), (3, 1), new GridCostMap(weights, 4), default(ManhattanHeuristic)));

        Assert.DoesNotContain((1, 1), pathfinder.Path());
    }

    [Fact]
    public void Fill_MarksUnreachableCells()
    {
        var map = new Map(
            "..#..",
            "..#..",
            "..#..");
        var pathfinder = new GridPathfinder(map.Width, map.Height);
        var distances = new int[map.Width * map.Height];

        pathfinder.Fill((0, 0), new GridPassabilityCost<Map>(map), distances);

        Assert.Equal(0, distances[Grid.Index((0, 0), map.Width)]);
        Assert.Equal(int.MaxValue, distances[Grid.Index((4, 0), map.Width)]);
        Assert.Equal(int.MaxValue, distances[Grid.Index((2, 0), map.Width)]);
    }

    [Fact]
    public void Fill_FromManySources_TakesTheNearestOne()
    {
        var map = new Map("......");
        var pathfinder = new GridPathfinder(map.Width, map.Height);
        var distances = new int[map.Width * map.Height];
        Span<(int X, int Y)> sources = stackalloc (int X, int Y)[2] { (0, 0), (5, 0) };

        pathfinder.Fill(sources, new GridPassabilityCost<Map>(map), distances);

        Assert.Equal(0, distances[0]);
        Assert.Equal(0, distances[5]);
        Assert.Equal(2 * GridStep.StraightCost, distances[2]);
        Assert.Equal(2 * GridStep.StraightCost, distances[3]);
    }

    [Fact]
    public void Fill_UpToMaxCost_LeavesFarCellsUnreachable()
    {
        var map = new Map("......");
        var pathfinder = new GridPathfinder(map.Width, map.Height);
        var distances = new int[map.Width * map.Height];

        var count = pathfinder.Fill((0, 0), new GridPassabilityCost<Map>(map), distances,
            GridConnectivity.Axial, 2 * GridStep.StraightCost);

        Assert.Equal(3, count);
        Assert.Equal(int.MaxValue, distances[3]);
    }

    [Fact]
    public void Constructor_OfEmptyGrid_Throws()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new GridPathfinder(0, 1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new GridPathfinder(1, -1));
    }
}
