namespace SMath.Statistics
{
    using Xunit;

    public class StandardDeviationTest
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(1, 1)]
        [InlineData(3.5, 1.8708287)]
        public void FromVariance(double variance, double expected)
        {
            Assert.Equal(expected, StandardDeviation.FromVariance(variance), 6);
        }
    }
}