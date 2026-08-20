namespace SMath.Statistics;

using System;
using Xunit;

public class WeightedPearsonCorrelationTest
{
    private static readonly double[] SampleA = [1d, 2d, 3d, 4d, 5d];
    private static readonly double[] SampleB = [2d, 1d, 4d, 3d, 5d];
    private static readonly double[] SampleWeights = [1d, 2d, 3d, 4d, 5d];

    // reference coefficient of the data above, taken from the two pass definition
    // sum(w*(a-am)*(b-bm)) / sqrt(sum(w*(a-am)^2) * sum(w*(b-bm)^2)), the unweighted
    // coefficient of the same data is 0.8
    private const double ExpectedCoefficient = 0.8124207484594865d;

    [Fact]
    public void EvalArray_KnownData()
        => Assert.Equal(
            ExpectedCoefficient,
            PearsonCorrelation.Weighted.Eval(SampleA, SampleB, SampleWeights),
            12);

    [Fact]
    public void EvalSpan_KnownData()
        => Assert.Equal(
            ExpectedCoefficient,
            PearsonCorrelation.Weighted.Eval(
                new ReadOnlySpan<double>(SampleA),
                new ReadOnlySpan<double>(SampleB),
                new ReadOnlySpan<double>(SampleWeights)),
            12);

    [Fact]
    public void EvalArray_ScaledWeights_SameCoefficient()
    {
        // the sum of weights cancels out in the ratio, scaling them cannot move the coefficient
        double[] scaled = [2d, 4d, 6d, 8d, 10d];

        Assert.Equal(
            PearsonCorrelation.Weighted.Eval(SampleA, SampleB, SampleWeights),
            PearsonCorrelation.Weighted.Eval(SampleA, SampleB, scaled),
            12);
    }

    [Fact]
    public void EvalArray_EqualWeights_EqualsPlainCorrelation()
    {
        double[] ones = [1d, 1d, 1d, 1d, 1d];

        Assert.Equal(
            PearsonCorrelation.Eval(SampleA, SampleB),
            PearsonCorrelation.Weighted.Eval(SampleA, SampleB, ones),
            12);
    }

    [Fact]
    public void EvalSpan_EqualWeights_EqualsPlainCorrelation()
    {
        double[] ones = [1d, 1d, 1d, 1d, 1d];

        Assert.Equal(
            PearsonCorrelation.Eval(new ReadOnlySpan<double>(SampleA), new ReadOnlySpan<double>(SampleB)),
            PearsonCorrelation.Weighted.Eval(
                new ReadOnlySpan<double>(SampleA),
                new ReadOnlySpan<double>(SampleB),
                new ReadOnlySpan<double>(ones)),
            12);
    }

    [Fact]
    public void EvalSpan_MatchesEnumerable()
        => Assert.Equal(
            PearsonCorrelation.Weighted.Eval(SampleA, SampleB, SampleWeights),
            PearsonCorrelation.Weighted.Eval(
                new ReadOnlySpan<double>(SampleA),
                new ReadOnlySpan<double>(SampleB),
                new ReadOnlySpan<double>(SampleWeights)),
            12);

    [Fact]
    public void EvalArray_DoubledWeight_EqualsRepeatedPair()
    {
        // weighting a pair by two has to count exactly as listing the same pair twice
        double[] a = [1d, 2d, 3d, 1d];
        double[] b = [2d, 1d, 4d, 2d];
        double[] ones = [1d, 1d, 1d, 1d];

        double[] aOnce = [1d, 2d, 3d];
        double[] bOnce = [2d, 1d, 4d];
        double[] doubledWeights = [2d, 1d, 1d];

        Assert.Equal(
            PearsonCorrelation.Weighted.Eval(a, b, ones),
            PearsonCorrelation.Weighted.Eval(aOnce, bOnce, doubledWeights),
            12);
    }

    [Fact]
    public void EvalArray_ZeroWeight_PairIsIgnored()
    {
        double[] a = [1d, 2d, 3d, 1000d];
        double[] b = [2d, 1d, 4d, -1000d];
        double[] weights = [1d, 2d, 3d, 0d];

        double[] aWithout = [1d, 2d, 3d];
        double[] bWithout = [2d, 1d, 4d];
        double[] weightsWithout = [1d, 2d, 3d];

        Assert.Equal(
            PearsonCorrelation.Weighted.Eval(aWithout, bWithout, weightsWithout),
            PearsonCorrelation.Weighted.Eval(a, b, weights),
            12);
    }

    [Fact]
    public void EvalArray_LeadingZeroWeights_PairIsIgnored()
    {
        // the incremental update divides by the running sum of weights,
        // a leading zero weight must not turn that first division into 0/0
        double[] a = [1000d, 1d, 2d, 3d];
        double[] b = [-1000d, 2d, 1d, 4d];
        double[] weights = [0d, 1d, 2d, 3d];

        double[] aWithout = [1d, 2d, 3d];
        double[] bWithout = [2d, 1d, 4d];
        double[] weightsWithout = [1d, 2d, 3d];

        Assert.Equal(
            PearsonCorrelation.Weighted.Eval(aWithout, bWithout, weightsWithout),
            PearsonCorrelation.Weighted.Eval(a, b, weights),
            12);
    }

