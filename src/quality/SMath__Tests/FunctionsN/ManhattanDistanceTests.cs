using System;
using System.Linq;
using SMath.Geometry2D;
using Xunit;

namespace SMath.FunctionsN;

public class ManhattanDistanceTests
{
    private static readonly double[] Point = [3d, -4d];
    private static readonly double[] PointA = [1d, 2d];
    private static readonly double[] PointB = [4d, 6d];
    private static readonly double[] Shorter = [1d];

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("sum(|xi|)", ManhattanDistance.PlainTextFormula);
    }

    [Fact]
    public void Eval_FromOrigin_Span()
    {
        Assert.Equal(7d, ManhattanDistance.Eval(new ReadOnlySpan<double>(Point)));
        Assert.Equal(6d, ManhattanDistance.Eval<double>([1d, -2d, 3d]));
        Assert.Equal(0d, ManhattanDistance.Eval<double>([]));
    }

    [Fact]
    public void Eval_FromOrigin_Int()
    {
        Assert.Equal(7, ManhattanDistance.Eval<int>([3, -4]));
    }

    [Fact]
    public void Eval_FromOrigin_SpanMatchesEnumerable()
    {
        Assert.Equal(
            ManhattanDistance.Eval(Point.AsEnumerable()),
            ManhattanDistance.Eval(new ReadOnlySpan<double>(Point)));
    }

    [Fact]
    public void Eval_TwoPoints_Span()
    {
        Assert.Equal(7d, ManhattanDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(PointB)));
    }

    [Fact]
    public void Eval_TwoPoints_SpanMatchesEnumerable()
    {
        Assert.Equal(
            ManhattanDistance.Eval(PointA.AsEnumerable(), PointB.AsEnumerable()),
            ManhattanDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(PointB)));
    }

    [Fact]
    public void Eval_TwoPoints_MatchesPoint2ManhattanDistance()
    {
        Assert.Equal(
            Point2.ManhattanDistance((1d, 2d), (4d, 6d)),
            ManhattanDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(PointB)));
    }

    [Fact]
    public void Eval_InconsistentLengthThrows()
    {
        Assert.Throws<ArgumentException>(() =>
            ManhattanDistance.Eval(new ReadOnlySpan<double>(PointA), new ReadOnlySpan<double>(Shorter)));
        Assert.Throws<ArgumentException>(() =>
            ManhattanDistance.Eval(PointA.AsEnumerable(), Shorter.AsEnumerable()));
    }
}
