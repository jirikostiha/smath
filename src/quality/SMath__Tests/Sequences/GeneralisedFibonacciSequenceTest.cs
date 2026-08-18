using Xunit;

namespace SMath.Sequences;

public class GeneralisedFibonacciSequenceTest
{
    [Fact]
    public void Terms()
    {
        Assert.Equal(new double[] { 0, 1, 1, 2, 3, 5 }, GeneralisedFibonacciSequence.Terms(0d, 1d, 6).ToArray());
        Assert.Equal(new double[] { -1, 2, 1, 3, 4 }, GeneralisedFibonacciSequence.Terms(-1d, 2d, 5).ToArray());
    }
}
