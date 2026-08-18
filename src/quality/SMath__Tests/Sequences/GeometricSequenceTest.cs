using Xunit;

namespace SMath.Sequences;

public class GeometricSequenceTest
{
    [Fact]
    public void Terms()
    {
        Assert.Equal(new double[] { 1, 1, 1 }, GeometricSequence.Terms(1d, 1d, 3u).ToArray());
        Assert.Equal(new double[] { 1, 2, 4 }, GeometricSequence.Terms(1d, 2d, 3u).ToArray());
        Assert.Equal(new double[] { 8, 4, 2, 1, 0.5 }, GeometricSequence.Terms(8d, 0.5d, 5u).ToArray());
        Assert.Equal(new double[] { -3, 6, -12 }, GeometricSequence.Terms(-3d, -2d, 3u).ToArray());
    }
}