    [Fact]
    public void EvalArray_LargeOffset_StaysAccurate()
    {
        // correlation does not depend on the origin, and deviations are taken from the running
        // mean instead of from raw power sums, so a large common offset must not eat the
        // significant digits, the same data through sums of squares is already off in the fifth
        double[] a = [1e6 + 1d, 1e6 + 2d, 1e6 + 3d, 1e6 + 4d, 1e6 + 5d];
        double[] b = [1e6 + 2d, 1e6 + 1d, 1e6 + 4d, 1e6 + 3d, 1e6 + 5d];

        Assert.Equal(ExpectedCoefficient, PearsonCorrelation.Weighted.Eval(a, b, SampleWeights), 9);
    }

    [Fact]
    public void EvalArray_FullCorrelation()
    {
        double[] a = [1d, 2d, 3d];
        double[] b = [2d, 4d, 6d];
        double[] weights = [0.5d, 2d, 3.25d];

        Assert.Equal(1d, PearsonCorrelation.Weighted.Eval(a, b, weights), 12);
    }

    [Fact]
    public void EvalArray_FullNegativeCorrelation()
    {
        double[] a = [1d, 2d, 3d];
        double[] b = [-2d, -4d, -6d];
        double[] weights = [0.5d, 2d, 3.25d];

        Assert.Equal(-1d, PearsonCorrelation.Weighted.Eval(a, b, weights), 12);
    }

    [Fact]
    public void EvalArray_ConstantSequence_IsNaN()
    {
        // a constant sequence has no deviation, the coefficient cannot be decided
        double[] a = [1d, 1d, 1d];
        double[] b = [1d, 2d, 3d];
        double[] weights = [1d, 2d, 3d];

        Assert.True(double.IsNaN(PearsonCorrelation.Weighted.Eval(a, b, weights)));
    }

    [Fact]
    public void EvalArray_AllWeightsAreZero_IsNaN()
    {
        double[] weights = [0d, 0d, 0d, 0d, 0d];

        Assert.True(double.IsNaN(PearsonCorrelation.Weighted.Eval(SampleA, SampleB, weights)));
    }

    [Fact]
    public void EvalArray_AllAreEmpty_IsNaN()
        => Assert.True(double.IsNaN(PearsonCorrelation.Weighted.Eval(
            Array.Empty<double>(), Array.Empty<double>(), Array.Empty<double>())));

    [Fact]
    public void EvalSpan_AllAreEmpty_IsNaN()
        => Assert.True(double.IsNaN(PearsonCorrelation.Weighted.Eval(
            ReadOnlySpan<double>.Empty, ReadOnlySpan<double>.Empty, ReadOnlySpan<double>.Empty)));

    [Fact]
    public void EvalArray_DifferentSequenceLength_Exception()
        => Assert.Throws<ArgumentException>(()
            => PearsonCorrelation.Weighted.Eval(new[] { 1d }, new[] { 1d, 2d }, new[] { 1d }));

    [Fact]
    public void EvalArray_DifferentWeightsLength_Exception()
        => Assert.Throws<ArgumentException>(()
            => PearsonCorrelation.Weighted.Eval(new[] { 1d, 2d }, new[] { 1d, 2d }, new[] { 1d }));

    [Fact]
    public void EvalSpan_DifferentSequenceLength_Exception()
        => Assert.Throws<ArgumentException>(()
            => PearsonCorrelation.Weighted.Eval(
                new ReadOnlySpan<double>(new[] { 1d }),
                new ReadOnlySpan<double>(new[] { 1d, 2d }),
                new ReadOnlySpan<double>(new[] { 1d })));

    [Fact]
    public void EvalSpan_DifferentWeightsLength_Exception()
        => Assert.Throws<ArgumentException>(()
            => PearsonCorrelation.Weighted.Eval(
                new ReadOnlySpan<double>(new[] { 1d, 2d }),
                new ReadOnlySpan<double>(new[] { 1d, 2d }),
                new ReadOnlySpan<double>(new[] { 1d })));

    [Fact]
    public void EvalArray_NegativeWeight_Exception()
        => Assert.Throws<ArgumentOutOfRangeException>(()
            => PearsonCorrelation.Weighted.Eval(new[] { 1d, 2d }, new[] { 1d, 2d }, new[] { 1d, -1d }));

    [Fact]
    public void EvalSpan_NegativeWeight_Exception()
        => Assert.Throws<ArgumentOutOfRangeException>(()
            => PearsonCorrelation.Weighted.Eval(
                new ReadOnlySpan<double>(new[] { 1d, 2d }),
                new ReadOnlySpan<double>(new[] { 1d, 2d }),
                new ReadOnlySpan<double>(new[] { 1d, -1d })));

    [Fact]
    public void EvalArray_LazySequencesAreEnumeratedOnlyOnce()
    {
        var enumerations = 0;
        var lazyA = Probe(SampleA, () => enumerations++);
        var lazyB = Probe(SampleB, () => enumerations++);
        var lazyWeights = Probe(SampleWeights, () => enumerations++);

        var coefficient = PearsonCorrelation.Weighted.Eval(lazyA, lazyB, lazyWeights);

        Assert.Equal(ExpectedCoefficient, coefficient, 12);

        // one pass over each of the three sequences, no length pre-check pass
        Assert.Equal(3, enumerations);
    }

    /// <summary> Counts how many times the sequence gets enumerated. </summary>
    private static IEnumerable<T> Probe<T>(IEnumerable<T> source, Action onEnumerate)
    {
        onEnumerate();
        foreach (var item in source)
            yield return item;
    }
}
