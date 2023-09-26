namespace SMath.Statistics;

using System;
using Xunit;

public class CovarianceTest
{
    [Theory]
    [MemberData(nameof(CovarianceData.Data), MemberType = typeof(CovarianceData))]
    public void Eval(int[] seqA, int[] seqB, int expected)
    {
        var covariance = Covariance.Eval(seqA, seqB, out _);
        Assert.Equal(expected, covariance, 6);
    }

    [Theory]
    [MemberData(nameof(CovarianceData.Data), MemberType = typeof(CovarianceData))]
    public void EvalSpan(int[] seqA, int[] seqB, int expected)
    {
        var covariance = Covariance.Eval(new ReadOnlySpan<int>(seqA), new ReadOnlySpan<int>(seqB));
        Assert.Equal(expected, covariance, 6);
    }

    [Fact]
    public void Eval_BothSequencesAreEmpty_NoException()
    {
        Covariance.Eval(Array.Empty<int>(), Array.Empty<int>(), out _);
    }

    [Fact]
    public void EvalSpan_BothSequencesAreEmpty_NoException()
    {
        Covariance.Eval(new ReadOnlySpan<int>(Array.Empty<int>()), new ReadOnlySpan<int>(Array.Empty<int>()));
    }

    [Fact]
    public void Eval_InconsistentSequences_Exception()
    {
         Assert.Throws<ArgumentException>(() => Covariance.Eval(new int[] { 1 }, new int[] { 1, 2 }));
    }

    [Fact]
    public void EvalSpan_InconsistentSequences_Exception()
    {
        Assert.Throws<ArgumentException>(() => Covariance.Eval(new ReadOnlySpan<int>(new int[] { 1 }), new ReadOnlySpan<int>(new int[] { 1, 2 })));
    }
}