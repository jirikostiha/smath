namespace SMath.Statistics;

using System;
using Xunit;

public class MomentsAndKurtosisTest
{
    [Fact]
    public void CentralMoment_Degree1_IsZero()
    {
        var data = new double[] { 1, 2, 3, 4, 5 };
        // First central moment is always zero (sum(x - mean) / n = 0)
        Assert.Equal(0.0, Moment.Eval(data, 1), 6);
        Assert.Equal(0.0, Moment.Eval(new ReadOnlySpan<double>(data), 1), 6);
    }

    [Fact]
    public void CentralMoment_Degree2_IsPopulationVariance()
    {
        var data = new double[] { 1, 2, 3, 4, 5 };
        double expected = Variance.Population.Eval(data);
        Assert.Equal(expected, Moment.Eval(data, 2), 6);
        Assert.Equal(expected, Moment.Eval(new ReadOnlySpan<double>(data), 2), 6);
    }

    [Fact]
    public void StandardizedMoment_Degree1And2()
    {
        var data = new double[] { 1, 2, 3, 4, 5 };
        Assert.Equal(0.0, StandardizedMoment.Eval(data, 1), 6);
        Assert.Equal(1.0, StandardizedMoment.Eval(data, 2), 6);
    }

    [Fact]
    public void Skewness_SymmetricDistribution_IsZero()
    {
        var data = new double[] { 1, 2, 3, 4, 5 };
        Assert.Equal(0.0, Skewness.Eval(data), 6);
        Assert.Equal(0.0, Skewness.Eval(new ReadOnlySpan<double>(data), 6), 6);
    }

    [Fact]
    public void Kurtosis_And_ExcessKurtosis()
    {
        var data = new double[] { 1, 2, 3, 4, 5 };
        double k = Kurtosis.Eval(data);
        double ek = ExcessKurtosis.Eval(data);

        Assert.Equal(k - 3.0, ek, 6);
        Assert.Equal(k, Kurtosis.Eval(new ReadOnlySpan<double>(data)), 6);
    }
}
