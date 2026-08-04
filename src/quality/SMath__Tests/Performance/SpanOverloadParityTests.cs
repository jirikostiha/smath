namespace SMath.Tests.Performance;

using System;
using System.Linq;
using SMath;
using SMath.Geometry2D;
using SMath.Statistics;
using Xunit;

/// <summary>
/// The span based overloads are performance oriented alternatives of the enumerable ones.
/// They must produce identical results, these tests lock that contract in.
/// </summary>
public class SpanOverloadParityTests
{
    private static readonly double[] SampleA = [1.5, -2.25, 3d, 0d, 7.75, -4.5, 6d, 2.125];
    private static readonly double[] SampleB = [-3d, 4.5, 1.25, 8d, -1.5, 2d, 5.25, 0.75];

    [Fact]
    public void Product_SpanMatchesEnumerable()
    {
        var fromEnumerable = Product.Eval(SampleA.AsEnumerable());
        var fromSpan = Product.Eval<double>(SampleA);

        Assert.Equal(fromEnumerable, fromSpan);
    }

    [Fact]
    public void Product_SpanReportsCount()
    {
        var fromSpan = Product.Eval<double, int>(SampleA, out var spanCount);
        var fromEnumerable = Product.Eval<double, int>(SampleA.AsEnumerable(), out var enumerableCount);

        Assert.Equal(SampleA.Length, spanCount);
        Assert.Equal(enumerableCount, spanCount);
        Assert.Equal(fromEnumerable, fromSpan);
    }

