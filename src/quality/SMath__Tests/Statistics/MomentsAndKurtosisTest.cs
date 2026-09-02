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
        Assert.Equal(0.0, Skewness.Eval(new ReadOnlySpan<double>(data)), 6);
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

    [Fact]
    public void StandardizedMoment_SinglePassLazySequence_Succeeds()
    {
        var data = new double[] { 1, 2, 3, 4, 5 };
        var onceOnly = new SinglePassEnumerable<double>(data);

        var result = StandardizedMoment.Eval(onceOnly, 3);
        Assert.Equal(0.0, result, 6);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    public void StandardizedMoment_SpanMatchesEnumerable(int degree)
    {
        var data = new double[] { 1, 2, 4, 7, 11, 16 };
        Assert.Equal(
            StandardizedMoment.Eval(data, degree),
            StandardizedMoment.Eval(new ReadOnlySpan<double>(data), degree),
            12);
    }

    [Fact]
    public void StandardizedMoment_Empty_ReturnsNaN()
    {
        Assert.True(double.IsNaN(StandardizedMoment.Eval(Array.Empty<double>(), 3)));
        Assert.True(double.IsNaN(StandardizedMoment.Eval(ReadOnlySpan<double>.Empty, 3)));
    }

    private sealed class SinglePassEnumerable<T> : System.Collections.Generic.IEnumerable<T>
    {
        private readonly System.Collections.Generic.IEnumerable<T> _source;
        private int _enumerated;

        public SinglePassEnumerable(System.Collections.Generic.IEnumerable<T> source) => _source = source;

        public System.Collections.Generic.IEnumerator<T> GetEnumerator()
        {
            if (System.Threading.Interlocked.Increment(ref _enumerated) > 1)
                throw new InvalidOperationException("Sequence was enumerated more than once.");
            return _source.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
