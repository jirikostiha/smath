using System;
using System.Linq;
using Xunit;

namespace SMath.FunctionsN;

public class RosenbrockFunctionTests
{
    private static readonly double[] Ones2 = [1d, 1d];
    private static readonly double[] Origin2 = [0d, 0d];
    private static readonly double[] Twos2 = [2d, 2d];
    private static readonly double[] Ones3 = [1d, 1d, 1d];
    private static readonly double[] Ramp3 = [0d, 1d, 2d];

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("sum((a - xi)^2 + b*(xi+1 - xi^2)^2)", RosenbrockFunction.PlainTextFormula);
    }

    [Fact]
    public void DefaultParameters()
    {
        Assert.Equal(1d, RosenbrockFunction.DefaultA<double>());
        Assert.Equal(100d, RosenbrockFunction.DefaultB<double>());
    }

    [Theory]
    [InlineData(1d, 1d, 0d)]
    [InlineData(0d, 0d, 1d)]
    [InlineData(2d, 2d, 401d)]
    public void Eval_TwoDimensions(double x1, double x2, double expected)
    {
        Assert.Equal(expected, RosenbrockFunction.Eval(x1, x2), 6);
    }

    [Fact]
    public void Eval_AtGlobalMinimumPoint_IsGlobalMinimum()
    {
        Assert.Equal(
            RosenbrockFunction.GlobalMinimum<double>(),
            RosenbrockFunction.Eval(new ReadOnlySpan<double>(Ones2)),
            6);
        Assert.Equal(
            RosenbrockFunction.GlobalMinimum<double>(),
            RosenbrockFunction.Eval(new ReadOnlySpan<double>(Ones3)),
            6);
    }

    [Fact]
    public void Eval_Span()
    {
        Assert.Equal(1d, RosenbrockFunction.Eval(new ReadOnlySpan<double>(Origin2)), 6);
        Assert.Equal(401d, RosenbrockFunction.Eval(new ReadOnlySpan<double>(Twos2)), 6);
        Assert.Equal(201d, RosenbrockFunction.Eval(new ReadOnlySpan<double>(Ramp3)), 6);
    }

    [Fact]
    public void Eval_SpanMatchesEnumerable()
    {
        Assert.Equal(
            RosenbrockFunction.Eval(Ramp3.AsEnumerable()),
            RosenbrockFunction.Eval(new ReadOnlySpan<double>(Ramp3)),
            9);
    }

    [Fact]
    public void Eval_SinglePoint_IsZero()
    {
        Assert.Equal(0d, RosenbrockFunction.Eval<double>([5d]), 6);
        Assert.Equal(0d, RosenbrockFunction.Eval<double>([]), 6);
    }

    [Fact]
    public void Eval_IsNotBelowGlobalMinimum()
    {
        for (var x = -2d; x <= 2d; x += 0.25d)
        {
            for (var y = -2d; y <= 2d; y += 0.25d)
                Assert.True(RosenbrockFunction.Eval(x, y) >= 0d);
        }
    }
}
