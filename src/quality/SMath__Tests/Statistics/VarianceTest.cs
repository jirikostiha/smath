namespace SMath.Statistics
{
    using System;
    using Xunit;

    public class VarianceTest
    {
        [Theory]
        [InlineData(new double[] { 0, 0 }, 0)]
        [InlineData(new double[] { 1, 1 }, 0)]
        [InlineData(new double[] { 2, 2 }, 0)]
        [InlineData(new double[] { 0, 1, 2 }, 1)]
        [InlineData(new double[] { 0, -1, -2 }, 1)]
        [InlineData(new double[] { 1, 2, 3, 4, 5, 6 }, 3.5)]
        public void SampleEval(double[] sequence, double expected)
        {
            var variance = Variance.Sample.Eval(sequence);
            Assert.Equal(expected, variance, 6);
        }

        [Fact]
        public void SampleEval_EmptySequence_NoException()
        {
            Variance.Sample.Eval(Array.Empty<int>());
        }

        [Theory]
        [InlineData(new double[] { 0, 0 }, 0)]
        [InlineData(new double[] { 1, 1 }, 0)]
        [InlineData(new double[] { 2, 2 }, 0)]
        [InlineData(new double[] { 0, 1, 2 }, 0.66666667)]
        [InlineData(new double[] { 0, -1, -2 }, 0.66666667)]
        [InlineData(new double[] { 1, 2, 3, 4, 5, 6 }, 2.9166667)]
        public void PoplulationEval(double[] sequence, double expected)
        {
            var variance = Variance.Population.Eval(sequence);
            Assert.Equal(expected, variance, 6);
        }

        [Fact]
        public void PopulationEval_EmptySequence_NoException()
        {
            Variance.Population.Eval(Array.Empty<int>());
        }
    }
}