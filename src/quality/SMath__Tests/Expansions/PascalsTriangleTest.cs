using Xunit;

namespace SMath.Expansions;

public class PascalsTriangleTest
{
    [Fact]
    public void Row()
    {
        Assert.Equal(new[] { 1 }, PascalsTriangle.Row<int>(0));
        Assert.Equal(new[] { 1, 1 }, PascalsTriangle.Row<int>(1));
        Assert.Equal(new[] { 1, 2, 1 }, PascalsTriangle.Row<int>(2));
        Assert.Equal(new[] { 1, 3, 3, 1 }, PascalsTriangle.Row<int>(3));
        Assert.Equal(new[] { 1, 4, 6, 4, 1 }, PascalsTriangle.Row<int>(4));
    }

    [Fact]
    public void Row_WithBaseNumber()
    {
        Assert.Equal(new[] { 2, 8, 12, 8, 2 }, PascalsTriangle.Row(4, 2));
    }

    [Fact]
    public void Row_MatchesBinomialCoefficients()
    {
        const int rowNumber = 12;
        var row = PascalsTriangle.Row<long>(rowNumber);

        for (int k = 0; k < row.Length; k++)
            Assert.Equal(BinomialCoefficient.Eval((long)rowNumber, k), row[k]);
    }

    [Theory]
    [InlineData(-1, 0)]
    [InlineData(0, 1)]
    [InlineData(4, 5)]
    public void RowLength(int rowNumber, int expected)
    {
        Assert.Equal(expected, PascalsTriangle.RowLength(rowNumber));
    }

    [Fact]
    public void Row_BufferMatchesArray()
    {
        Span<int> buffer = stackalloc int[PascalsTriangle.RowLength(4)];
        var count = PascalsTriangle.Row(4, 1, buffer);

        Assert.Equal(5, count);
        Assert.Equal(PascalsTriangle.Row<int>(4), buffer[..count].ToArray());
    }

    [Fact]
    public void Row_ShortBufferThrows()
    {
        var tooShort = new int[2];

        Assert.Throws<ArgumentException>(() => PascalsTriangle.Row(4, 1, tooShort));
    }

    [Fact]
    public void Rows()
    {
        Assert.Equal(
            new[] { new[] { 1 }, new[] { 1, 1 }, new[] { 1, 2, 1 } },
            PascalsTriangle.Rows<int>(3).ToArray());
    }

    [Fact]
    public void Rows_NonPositiveCountIsEmpty()
    {
        Assert.Empty(PascalsTriangle.Rows<int>(0));
    }
}
