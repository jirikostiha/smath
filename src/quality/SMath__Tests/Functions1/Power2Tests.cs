using Xunit;

namespace SMath.Functions1;

public class Power2Tests
{
    [Fact]
    public void NumberDomain_IsFullRange()
    {
        var domain = Power2.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }

    [Fact]
    public void IsEven()
    {
        Assert.True(Power2.IsEven);
        Assert.False(Power2.IsOdd);
    }
}
