using Xunit;

namespace SMath.Functions1;

public class SineTests
{
    [Fact]
    public void IsOdd_NotEven()
    {
        Assert.True(Sine.IsOdd);
        Assert.False(Sine.IsEven);
    }

    [Fact]
    public void Image_IsMinusOneToOne()
    {
        var image = Sine.Image<double>();
        Assert.Equal(-1d, image.Min, 6);
        Assert.Equal(1d, image.Max, 6);
    }

    [Fact]
    public void NumberImage_IsMinusOneToOne()
    {
        var image = Sine.NumberImage<double>();
        Assert.Equal(-1d, image.Min, 6);
        Assert.Equal(1d, image.Max, 6);
    }

    [Fact]
    public void NumberDomain_IsFullRange()
    {
        var domain = Sine.NumberDomain<double>();
        Assert.Equal(double.MinValue, domain.Min);
        Assert.Equal(double.MaxValue, domain.Max);
    }
}
