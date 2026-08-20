namespace SMath.Statistics;

using System;
using System.Collections.Generic;
using Xunit;

public class MeansTest
{
    [Fact]
    public void GeometricMean_Values()
    {
        var data = new double[] { 1, 2, 4 };
        // Geometric mean of 1, 2, 4 is (8)^(1/3) = 2
        Assert.Equal(2.0, GeometricMean.Eval(data), 6);
        Assert.Equal(2.0, GeometricMean.Eval(new ReadOnlySpan<double>(data)), 6);
    }

    [Fact]
    public void GeometricMean_ContainsZero()
    {
        var data = new double[] { 1, 0, 4 };
        Assert.Equal(0.0, GeometricMean.Eval(data));
        Assert.Equal(0.0, GeometricMean.Eval(new ReadOnlySpan<double>(data)));
    }

    [Fact]
    public void GeometricMean_ContainsNegative()
    {
        var data = new double[] { -1, 2, 4 };
        Assert.True(double.IsNaN(GeometricMean.Eval(data)));
        Assert.True(double.IsNaN(GeometricMean.Eval(new ReadOnlySpan<double>(data))));
    }

    [Fact]
    public void HarmonicMean_Values()
    {
        var data = new double[] { 1, 2, 4 };
        // 3 / (1/1 + 1/2 + 1/4) = 3 / 1.75 = 12/7 ~ 1.7142857
        Assert.Equal(12.0 / 7.0, HarmonicMean.Eval(data), 6);
        Assert.Equal(12.0 / 7.0, HarmonicMean.Eval(new ReadOnlySpan<double>(data)), 6);
    }

    [Fact]
    public void HarmonicMean_ContainsZero()
    {
        var data = new double[] { 1, 0, 4 };
        Assert.Equal(0.0, HarmonicMean.Eval(data));
        Assert.Equal(0.0, HarmonicMean.Eval(new ReadOnlySpan<double>(data)));
    }

    [Fact]
    public void QuadraticMean_Values()
    {
        var data = new double[] { 1, 2, 3 };
        // sqrt((1 + 4 + 9)/3) = sqrt(14/3) ~ 2.16024689
        double expected = Math.Sqrt(14.0 / 3.0);
        Assert.Equal(expected, QuadraticMean.Eval(data), 6);
        Assert.Equal(expected, QuadraticMean.Eval(new ReadOnlySpan<double>(data)), 6);
    }

    [Fact]
    public void CubicMean_Values()
    {
        var data = new double[] { 1, 2, 3 };
        // cbrt((1 + 8 + 27)/3) = cbrt(36/3) = cbrt(12) ~ 2.289428485
        double expected = Math.Cbrt(12.0);
        Assert.Equal(expected, CubicMean.Eval(data), 6);
        Assert.Equal(expected, CubicMean.Eval(new ReadOnlySpan<double>(data)), 6);
    }

    [Fact]
    public void GeneralizedMean_SpecialCases()
    {
        var data = new double[] { 1, 2, 4 };

        Assert.Equal(GeometricMean.Eval(data), GeneralizedMean.Eval(data, 0), 6);
        Assert.Equal(ArithmeticMean.Eval(data), GeneralizedMean.Eval(data, 1), 6);
        Assert.Equal(QuadraticMean.Eval(data), GeneralizedMean.Eval(data, 2), 6);
        Assert.Equal(HarmonicMean.Eval(data), GeneralizedMean.Eval(data, -1), 6);
        Assert.Equal(CubicMean.Eval(data), GeneralizedMean.Eval(data, 3), 6);
    }

    [Fact]
    public void WeightedArithmeticMean_Pairs()
    {
        var pairs = new KeyValuePair<double, double>[]
        {
            new(1.0, 2.0),
            new(3.0, 1.0),
            new(5.0, 1.0)
        };
        // (1*2 + 3*1 + 5*1) / (2+1+1) = 10 / 4 = 2.5
        Assert.Equal(2.5, WeightedArithmeticMean.Eval(pairs), 6);
    }

    [Fact]
    public void WeightedArithmeticMean_ParallelSequences()
    {
        var values = new double[] { 1.0, 3.0, 5.0 };
        var weights = new double[] { 2.0, 1.0, 1.0 };

        Assert.Equal(2.5, WeightedArithmeticMean.Eval(values, weights), 6);
        Assert.Equal(2.5, WeightedArithmeticMean.Eval(new ReadOnlySpan<double>(values), new ReadOnlySpan<double>(weights)), 6);
    }
}
