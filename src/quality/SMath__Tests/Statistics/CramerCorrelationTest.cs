namespace SMath.Statistics;

using System;
using Xunit;

public class CramerCorrelationTest
{
    [Theory]
    [MemberData(nameof(CramerCorrelationData.FullyAssociatedSequences), MemberType = typeof(CramerCorrelationData))]
    public void EvalArray_FullAssociation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(1d, CramerCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(CramerCorrelationData.FullyAssociatedSequences), MemberType = typeof(CramerCorrelationData))]
    public void EvalSpan_FullAssociation(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(1d, CramerCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Theory]
    [MemberData(nameof(CramerCorrelationData.KnownAssociationData), MemberType = typeof(CramerCorrelationData))]
    public void EvalArray_KnownAssociation(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, CramerCorrelation.Eval(aSequence, bSequence), 6);
    }

    [Theory]
    [MemberData(nameof(CramerCorrelationData.KnownAssociationData), MemberType = typeof(CramerCorrelationData))]
    public void EvalSpan_KnownAssociation(int[] aSequence, int[] bSequence, double expected)
    {
        Assert.Equal(expected, CramerCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)), 6);
    }

    [Theory]
    [MemberData(nameof(CramerCorrelationData.SingleCategoryData), MemberType = typeof(CramerCorrelationData))]
    public void EvalArray_SingleCategory_Undefined(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(double.NaN, CramerCorrelation.Eval(aSequence, bSequence));
    }

    [Theory]
    [MemberData(nameof(CramerCorrelationData.SingleCategoryData), MemberType = typeof(CramerCorrelationData))]
    public void EvalSpan_SingleCategory_Undefined(int[] aSequence, int[] bSequence)
    {
        Assert.Equal(double.NaN, CramerCorrelation.Eval(new ReadOnlySpan<int>(aSequence), new ReadOnlySpan<int>(bSequence)));
    }

    [Fact]
    public void EvalArray_BothAreEmpty_NoException() =>
        CramerCorrelation.Eval(Array.Empty<double>(), Array.Empty<double>());

    [Fact]
    public void EvalSpan_BothAreEmpty_NoException() =>
        CramerCorrelation.Eval(new ReadOnlySpan<double>(Array.Empty<double>()), new ReadOnlySpan<double>(Array.Empty<double>()));

    [Fact]
    public void EvalArray_DifferentLength_Exception() =>
        Assert.Throws<ArgumentException>(() => CramerCorrelation.Eval(new int[] { 1 }, new int[] { 1, 2 }));

    [Fact]
    public void EvalSpan_DifferentLength_Exception() =>
        Assert.Throws<ArgumentException>(() => CramerCorrelation.Eval(new ReadOnlySpan<int>(new int[] { 1 }), new ReadOnlySpan<int>(new int[] { 1, 2 })));
}
