using System;
using System.Linq;
using SMath.Grid2D;
using Xunit;

namespace SMath;

public class GridTests
{
    [Theory]
    [InlineData(0, 0, 4, 0)]
    [InlineData(3, 0, 4, 3)]
    [InlineData(0, 1, 4, 4)]
    [InlineData(2, 3, 4, 14)]
    public void Index_And_Cell_AreInverse(int x, int y, int width, int expectedIndex)
    {
        Assert.Equal(expectedIndex, Grid.Index(x, y, width));
        Assert.Equal((x, y), Grid.Cell(expectedIndex, width));
    }

    [Theory]
    [InlineData(0, 0, true)]
    [InlineData(3, 4, true)]
    [InlineData(4, 4, false)]
    [InlineData(-1, 0, false)]
    [InlineData(0, -1, false)]
    public void Contains(int x, int y, bool expected)
        => Assert.Equal(expected, Grid.Contains(x, y, 4, 5));

    [Theory]
    [InlineData(-1, 4, 3)]
    [InlineData(0, 4, 0)]
    [InlineData(4, 4, 0)]
    [InlineData(-5, 4, 3)]
    public void Wrap(int value, int length, int expected)
        => Assert.Equal(expected, Grid.Wrap(value, length));

    [Fact]
    public void Neighbors4_InCorner_HasTwo()
    {
        Span<int> neighbors = stackalloc int[4];

        var count = Grid.Neighbors4(0, 0, 3, 3, neighbors);

        Assert.Equal(2, count);
        Assert.Equal(new[] { 1, 3 }, neighbors[..count].ToArray());
    }

    [Fact]
    public void Neighbors4_InMiddle_HasFour()
    {
        Span<int> neighbors = stackalloc int[4];

        Assert.Equal(4, Grid.Neighbors4(1, 1, 3, 3, neighbors));
    }

    [Fact]
    public void Neighbors8_InMiddle_HasEight()
    {
        Span<int> neighbors = stackalloc int[8];

        Assert.Equal(8, Grid.Neighbors8(1, 1, 3, 3, neighbors));
    }

    [Fact]
    public void Neighbors8_InCorner_HasThree()
    {
        Span<int> neighbors = stackalloc int[8];

        Assert.Equal(3, Grid.Neighbors8(0, 0, 3, 3, neighbors));
    }

    [Fact]
    public void Cells_HasAllOfThem()
        => Assert.Equal(6, Grid.Cells(3, 2).Distinct().Count());

    [Fact]
    public void Spiral_IsOrderedByDistance()
    {
        var cells = Grid.Spiral((5, 5), 2).ToArray();

        Assert.Equal((5, 5), cells[0]);
        Assert.Equal(25, cells.Length);
        Assert.Equal(25, cells.Distinct().Count());
    }

    [Fact]
    public void Spiral_InGrid_SkipsOutsideCells()
    {
        var cells = Grid.Spiral((0, 0), 1, 3, 3).ToArray();

        Assert.Equal(new[] { (0, 0), (1, 0), (1, 1), (0, 1) }, cells);
    }
}
