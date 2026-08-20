namespace SMath.Statistics;

using System;
using System.Linq;
using Xunit;

public class MedianAndModeTest
{
    [Fact]
    public void Median_OddLength()
    {
        var data = new double[] { 5, 1, 3 };
        Assert.Equal(3.0, Median.Eval(data));
        Assert.Equal(3.0, Median.Eval(new ReadOnlySpan<double>(data)));
    }

    [Fact]
    public void Median_EvenLength()
    {
        var data = new double[] { 5, 1, 3, 9 };
        // sorted: 1, 3, 5, 9 -> median (3 + 5)/2 = 4
        Assert.Equal(4.0, Median.Eval(data));
        Assert.Equal(4.0, Median.Eval(new ReadOnlySpan<double>(data)));
    }

    [Fact]
    public void Median_Empty()
    {
        Assert.True(double.IsNaN(Median.Eval(Array.Empty<double>())));
        Assert.True(double.IsNaN(Median.Eval(new ReadOnlySpan<double>(Array.Empty<double>()))));
    }

    [Fact]
    public void Mode_Unimodal()
    {
        var data = new int[] { 1, 2, 2, 3, 4 };
        var modes = Mode.Eval(data).ToArray();

        Assert.Single(modes);
        Assert.Equal(2, modes[0]);
    }

    [Fact]
    public void Mode_Bimodal()
    {
        var data = new int[] { 1, 1, 2, 3, 3 };
        var modes = Mode.Eval(data).ToArray();

        Assert.Equal(2, modes.Length);
        Assert.Contains(1, modes);
        Assert.Contains(3, modes);
    }

    [Fact]
    public void Mode_SpanOverload()
    {
        var data = new int[] { 7, 8, 8, 9 };
        var modes = Mode.Eval(new ReadOnlySpan<int>(data)).ToArray();

        Assert.Single(modes);
        Assert.Equal(8, modes[0]);
    }
}
