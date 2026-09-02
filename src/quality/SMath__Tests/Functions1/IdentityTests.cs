using Xunit;

namespace SMath.Functions1;

public class IdentityTests
{
    [Fact]
    public void NumberDomain_IsFullRange()
    {
        // Regression: NumberDomain used to return (MaxValue, MaxValue).
        // The identity function is defined for every number, so the domain
        // must span the whole numeric range.
        var domain = Identity.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void Domain_IsFullRange()
    {
        var domain = Identity.Domain<double>();
        Assert.Equal(double.NegativeInfinity, domain.Min);
        Assert.Equal(double.PositiveInfinity, domain.Max);
    }

    [Fact]
    public void NumberDomain_WorksForIntegerTypes()
    {
        // Regression: NumberDomain/NumberImage were constrained to IFloatingPointIeee754,
        // so the "any number type" overloads could not be used with integer types at all,
        // unlike the very same members on Power2 and Sine.
        var domain = Identity.NumberDomain<int>();
        Assert.Equal(int.MinValue, domain.Min);
        Assert.Equal(int.MaxValue, domain.Max);

        var image = Identity.NumberImage<int>();
        Assert.Equal(int.MinValue, image.Min);
        Assert.Equal(int.MaxValue, image.Max);
    }

    [Fact]
    public void NumberImage_IsFullRange()
    {
        var image = Identity.NumberImage<double>();
        Assert.Equal(double.MinValue, image.Min);
        Assert.Equal(double.MaxValue, image.Max);
    }

    [Theory]
    [InlineData(-3d)]
    [InlineData(0d)]
    [InlineData(2.5d)]
    public void Eval_ReturnsInput(double x)
    {
        Assert.Equal(x, Identity.Eval(x));
    }

    [Fact]
    public void IsOdd()
    {
        Assert.True(Identity.IsOdd);
        Assert.False(Identity.IsEven);
    }

    [Theory]
    [InlineData(-3d)]
    [InlineData(0d)]
    [InlineData(2.5d)]
    public void DerivativeEval(double x)
    {
        Assert.Equal(1d, Identity.DerivativeEval(x));
        Assert.Equal(1d, Identity.DerivativeEval<double>());
    }
}
