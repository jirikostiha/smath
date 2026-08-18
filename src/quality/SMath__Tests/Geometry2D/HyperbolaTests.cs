using SMath.Geometry2D;
using Xunit;

namespace SMath.Tests.Geometry2D;

public class HyperbolaTests
{
    [Fact]
    public void Eccentricity_FromRadius()
    {
        Assert.Equal(double.Sqrt(2), Hyperbola.Eccentricity.FromRadius(3.0, 3.0), 5);
    }
}
