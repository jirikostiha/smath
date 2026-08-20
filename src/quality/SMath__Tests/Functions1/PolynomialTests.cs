using System;
using System.Linq;
using Xunit;

namespace SMath.Functions1;

public class PolynomialTests
{
    private static readonly double[] Quadratic = [1d, -3d, 2d];

    [Fact]
    public void PlainTextFormula()
    {
        Assert.Equal("c0 + c1*x + c2*x^2 + ... + cn*x^n", Polynomial.PlainTextFormula);
    }

    [Theory]
    [InlineData(0d, 1d)]
    [InlineData(1d, 0d)]
    [InlineData(2d, 3d)]
    [InlineData(-1d, 6d)]
    public void Eval_Span(double x, double expected)
    {
        Assert.Equal(expected, Polynomial.Eval(x, new ReadOnlySpan<double>(Quadratic)), 6);
    }

    [Theory]
    [InlineData(0d, 1d)]
    [InlineData(1d, 0d)]
    [InlineData(2d, 3d)]
    [InlineData(-1d, 6d)]
    public void Eval_Enumerable(double x, double expected)
    {
        Assert.Equal(expected, Polynomial.Eval(x, Quadratic.AsEnumerable()), 6);
    }

    [Fact]
    public void Eval_NoCoefficients_IsZero()
    {
        Assert.Equal(0d, Polynomial.Eval<double>(3d, []));
    }

    [Fact]
    public void Eval_SingleCoefficient_IsConstant()
    {
        Assert.Equal(7d, Polynomial.Eval<double>(3d, [7d]));
    }

    [Theory]
    [InlineData(0d, -3d)]
    [InlineData(2d, 5d)]
    [InlineData(-1d, -7d)]
    public void DerivativeEval_Span(double x, double expected)
    {
        Assert.Equal(expected, Polynomial.DerivativeEval(x, new ReadOnlySpan<double>(Quadratic)), 6);
    }

    [Theory]
    [InlineData(0d, -3d)]
    [InlineData(2d, 5d)]
    [InlineData(-1d, -7d)]
    public void DerivativeEval_Enumerable(double x, double expected)
    {
        Assert.Equal(expected, Polynomial.DerivativeEval(x, Quadratic.AsEnumerable()), 6);
    }

    [Fact]
    public void Degree()
    {
        Assert.Equal(2, Polynomial.Degree(new ReadOnlySpan<double>(Quadratic)));
        Assert.Equal(1, Polynomial.Degree<double>([1d, -3d, 0d]));
        Assert.Equal(-1, Polynomial.Degree<double>([0d, 0d]));
        Assert.Equal(-1, Polynomial.Degree<double>([]));
    }

    [Fact]
    public void DerivativeCoefficients()
    {
        Assert.Equal(new[] { -3d, 4d }, Polynomial.DerivativeCoefficients(new ReadOnlySpan<double>(Quadratic)));
        Assert.Empty(Polynomial.DerivativeCoefficients<double>([5d]));
    }

    [Fact]
    public void Eval_MatchesPower2()
    {
        foreach (var x in new[] { -2.5d, 0d, 1.4d })
            Assert.Equal(Power2.Eval(x), Polynomial.Eval<double>(x, [0d, 0d, 1d]), 6);
    }
}
