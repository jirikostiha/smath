using Xunit;

namespace SMath.Sequences;

public class FibonacciSequenceTest
{
    [Fact]
    public void Terms()
    {
        Assert.Equal(new uint[] { 0 }, FibonacciSequence.Terms(1u).ToArray());
        Assert.Equal(new uint[] { 0, 1 }, FibonacciSequence.Terms(2u).ToArray());
        Assert.Equal(new uint[] { 0, 1, 1, 2, 3, 5 }, FibonacciSequence.Terms(6u).ToArray());
    }
}
