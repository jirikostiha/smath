namespace SMath.Statistics
{
    using System;
    using System.Linq;
    using Xunit;

    public class PearsonCrossCorrelationTest
    {
        [Fact]
        public void CrossCorrelation()
        {
            var aSequence = new[] { 1, 2, 3, 4, 5 };
            var bSequence = new[] { 1, 2, 1, 2, 3 };
            var lags = new[] { 0, 1, -1 };

            var correlations = PearsonCorrelation.Cross.Eval(aSequence, bSequence, lags)
                .ToArray();

            Assert.NotNull(correlations);
            Assert.Equal(3, correlations.Length);
            Assert.Equal(0.7559, correlations.ElementAt(0).Coef, 4);
            Assert.Equal(0.6325, correlations.ElementAt(1).Coef, 4);
            Assert.Equal(0.4472, correlations.ElementAt(2).Coef, 4);
        }
    }
}