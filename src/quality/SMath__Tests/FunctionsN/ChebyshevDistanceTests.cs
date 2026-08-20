using System;
using System.Linq;
using SMath.Geometry2D;
using Xunit;

namespace SMath.FunctionsN;

public class ChebyshevDistanceTests
{
    private static readonly double[] Point = [3d, -4d];
    private static readonly double[] PointA = [1d, 2d];
    private static readonly double[] PointB = [4d, 6d];
    private static readonly double[] Shorter = [1d];

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("max(|xi|)", ChebyshevDistance.PlainTextFormula);
    }

    [Fact]
    public void Eval_FromOrigin_Span()
    {
        Assert.Equal(4d, ChebyshevDistance.Eval(new ReadOnlySpan<double>(Point)));
        Assert.Equal(3d, ChebyshevDistance.Eval<double>([1d, -2d, 3d]));
        Assert.Equal(0d, ChebyshevDistance.Eval<double>([]));
    }

    [Fact]
    public void Eval_FromOrigin_Int()
    {
        Assert.Equal(4, ChebyshevDistance.Eval<int>([3, -4]));
    }

    [Fact]
    public void Eval_FromOrigin_SpanMatchesEnumerable()
    {
        Assert.Equal(
            ChebyshevDistance.Eval(Point.AsEnumerable()),
            ChebyshevDistance.Eval(new ReadOnlySpan<double>(Point)));
    }

    [Fact]
    public void Eval_TwoPoints_Span()
    {
        Assert.Equal(4d, ChebyshevDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(PointB)));
    }

    [Fact]
    public void Eval_TwoPoints_SpanMatchesEnumerable()
    {
        Assert.Equal(
            ChebyshevDistance.Eval(PointA.AsEnumerable(), PointB.AsEnumerable()),
            ChebyshevDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(PointB)));
    }

    [Fact]
    public void Eval_TwoPoints_MatchesPoint2ChebyshevDistance()
    {
        Assert.Equal(
            Point2.ChebyshevDistance((1d, 2d), (4d, 6d)),
            ChebyshevDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(PointB)));
    }

    [Fact]
    public void Eval_InconsistentLengthThrows()
    {
        Assert.Throws<ArgumentException>(() =>
            ChebyshevDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(Shorter)));
        Assert.Throws<ArgumentException>(() =>
            ChebyshevDistance.Eval(PointA.AsEnumerable(), Shorter.AsEnumerable()));
    }
}
