namespace SMath.Statistics;

using System;
using Xunit;

public class SampleVarianceTest
{
    [Theory]
    [MemberData(nameof(VarianceData.Sample), MemberType = typeof(VarianceData))]
    public void EvalArray(int[] sequence, double expected)
    {
        var variance = Variance.Sample.Eval(sequence);
        Assert.Equal(expected, variance, 6);
    }

    [Theory]
    [MemberData(nameof(VarianceData.Sample), MemberType = typeof(VarianceData))]
    public void EvalSpan(int[] sequence, double expected)
    {
        var variance = Variance.Sample.Eval(new ReadOnlySpan<int>(sequence));
        Assert.Equal(expected, variance, 6);
    }

    [Fact]
    public void EvalArray_Empty_ReturnsNaN()
    {
        Assert.True(double.IsNaN(Variance.Sample.Eval(Array.Empty<int>())));
    }

    [Fact]
    public void EvalSpan_Empty_ReturnsNaN()
    {
        Assert.True(double.IsNaN(Variance.Sample.Eval(new ReadOnlySpan<int>(Array.Empty<int>()))));
    }

    [Fact]
    public void EvalArray_SingleElement_ReturnsNaN()
    {
        Assert.True(double.IsNaN(Variance.Sample.Eval(new[] { 42 })));
    }

    [Fact]
    public void EvalSpan_SingleElement_ReturnsNaN()
    {
        Assert.True(double.IsNaN(Variance.Sample.Eval(new ReadOnlySpan<int>(new[] { 42 }))));
    }
}