    [Fact]
    public void Product_EmptySpanIsEmptyProduct()
        => Assert.Equal(1d, Product.Eval<double>(ReadOnlySpan<double>.Empty));

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void Histogram_ExclusiveSpanMatchesEnumerable(int boundCount)
    {
        double[] bounds = [0d, 2.5, 6d];
        var used = bounds.Take(boundCount).ToArray();

        var fromEnumerable = Histogram.GetExclusive<double, int>(SampleA.AsEnumerable(), used);
        var fromSpan = Histogram.GetExclusive<double, int>(SampleA, used);

        Assert.Equal(fromEnumerable, fromSpan);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void Histogram_InclusiveSpanMatchesEnumerable(int boundCount)
    {
        double[] bounds = [0d, 2.5, 6d];
        var used = bounds.Take(boundCount).ToArray();

        var fromEnumerable = Histogram.GetInclusive<double, int>(SampleA.AsEnumerable(), used);
        var fromSpan = Histogram.GetInclusive<double, int>(SampleA, used);

        Assert.Equal(fromEnumerable, fromSpan);
    }

    [Fact]
    public void Histogram_SpanCountsSumUpToDataLength()
    {
        double[] bounds = [0d, 2.5, 6d];
        var bins = Histogram.GetExclusive<double, int>(SampleA, bounds);

        Assert.Equal(SampleA.Length, bins.Sum(bin => bin.Count));
    }

    [Fact]
    public void Variance_SampleSpanMatchesEnumerable()
        => Assert.Equal(
            Variance.Sample.Eval(SampleA.AsEnumerable()),
            Variance.Sample.Eval<double>(SampleA),
            12);

    [Fact]
    public void Variance_PopulationSpanMatchesEnumerable()
        => Assert.Equal(
            Variance.Population.Eval(SampleA.AsEnumerable()),
            Variance.Population.Eval<double>(SampleA),
            12);

    [Fact]
    public void StandardDeviation_SampleSpanMatchesEnumerable()
        => Assert.Equal(
            StandardDeviation.Sample.Eval(SampleA.AsEnumerable()),
            StandardDeviation.Sample.Eval<double>(SampleA),
            12);

    [Fact]
    public void StandardDeviation_SampleIsSquareRootOfVariance()
        => Assert.Equal(
            Math.Sqrt(Variance.Sample.Eval<double>(SampleA)),
            StandardDeviation.Sample.Eval<double>(SampleA),
            12);

    [Fact]
    public void StandardDeviation_PopulationIsSquareRootOfVariance()
        => Assert.Equal(
            Math.Sqrt(Variance.Population.Eval<double>(SampleA)),
            StandardDeviation.Population.Eval<double>(SampleA),
            12);

    [Fact]
    public void Covariance_SpanMatchesEnumerable()
    {
        var fromEnumerable = Covariance.Eval(SampleA.AsEnumerable(), SampleB.AsEnumerable(), out var count);
        var fromSpan = Covariance.Eval<double>(SampleA, SampleB);

        Assert.Equal(SampleA.Length, count);
        Assert.Equal(fromEnumerable, fromSpan, 12);
    }

    [Fact]
    public void Covariance_LazySequenceIsEnumeratedOnlyOnce()
    {
        var enumerations = 0;
        var lazyA = Probe(SampleA, () => enumerations++);
        var lazyB = Probe(SampleB, () => enumerations++);

        Covariance.Eval(lazyA, lazyB, out _);

        // one pass over each of the two sequences, no length pre-check pass
        Assert.Equal(2, enumerations);
    }

    [Fact]
    public void Covariance_InconsistentLengthThrows()
    {
        var shorter = SampleB.Take(SampleB.Length - 1);

        Assert.Throws<ArgumentException>(() => Covariance.Eval(SampleA.AsEnumerable(), shorter, out _));
        Assert.Throws<ArgumentException>(() => Covariance.Eval(shorter, SampleA.AsEnumerable(), out _));
    }

    [Fact]
    public void PearsonCorrelation_SpanMatchesEnumerable()
        => Assert.Equal(
            PearsonCorrelation.Eval(SampleA.AsEnumerable(), SampleB.AsEnumerable()),
            PearsonCorrelation.Eval<double>(SampleA, SampleB),
            12);

    [Fact]
    public void PearsonCorrelation_InconsistentLengthThrows()
    {
        var shorter = SampleB.Take(SampleB.Length - 1);

        Assert.Throws<ArgumentException>(() => PearsonCorrelation.Eval(SampleA.AsEnumerable(), shorter));
    }

    [Fact]
    public void PearsonCorrelation_EvalPerfSpanMatchesList()
    {
        foreach (var lag in new[] { -3, -1, 0, 1, 3 })
        {
            var fromList = PearsonCorrelation.EvalPerf(SampleA, SampleB, lag);
            var fromSpan = PearsonCorrelation.EvalPerf<double, int>(SampleA, SampleB, lag);

            Assert.Equal(fromList, fromSpan, 12);
        }
    }

    [Fact]
    public void PearsonCorrelation_CrossSpanMatchesEnumerable()
    {
        int[] lags = [-2, -1, 0, 1, 2];

        var expected = PearsonCorrelation.Cross.Eval(SampleA, SampleB, lags).ToArray();

        var actual = new double[lags.Length];
        var written = PearsonCorrelation.Cross.Eval<double, int>(SampleA, SampleB, lags, actual);

        Assert.Equal(lags.Length, written);
        for (int i = 0; i < lags.Length; i++)
            Assert.Equal(expected[i].Coef, actual[i], 12);
    }

    [Fact]
    public void PearsonCorrelation_AutoSpanMatchesEnumerable()
    {
        int[] lags = [-2, -1, 0, 1, 2];

        var expected = PearsonCorrelation.Auto.Eval(SampleA, lags).ToArray();

        var actual = new double[lags.Length];
        var written = PearsonCorrelation.Auto.Eval<double, int>(SampleA, lags, actual);

        Assert.Equal(lags.Length, written);
        for (int i = 0; i < lags.Length; i++)
            Assert.Equal(expected[i].Coef, actual[i], 12);
    }

    [Fact]
    public void PearsonCorrelation_AutoAtZeroLagIsPerfectlyCorrelated()
    {
        int[] lags = [0];
        var actual = new double[1];
        PearsonCorrelation.Auto.Eval<double, int>(SampleA, lags, actual);

        Assert.Equal(1d, actual[0], 12);
    }

    [Fact]
    public void PearsonCorrelation_CrossShortDestinationThrows()
    {
        int[] lags = [-1, 0, 1];
        var tooShort = new double[lags.Length - 1];

        Assert.Throws<ArgumentException>(() =>
            PearsonCorrelation.Cross.Eval<double, int>(SampleA, SampleB, lags, tooShort));
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 4)]
    [InlineData(2, 8)]
    [InlineData(5, 20)]
    public void Point2_ManhattanCircleCountMatchesEnumerable(int distance, int expected)
    {
        Assert.Equal(expected, Point2.ManhattanCircleCount(distance));

        if (distance > 0)
            Assert.Equal(
                expected,
                Point2.CoordinatesAtManhattanDistance((0, 0), distance).Count());
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 5)]
    [InlineData(2, 13)]
    [InlineData(4, 41)]
    public void Point2_ManhattanDiskCountMatchesEnumerable(int distance, int expected)
    {
        Assert.Equal(expected, Point2.ManhattanDiskCount(distance));
        Assert.Equal(
            expected,
            Point2.CoordinatesUpToManhattanDistance((0, 0), distance).Count());
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 8)]
    [InlineData(3, 24)]
    public void Point2_ChebyshevRingCountMatchesEnumerable(int distance, int expected)
    {
        Assert.Equal(expected, Point2.ChebyshevRingCount(distance));

        if (distance > 0)
            Assert.Equal(
                expected,
                Point2.CoordinatesAtChebyshevDistance((0, 0), distance).Count());
    }

    [Theory]
    [InlineData(0, 1)]
    [InlineData(1, 9)]
    [InlineData(3, 49)]
    public void Point2_ChebyshevSquareCountMatchesEnumerable(int distance, int expected)
    {
        Assert.Equal(expected, Point2.ChebyshevSquareCount(distance));
        Assert.Equal(
            expected,
            Point2.CoordinatesUpToChebyshevDistance((0, 0), distance).Count());
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(4)]
    public void Point2_CoordinatesAtManhattanDistanceSpanMatchesEnumerable(int distance)
    {
        var expected = Point2.CoordinatesAtManhattanDistance((3, -7), distance).ToArray();

        var buffer = new (int X, int Y)[Point2.ManhattanCircleCount(distance)];
        var written = Point2.CoordinatesAtManhattanDistance((3, -7), distance, buffer);

        Assert.Equal(expected.Length, written);
        Assert.Equal(expected, buffer[..written]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void Point2_CoordinatesUpToManhattanDistanceSpanMatchesEnumerable(int distance)
    {
        var expected = Point2.CoordinatesUpToManhattanDistance((-2, 5), distance).ToArray();

        var buffer = new (int X, int Y)[Point2.ManhattanDiskCount(distance)];
        var written = Point2.CoordinatesUpToManhattanDistance((-2, 5), distance, buffer);

        Assert.Equal(expected.Length, written);
        Assert.Equal(expected, buffer[..written]);
    }

    [Fact]
    public void Point2_CoordinatesUpToManhattanDistanceBoundedSpanMatchesEnumerable()
    {
        var center = (2, 2);
        var bottom = (0, 0);
        var top = (4, 3);

        var expected = Point2.CoordinatesUpToManhattanDistance(center, 3, bottom, top).ToArray();

        var buffer = new (int X, int Y)[Point2.ManhattanDiskCount(3)];
        var written = Point2.CoordinatesUpToManhattanDistance(center, 3, bottom, top, buffer);

        Assert.Equal(expected.Length, written);
        Assert.Equal(expected, buffer[..written]);
    }

    [Theory]
    [InlineData(0, 2)]
    [InlineData(1, 3)]
    [InlineData(2, 2)]
    public void Point2_CoordinatesInManhattanDistanceRangeSpanMatchesEnumerable(int min, int max)
    {
        var expected = Point2.CoordinatesInManhattanDistanceRange((1, 1), min, max).ToArray();

        var buffer = new (int X, int Y)[Point2.ManhattanDiskCount(Math.Max(max, 0))];
        var written = Point2.CoordinatesInManhattanDistanceRange((1, 1), min, max, buffer);

        Assert.Equal(expected.Length, written);
        Assert.Equal(expected, buffer[..written]);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(3)]
    public void Point2_CoordinatesUpToChebyshevDistanceSpanMatchesEnumerable(int distance)
    {
        var expected = Point2.CoordinatesUpToChebyshevDistance((-4, 6), distance).ToArray();

        var buffer = new (int X, int Y)[Point2.ChebyshevSquareCount(distance)];
        var written = Point2.CoordinatesUpToChebyshevDistance((-4, 6), distance, buffer);

        Assert.Equal(expected.Length, written);
        Assert.Equal(expected, buffer[..written]);
    }

    [Fact]
    public void Point2_ShortDestinationThrows()
    {
        var tooShort = new (int X, int Y)[1];

        Assert.Throws<ArgumentException>(() =>
            Point2.CoordinatesUpToManhattanDistance((0, 0), 3, tooShort));
        Assert.Throws<ArgumentException>(() =>
            Point2.CoordinatesUpToChebyshevDistance((0, 0), 3, tooShort));
    }

    [Fact]
    public void Point2_NegativeDistanceWritesNothing()
    {
        var buffer = new (int X, int Y)[4];

        Assert.Equal(0, Point2.CoordinatesAtManhattanDistance((0, 0), -1, buffer));
        Assert.Equal(0, Point2.CoordinatesUpToManhattanDistance((0, 0), -1, buffer));
        Assert.Equal(0, Point2.CoordinatesUpToChebyshevDistance((0, 0), -1, buffer));
    }

    /// <summary> Counts how many times the sequence gets enumerated. </summary>
    private static IEnumerable<T> Probe<T>(IEnumerable<T> source, Action onEnumerate)
    {
        onEnumerate();
        foreach (var item in source)
            yield return item;
    }
}
