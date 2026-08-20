using System;
using System.Linq;
using Xunit;

namespace SMath.FunctionsN;

public class RastriginFunctionTests
{
    private static readonly double[] Origin2 = [0d, 0d];
    private static readonly double[] Ones2 = [1d, 1d];
    private static readonly double[] Halves2 = [0.5d, 0.5d];
    private static readonly double[] Integers3 = [1d, 2d, 3d];

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("a*n + sum(xi^2 - a*cos(2*pi*xi))", RastriginFunction.PlainTextFormula);
    }

    [Fact]
    public void DefaultAmplitude_IsTen()
    {
        Assert.Equal(10d, RastriginFunction.DefaultAmplitude<double>());
    }

    [Fact]
    public void Eval_AtOrigin_IsGlobalMinimum()
    {
        Assert.Equal(
            RastriginFunction.GlobalMinimum<double>(),
            RastriginFunction.Eval(new ReadOnlySpan<double>(Origin2)),
            6);
    }

    [Fact]
    public void Eval_Span()
    {
        Assert.Equal(2d, RastriginFunction.Eval(new ReadOnlySpan<double>(Ones2)), 6);
        Assert.Equal(40.5d, RastriginFunction.Eval(new ReadOnlySpan<double>(Halves2)), 6);
        Assert.Equal(14d, RastriginFunction.Eval(new ReadOnlySpan<double>(Integers3)), 6);
    }

    [Fact]
    public void Eval_SpanMatchesEnumerable()
    {
        Assert.Equal(
            RastriginFunction.Eval(Integers3.AsEnumerable()),
            RastriginFunction.Eval(new ReadOnlySpan<double>(Integers3)),
            9);
    }

    [Fact]
    public void Eval_WithExplicitAmplitude()
    {
        Assert.Equal(
            RastriginFunction.Eval(new ReadOnlySpan<double>(Integers3)),
            RastriginFunction.Eval(new ReadOnlySpan<double>(Integers3), 10d),
            9);
    }

    [Fact]
    public void Eval_EmptyPoint_IsZero()
    {
        Assert.Equal(0d, RastriginFunction.Eval<double>([]), 6);
    }

    [Fact]
    public void Eval_IsNotBelowGlobalMinimum()
    {
        for (var x = -5d; x <= 5d; x += 0.25d)
            Assert.True(RastriginFunction.Eval<double>([x]) >= -1e-9d);
    }
}
