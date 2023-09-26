namespace SMath.Statistics;

using System;
using Xunit;

public class PopulationVarianceTest
{
    [Theory]
    [MemberData(nameof(VarianceData.Population), MemberType = typeof(VarianceData))]
    public void EvalArray(int[] sequence, double expected)
    {
        var variance = Variance.Population.Eval(sequence);
        Assert.Equal(expected, variance, 6);
    }

    [Theory]
    [MemberData(nameof(VarianceData.Population), MemberType = typeof(VarianceData))]
    public void EvalSpan(int[] sequence, double expected)
    {
        var variance = Variance.Population.Eval(new ReadOnlySpan<int>(sequence));
        Assert.Equal(expected, variance, 6);
    }

    [Fact]
    public void EvalArray_Empty_NoException()
    {
        Variance.Population.Eval(Array.Empty<int>());
    }

    [Fact]
    public void EvalSpan_Empty_NoException()
    {
        Variance.Population.Eval(new ReadOnlySpan<int>(Array.Empty<int>()));
    }
}