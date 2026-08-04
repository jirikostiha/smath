namespace SMath.Statistics;

using System;
using Xunit;

public class PearsonCorrelationTest
{
    #region Eval
    [Theory]
    [MemberData(nameof(PearsonCorrelationData.FullyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
    public void EvalArray_FullCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(1d, PearsonCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(PearsonCorrelationData.FullyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
    public void EvalSpan_FullCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(1d, PearsonCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Theory]
    [MemberData(nameof(PearsonCorrelationData.FullyNegativellyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
    public void EvalArray_FullNegativeCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(-1, PearsonCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(PearsonCorrelationData.FullyNegativellyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
    public void EvalSpan_FullNegativeCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(-1, PearsonCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Theory]
    [MemberData(nameof(PearsonCorrelationData.NotCorrelatedData), MemberType = typeof(PearsonCorrelationData))]
    public void EvalArray_NotCorrelated(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, PearsonCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(PearsonCorrelationData.NotCorrelatedData), MemberType = typeof(PearsonCorrelationData))]
    public void EvalSpan_NotCorrelated(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, PearsonCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Theory]
    [MemberData(nameof(PearsonCorrelationData.SameValues), MemberType = typeof(PearsonCorrelationData))]
    public void EvalArray_SameValues(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, PearsonCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(PearsonCorrelationData.SameValues), MemberType = typeof(PearsonCorrelationData))]
    public void EvalSpan_SameValues(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, PearsonCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Fact]
    public void EvalArray_BothAreEmpty_NoException() =>
        PearsonCorrelation.Eval(Array.Empty<double>(), Array.Empty<double>());

    [Fact]
    public void EvalSpan_BothAreEmpty_NoException() =>
        PearsonCorrelation.Eval(new ReadOnlySpan<double>(Array.Empty<double>()), new ReadOnlySpan<double>(Array.Empty<double>()));

    [Fact]
    public void EvalArray_DifferentLength_Exception() =>
        Assert.Throws<ArgumentException>(() => PearsonCorrelation.Eval(new int[] { 1 }, new int[] { 1, 2 }));

    [Fact]
    public void EvalSpan_DifferentLength_Exception() =>
        Assert.Throws<ArgumentException>(() => PearsonCorrelation.Eval(new ReadOnlySpan<int>(new int[] { 1 }), new ReadOnlySpan<int>(new int[] { 1, 2 })));
    #endregion

    #region EvalPerf
    [Theory]
    [MemberData(nameof(PearsonCorrelationData.FullyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
    public void EvalArrayPerf_FullCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(1d, PearsonCorrelation.EvalPerf(aSequence, bSequence, 0), 6);
    }

    [Theory]
    [MemberData(nameof(PearsonCorrelationData.FullyNegativellyCorrelatedSequences), MemberType = typeof(PearsonCorrelationData))]
    public void EvalArrayPerf_FullNegativeCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(-1, PearsonCorrelation.EvalPerf(aSequence, bSequence, 0), 6);
    }

    [Theory]
    [MemberData(nameof(PearsonCorrelationData.NotCorrelatedData), MemberType = typeof(PearsonCorrelationData))]
    public void EvalArrayPerf_NotCorrelated(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, PearsonCorrelation.EvalPerf(aSequence, bSequence, 0), 6);
    }

    [Fact]
    public void EvalArrayPerf_BothSequencesAreEmpty_NoException() =>
        PearsonCorrelation.EvalPerf(Array.Empty<double>(), Array.Empty<double>(), 0);

    [Fact]
    public void EvalArrayPerf_ZeroLag_EqualsPlainEval()
    {
        var a = new[] { 1d, 2d, 4d, 7d, 11d, 16d };
        var b = new[] { 2d, 3d, 5d, 8d, 13d, 21d };

        // With lag 0 the perf variant must match the plain Pearson correlation.
        Assert.Equal(PearsonCorrelation.Eval(a, b), PearsonCorrelation.EvalPerf(a, b, 0), 6);
    }

    [Fact]
    public void EvalArrayPerf_NegativeLag_UsesEdgeClamping()
    {
        // Regression: for negative lags the edge-clamping index guard used the
        // nonsensical condition 'iy < Count - iy', which wrongly clamped every
        // index in the upper half of the sequence to the last element.
        // A negative lag of -1 shifts b right by one with the first element
        // repeated on the leading edge, so the reference is b clamped as below.
        var a = new[] { 1d, 2d, 4d, 7d, 11d, 16d };
        var b = new[] { 2d, 3d, 5d, 8d, 13d, 21d };

        // clamped[i] = b[max(0, i - 1)]
        var clamped = new[] { b[0], b[0], b[1], b[2], b[3], b[4] };
        var expected = PearsonCorrelation.Eval(a, clamped);

        Assert.Equal(expected, PearsonCorrelation.EvalPerf(a, b, -1), 6);
    }

    [Theory]
    [MemberData(nameof(PearsonCorrelationData.SameValues), MemberType = typeof(PearsonCorrelationData))]
    public void EvalArrayPerf_SameValues(int[] aSequence, int[] bSequence, double expected)
    {
        // Regression: a constant sequence has no deviation, so the coefficient cannot
        // be decided. EvalPerf used to report 1 (perfect correlation) instead of NaN,
        // which contradicted the plain Eval on the very same data.
        Assert.Equal(expected, PearsonCorrelation.EvalPerf(aSequence, bSequence, 0), 6);
    }
    #endregion
}