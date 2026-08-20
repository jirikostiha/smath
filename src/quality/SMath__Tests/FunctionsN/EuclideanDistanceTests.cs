using System;
using System.Linq;
using SMath.Geometry2D;
using Xunit;

namespace SMath.FunctionsN;

public class EuclideanDistanceTests
{
    private static readonly double[] Point = [3d, 4d];
    private static readonly double[] PointA = [1d, 2d];
    private static readonly double[] PointB = [4d, 6d];
    private static readonly double[] Shorter = [1d];

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("sqrt(sum(xi^2))", EuclideanDistance.PlainTextFormula);
    }

    [Fact]
    public void Eval_FromOrigin_Span()
    {
        Assert.Equal(5d, EuclideanDistance.Eval(new ReadOnlySpan<double>(Point)), 6);
        Assert.Equal(3d, EuclideanDistance.Eval<double>([1d, 2d, 2d]), 6);
        Assert.Equal(0d, EuclideanDistance.Eval<double>([]), 6);
    }

    [Fact]
    public void Eval_FromOrigin_SpanMatchesEnumerable()
    {
        Assert.Equal(
            EuclideanDistance.Eval(Point.AsEnumerable()),
            EuclideanDistance.Eval(new ReadOnlySpan<double>(Point)));
    }

    [Fact]
    public void Eval_TwoPoints_Span()
    {
        Assert.Equal(
            5d,
            EuclideanDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(PointB)),
            6);
    }

    [Fact]
    public void Eval_TwoPoints_SpanMatchesEnumerable()
    {
        Assert.Equal(
            EuclideanDistance.Eval(PointA.AsEnumerable(), PointB.AsEnumerable()),
            EuclideanDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(PointB)));
    }

    [Fact]
    public void Eval_TwoPoints_MatchesPoint2Distance()
    {
        Assert.Equal(
            Point2.Distance((1d, 2d), (4d, 6d)),
            EuclideanDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(PointB)),
            9);
    }

    [Fact]
    public void Eval_InconsistentLengthThrows()
    {
        Assert.Throws<ArgumentException>(() =>
            EuclideanDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(Shorter)));
        Assert.Throws<ArgumentException>(() =>
            EuclideanDistance.Eval(PointA.AsEnumerable(), Shorter.AsEnumerable()));
    }
}
