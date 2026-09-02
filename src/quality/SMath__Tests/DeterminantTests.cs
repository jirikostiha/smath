using Xunit;

namespace SMath;

public class DeterminantTests
{
    [Fact]
    public void FromCells_2x2()
    {
        // | 1 2 |
        // | 3 4 |  = 1*4 - 2*3 = -2
        Assert.Equal(-2, Determinant.FromCells(1, 2, 3, 4));
    }

    [Fact]
    public void FromCells_3x3_Identity_IsOne()
    {
        Assert.Equal(1, Determinant.FromCells(
            1, 0, 0,
            0, 1, 0,
            0, 0, 1));
    }

    [Theory]
    // det of:
    // | 1 2 3 |
    // | 4 5 6 |
    // | 7 8 10 | = -3
    [InlineData(1, 2, 3, 4, 5, 6, 7, 8, 10, -3)]
    // | 2 0 1 |
    // | 3 0 0 |
    // | 5 1 1 | = 3
    [InlineData(2, 0, 1, 3, 0, 0, 5, 1, 1, 3)]
    // | 6 1 1 |
    // | 4 -2 5 |
    // | 2 8 7 | = -306
    [InlineData(6, 1, 1, 4, -2, 5, 2, 8, 7, -306)]
    public void FromCells_3x3(
        int r1c1, int r1c2, int r1c3,
        int r2c1, int r2c2, int r2c3,
        int r3c1, int r3c2, int r3c3,
        int expected)
    {
        Assert.Equal(expected, Determinant.FromCells(
            r1c1, r1c2, r1c3,
            r2c1, r2c2, r2c3,
            r3c1, r3c2, r3c3));
    }

    [Fact]
    public void FromRows_And_FromColumns_2x2()
    {
        Assert.Equal(-2, Determinant.FromRows((1, 2), (3, 4)));
        Assert.Equal(-2, Determinant.FromColumns((1, 3), (2, 4)));
    }

    [Fact]
    public void FromRows_And_FromColumns_3x3()
    {
        var row1 = (1, 2, 3);
        var row2 = (4, 5, 6);
        var row3 = (7, 8, 10);
        Assert.Equal(-3, Determinant.FromRows(row1, row2, row3));

        var col1 = (1, 4, 7);
        var col2 = (2, 5, 8);
        var col3 = (3, 6, 10);
        Assert.Equal(-3, Determinant.FromColumns(col1, col2, col3));
    }
}
