using System;
using System.Linq;
using SMath.Grid2D;
using Xunit;

namespace SMath;

public class RasterTests
{
    private readonly struct NoObstacle : IGridPredicate
    {
        public bool Test(int x, int y)
            => false;
    }

    private readonly struct WallAtX : IGridPredicate
    {
        private readonly int _x;

        public WallAtX(int x)
            => _x = x;

        public bool Test(int x, int y)
            => x == _x;
    }

    [Fact]
    public void Line_Horizontal()
    {
        var cells = Raster.Line((0, 0), (3, 0)).ToArray();

        Assert.Equal(new[] { (0, 0), (1, 0), (2, 0), (3, 0) }, cells);
    }

    [Fact]
    public void Line_Diagonal()
    {
        var cells = Raster.Line((0, 0), (3, 3)).ToArray();

        Assert.Equal(new[] { (0, 0), (1, 1), (2, 2), (3, 3) }, cells);
    }

    [Fact]
    public void Line_SinglePoint()
        => Assert.Equal(new[] { (2, 2) }, Raster.Line((2, 2), (2, 2)).ToArray());

    [Theory]
    [InlineData(0, 0, 3, 0, 4)]
    [InlineData(0, 0, 3, 3, 4)]
    [InlineData(0, 0, 0, 5, 6)]
    [InlineData(2, 2, 2, 2, 1)]
    public void LineCellCount_MatchesEnumeration(int x1, int y1, int x2, int y2, int expected)
    {
        Assert.Equal(expected, Raster.LineCellCount((x1, y1), (x2, y2)));
        Assert.Equal(expected, Raster.Line((x1, y1), (x2, y2)).Count());
    }

    [Fact]
    public void Line_ToBuffer_MatchesEnumeration()
    {
        var from = (1, 2);
        var to = (9, 5);
        Span<(int X, int Y)> cells = stackalloc (int X, int Y)[Raster.LineCellCount(from, to)];

        var count = Raster.Line(from, to, cells);

        Assert.Equal(Raster.Line(from, to).ToArray(), cells[..count].ToArray());
    }

    [Fact]
    public void ThickLine_IsFourConnected()
    {
        var cells = Raster.ThickLine((0, 0), (3, 2)).ToArray();

        for (var i = 1; i < cells.Length; i++)
        {
            var step = Math.Abs(cells[i].Item1 - cells[i - 1].Item1)
                + Math.Abs(cells[i].Item2 - cells[i - 1].Item2);

            Assert.Equal(1, step);
        }
    }

    [Fact]
    public void IsVisible_OverEmptyGrid_IsTrue()
        => Assert.True(Raster.IsVisible((0, 0), (5, 3), default(NoObstacle)));

    [Fact]
    public void IsVisible_BehindWall_IsFalse()
        => Assert.False(Raster.IsVisible((0, 0), (5, 0), new WallAtX(2)));

    [Fact]
    public void Cast_StopsBeforeWall()
        => Assert.Equal((1, 0), Raster.Cast((0, 0), (5, 0), new WallAtX(2)));

    [Fact]
    public void Cast_OverEmptyGrid_ReachesTarget()
        => Assert.Equal((5, 3), Raster.Cast((0, 0), (5, 3), default(NoObstacle)));

    [Fact]
    public void Circle_IsSymmetric()
    {
        var cells = Raster.Circle((0, 0), 3).ToHashSet();

        foreach (var cell in cells)
        {
            Assert.Contains((-cell.X, cell.Y), cells);
            Assert.Contains((cell.Y, cell.X), cells);
        }
    }

    [Fact]
    public void Circle_OfZeroRadius_IsCenter()
        => Assert.Equal(new[] { (1, 1) }, Raster.Circle((1, 1), 0).ToArray());

    [Fact]
    public void Disc_ContainsOnlyCellsInRadius()
    {
        foreach (var cell in Raster.Disc((0, 0), 4))
            Assert.True((cell.X * cell.X) + (cell.Y * cell.Y) <= 4 * 4 + 4);
    }

    [Fact]
    public void CellOfPoint_SnapsDown()
    {
        Assert.Equal((0, 0), Raster.CellOfPoint((0.5, 0.9), 1d));
        Assert.Equal((2, 1), Raster.CellOfPoint((5.0, 3.0), 2d));
        Assert.Equal((-1, -1), Raster.CellOfPoint((-0.5, -0.5), 1d));
    }

    [Fact]
    public void CenterOfCell_IsInTheMiddle()
    {
        Assert.Equal((3d, 3d), Raster.CenterOfCell((1, 1), 2d));
        Assert.Equal((0.5d, 0.5d), Raster.CenterOfCell((0, 0), 1d));
    }

    [Fact]
    public void CenterOfCell_IsInverseOfCellOfPoint()
        => Assert.Equal((2, 3), Raster.CellOfPoint(Raster.CenterOfCell((2, 3), 1.5d), 1.5d));
}
