namespace SMath.Statistics;

using System.Linq;
using Xunit;

public class HistogramTest
{
    [Fact]
    public void GetExclusive_ValueOnEdge()
    {
        var sequence = new[] { 0d };
        var bounds = new[] { 0d };

        var bins = Histogram.GetExclusive<double, int>(sequence, bounds).ToArray();

        Assert.Equal(0, bins[0].Count);
        Assert.Equal(1, bins[1].Count);
    }

    [Fact]
    public void GetInclusive_ValueOnEdge()
    {
        var sequence = new[] { 0d };
        var bounds = new[] { 0d };

        var bins = Histogram.GetInclusive<double, int>(sequence, bounds).ToArray();

        Assert.Equal(1, bins[0].Count);
        Assert.Equal(0, bins[1].Count);
    }

    [Fact]
    public void GetExclusive_SeveralValues()
    {
        var sequence = new[] { -3d, -2, -1, 0, 1, 2 };
        var bounds = new[] { -4d, -1, 1.5 };

        var bins = Histogram.GetExclusive<double, int>(sequence, bounds).ToArray();

        Assert.Equal(0, bins[0].Count);
        Assert.Equal(2, bins[1].Count);
        Assert.Equal(3, bins[2].Count);
        Assert.Equal(1, bins[3].Count);
    }
}
