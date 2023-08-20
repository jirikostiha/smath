namespace SMath.Statistics
{
    using System;
    using Xunit;

    public class CovarianceTest
    {
        [Theory]
        [InlineData(new double[] { 0, 0 }, new double[] { 0, 0 }, 0)]
        [InlineData(new double[] { 0, 0 }, new double[] { 1, 1 }, 0)]
        [InlineData(new double[] { 1, 1 }, new double[] { 1, 1 }, 0)]
        [InlineData(new double[] { 0, 1, 2 }, new double[] { 0, 1, 2 }, 1)]
        [InlineData(new double[] { 0, 1, 2 }, new double[] { 0, -1, -2 }, -1)]
        public void Eval(double[] seqA, double[] seqB, int expected)
        {
            var covariance = Covariance.Eval(seqA, seqB);
            Assert.Equal(expected, covariance, 6);
        }

        [Fact]
        public void Eval_FirstSequenceIsEmpty_NoException()
        {
            Covariance.Eval(Array.Empty<int>(), new int[] { 1, 2 });
        }

        [Fact]
        public void Eval_SecondSequenceIsEmpty_NoException()
        {
            Covariance.Eval(new int[] { 1, 2 }, Array.Empty<int>());
        }

        [Fact]
        public void Eval_BothSequencesAreEmpty_NoException()
        {
            Covariance.Eval(Array.Empty<int>(), Array.Empty<int>());
        }
    }
}