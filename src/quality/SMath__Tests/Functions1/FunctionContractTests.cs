using Xunit;

namespace SMath.Functions1;

/// <summary>
/// The domain and the image are reached here only through <see cref="ISingleDomain"/> and
/// <see cref="ISingleImage"/>, which is what the interfaces exist for. A function which
/// declares one of them but does not match its signature does not compile.
/// </summary>
public class FunctionContractTests
{
    private static (double Min, double Max) DomainOf<F>()
        where F : ISingleDomain
        => F.Domain<double>();

    private static (double Min, double Max) NumberDomainOf<F>()
        where F : ISingleDomain
        => F.NumberDomain<double>();

    private static (double Min, double Max) ImageOf<F>()
        where F : ISingleImage
        => F.Image<double>();

    private static (double Min, double Max) NumberImageOf<F>()
        where F : ISingleImage
        => F.NumberImage<double>();

    [Fact]
    public void DomainIsReachableThroughTheInterface()
    {
        Assert.Equal((double.NegativeInfinity, double.PositiveInfinity), DomainOf<Identity>());
        Assert.Equal((double.NegativeInfinity, double.PositiveInfinity), DomainOf<Sine>());
        Assert.Equal((0d, double.PositiveInfinity), DomainOf<Root2>());
        Assert.Equal((0d, double.PositiveInfinity), DomainOf<NaturalLogarithm>());

        // a function whose image depends on its parameters still has a domain
        Assert.Equal((double.NegativeInfinity, double.PositiveInfinity), DomainOf<GaussianFunction>());
        Assert.Equal((double.NegativeInfinity, double.PositiveInfinity), DomainOf<StepFunction>());
    }

    [Fact]
    public void NumberDomainIsReachableThroughTheInterface()
    {
        Assert.Equal((double.MinValue, double.MaxValue), NumberDomainOf<Identity>());
        Assert.Equal((0d, double.MaxValue), NumberDomainOf<Root2>());
    }

    [Fact]
    public void ImageIsReachableThroughTheInterface()
    {
        Assert.Equal((double.NegativeInfinity, double.PositiveInfinity), ImageOf<Identity>());
        Assert.Equal((-1d, 1d), ImageOf<Sine>());
        Assert.Equal((-1d, 1d), ImageOf<Cosine>());
        Assert.Equal((0d, double.PositiveInfinity), ImageOf<Power2>());
        Assert.Equal((0d, 1d), ImageOf<SigmoidFunction>());
    }

    [Fact]
    public void NumberImageIsReachableThroughTheInterface()
    {
        Assert.Equal((double.MinValue, double.MaxValue), NumberImageOf<Identity>());
        Assert.Equal((-1d, 1d), NumberImageOf<Sine>());
        Assert.Equal((0d, double.MaxValue), NumberImageOf<Power2>());
    }
}
