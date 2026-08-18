namespace SMath.Statistics;

using System;
using Xunit;

public class SpearmanRankCorrelationTest
{
    [Theory]
    [MemberData(nameof(SpearmanRankCorrelationData.FullyCorrelatedSequences), MemberType = typeof(SpearmanRankCorrelationData))]
    public void EvalArray_FullCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(1d, SpearmanRankCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(SpearmanRankCorrelationData.FullyCorrelatedSequences), MemberType = typeof(SpearmanRankCorrelationData))]
    public void EvalSpan_FullCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(1d, SpearmanRankCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Theory]
    [MemberData(nameof(SpearmanRankCorrelationData.FullyNegativellyCorrelatedSequences), MemberType = typeof(SpearmanRankCorrelationData))]
    public void EvalArray_FullNegativeCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(-1, SpearmanRankCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(SpearmanRankCorrelationData.FullyNegativellyCorrelatedSequences), MemberType = typeof(SpearmanRankCorrelationData))]
    public void EvalSpan_FullNegativeCorrelation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(-1, SpearmanRankCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Theory]
    [MemberData(nameof(SpearmanRankCorrelationData.NotCorrelatedData), MemberType = typeof(SpearmanRankCorrelationData))]
    public void EvalArray_NotCorrelated(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, SpearmanRankCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(SpearmanRankCorrelationData.NotCorrelatedData), MemberType = typeof(SpearmanRankCorrelationData))]
    public void EvalSpan_NotCorrelated(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, SpearmanRankCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Theory]
    [MemberData(nameof(SpearmanRankCorrelationData.SameValues), MemberType = typeof(SpearmanRankCorrelationData))]
    public void EvalArray_SameValues(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, SpearmanRankCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(SpearmanRankCorrelationData.SameValues), MemberType = typeof(SpearmanRankCorrelationData))]
    public void EvalSpan_SameValues(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, SpearmanRankCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Fact]
    public void EvalArray_BothAreEmpty_NoException() =>
        SpearmanRankCorrelation.Eval(Array.Empty<double>(), Array.Empty<double>());

    [Fact]
    public void EvalSpan_BothAreEmpty_NoException() =>
        SpearmanRankCorrelation.Eval(new ReadOnlySpan<double>(Array.Empty<double>()), new ReadOnlySpan<double>(Array.Empty<double>()));

    [Fact]
    public void EvalArray_DifferentLength_Exception() =>
        Assert.Throws<ArgumentException>(() => SpearmanRankCorrelation.Eval(new int[] { 1 }, new int[] { 1, 2 }));

    [Fact]
    public void EvalSpan_DifferentLength_Exception() =>
        Assert.Throws<ArgumentException>(() => SpearmanRankCorrelation.Eval(new ReadOnlySpan<int>(new int[] { 1 }), new ReadOnlySpan<int>(new int[] { 1, 2 })));
}
