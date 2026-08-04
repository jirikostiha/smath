using System;
using SMath.Grid2D;
using SMath.Pathfinding;
using Xunit;

namespace SMath;

public class GridFloodFillTests
{
    private readonly struct Map : IGridPredicate
    {
        private readonly string[] _rows;

        public Map(params string[] rows)
            => _rows = rows;

        public int Width
            => _rows[0].Length;

        public int Height
            => _rows.Length;

        public int CellCount
            => Width * Height;

        public bool Test(int x, int y)
            => _rows[y][x] == '.';
    }

    [Fact]
    public void Distances_OverEmptyRow_GrowByOne()
    {
        var map = new Map("....");
        var distances = new int[map.CellCount];
        var queue = new int[map.CellCount];

        var count = GridFloodFill.Distances((0, 0), map.Width, map.Height, map, distances, queue);

        Assert.Equal(4, count);
        Assert.Equal(new[] { 0, 1, 2, 3 }, distances);
    }

    [Fact]
    public void Distances_BehindWall_AreUnreached()
    {
        var map = new Map("..#..");
        var distances = new int[map.CellCount];
        var queue = new int[map.CellCount];

        GridFloodFill.Distances((0, 0), map.Width, map.Height, map, distances, queue);

        Assert.Equal(GridFloodFill.Unreached, distances[2]);
        Assert.Equal(GridFloodFill.Unreached, distances[4]);
    }

    [Fact]
    public void Distances_FromManySources_TakeTheNearestOne()
    {
        var map = new Map("......");
        var distances = new int[map.CellCount];
        var queue = new int[map.CellCount];
        Span<(int X, int Y)> sources = stackalloc (int X, int Y)[2] { (0, 0), (5, 0) };

        GridFloodFill.Distances(sources, map.Width, map.Height, map, distances, queue);

        Assert.Equal(new[] { 0, 1, 2, 2, 1, 0 }, distances);
    }

    [Fact]
    public void Distances_UpToMaxDistance_LeaveFarCellsUnreached()
    {
        var map = new Map("......");
        var distances = new int[map.CellCount];
        var queue = new int[map.CellCount];

        GridFloodFill.Distances((0, 0), map.Width, map.Height, map, distances, queue,
            GridConnectivity.Axial, 2);

        Assert.Equal(2, distances[2]);
        Assert.Equal(GridFloodFill.Unreached, distances[3]);
    }

    [Fact]
    public void Distances_Diagonally_AreChebyshev()
    {
        var map = new Map("...", "...", "...");
        var distances = new int[map.CellCount];
        var queue = new int[map.CellCount];

        GridFloodFill.Distances((0, 0), map.Width, map.Height, map, distances, queue,
            GridConnectivity.DiagonalCornerCutting);

        Assert.Equal(2, distances[Grid.Index((2, 2), map.Width)]);
    }

    [Fact]
    public void AreConnected()
    {
        var map = new Map("..#..");
        var distances = new int[map.CellCount];
        var queue = new int[map.CellCount];

        Assert.True(GridFloodFill.AreConnected((0, 0), (1, 0), map.Width, map.Height, map, distances, queue));
        Assert.False(GridFloodFill.AreConnected((0, 0), (4, 0), map.Width, map.Height, map, distances, queue));
        Assert.False(GridFloodFill.AreConnected((0, 0), (9, 9), map.Width, map.Height, map, distances, queue));
    }

    [Fact]
    public void Regions_OfSeparatedAreas_AreLabeledApart()
    {
        var map = new Map(
            "..#..",
            "..#..",
            "..#..");
        var labels = new int[map.CellCount];
        var queue = new int[map.CellCount];

        var count = GridFloodFill.Regions(map.Width, map.Height, map, labels, queue);

        Assert.Equal(2, count);
        Assert.Equal(GridFloodFill.Unreached, labels[2]);
        Assert.NotEqual(labels[0], labels[4]);
    }

    [Fact]
    public void Regions_OfOpenGrid_IsOne()
    {
        var map = new Map("...", "...");
        var labels = new int[map.CellCount];
        var queue = new int[map.CellCount];

        Assert.Equal(1, GridFloodFill.Regions(map.Width, map.Height, map, labels, queue));
        Assert.All(labels, label => Assert.Equal(0, label));
    }

    [Fact]
    public void Regions_OfFullGrid_IsZero()
    {
        var map = new Map("###", "###");
        var labels = new int[map.CellCount];
        var queue = new int[map.CellCount];

        Assert.Equal(0, GridFloodFill.Regions(map.Width, map.Height, map, labels, queue));
    }
}
