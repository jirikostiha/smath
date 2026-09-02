namespace SMath.Statistics;

using Xunit;

public class StandardDeviationTest
{
    [Theory]
    [InlineData(0, 0)]
    [InlineData(1, 1)]
    [InlineData(3.5, 1.8708287)]
    public void FromVariance(double variance, double expected)
    {
        Assert.Equal(expected, StandardDeviation.FromVariance(variance), 6);
    }

    [Fact]
    public void SampleEval_Empty_ReturnsNaN()
    {
        Assert.True(double.IsNaN(StandardDeviation.Sample.Eval(System.Array.Empty<int>())));
        Assert.True(double.IsNaN(StandardDeviation.Sample.Eval(new System.ReadOnlySpan<int>(System.Array.Empty<int>()))));
    }

    [Fact]
    public void SampleEval_SingleElement_ReturnsNaN()
    {
        Assert.True(double.IsNaN(StandardDeviation.Sample.Eval(new[] { 42 })));
        Assert.True(double.IsNaN(StandardDeviation.Sample.Eval(new System.ReadOnlySpan<int>(new[] { 42 }))));
    }

    [Fact]
    public void PopulationEval_Empty_ReturnsNaN()
    {
        Assert.True(double.IsNaN(StandardDeviation.Population.Eval(System.Array.Empty<int>())));
        Assert.True(double.IsNaN(StandardDeviation.Population.Eval(new System.ReadOnlySpan<int>(System.Array.Empty<int>()))));
    }
}