namespace SMath.Statistics;

using System;
using Xunit;

public class KendallCorrelationTest
{
    [Theory]
    [MemberData(nameof(KendallCorrelationData.FullyCorrelatedSequences), MemberType = typeof(KendallCorrelationData))]
    public void EvalArray_FullCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(1d, KendallCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(KendallCorrelationData.FullyCorrelatedSequences), MemberType = typeof(KendallCorrelationData))]
    public void EvalSpan_FullCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(1d, KendallCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Theory]
    [MemberData(nameof(KendallCorrelationData.FullyNegativellyCorrelatedSequences), MemberType = typeof(KendallCorrelationData))]
    public void EvalArray_FullNegativeCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(-1, KendallCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(KendallCorrelationData.FullyNegativellyCorrelatedSequences), MemberType = typeof(KendallCorrelationData))]
    public void EvalSpan_FullNegativeCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(-1, KendallCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Theory]
    [MemberData(nameof(KendallCorrelationData.NotCorrelatedData), MemberType = typeof(KendallCorrelationData))]
    public void EvalArray_NotCorrelated(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, KendallCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(KendallCorrelationData.NotCorrelatedData), MemberType = typeof(KendallCorrelationData))]
    public void EvalSpan_NotCorrelated(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, KendallCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Theory]
    [MemberData(nameof(KendallCorrelationData.SameValues), MemberType = typeof(KendallCorrelationData))]
    public void EvalArray_SameValues(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, KendallCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(KendallCorrelationData.SameValues), MemberType = typeof(KendallCorrelationData))]
    public void EvalSpan_SameValues(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, KendallCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Fact]
    public void EvalArray_BothAreEmpty_NoException() =>
        KendallCorrelation.Eval(Array.Empty<double>(), Array.Empty<double>());

    [Fact]
    public void EvalSpan_BothAreEmpty_NoException() =>
        KendallCorrelation.Eval(new ReadOnlySpan<double>(Array.Empty<double>()), new ReadOnlySpan<double>(Array.Empty<double>()));

    [Fact]
    public void EvalArray_DifferentLength_Exception() =>
        Assert.Throws<ArgumentException>(() => KendallCorrelation.Eval(new int[] { 1 }, new int[] { 1, 2 }));

    [Fact]
    public void EvalSpan_DifferentLength_Exception() =>
        Assert.Throws<ArgumentException>(() => KendallCorrelation.Eval(new ReadOnlySpan<int>(new int[] { 1 }), new ReadOnlySpan<int>(new int[] { 1, 2 })));
}
