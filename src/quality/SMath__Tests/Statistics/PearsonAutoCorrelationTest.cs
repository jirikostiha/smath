namespace SMath.Statistics
{
    using System;
    using System.Linq;
    using Xunit;

    public class PearsonAutoCorrelationTest
    {
        [Fact]
        public void AutoCorrelation()
        {
            var aSequence = new[] { 1, 2, 3, 4, 0 };
            var lags = new[] { 0, 1, 2, -1, -2 };

            var correlations = PearsonCorrelation.Auto.Eval(aSequence, lags)
                .ToArray();

            Assert.NotNull(correlations);
            Assert.Equal(5, correlations.Length);
            Assert.Equal(1, correlations.ElementAt(0).Coef, 4);
            Assert.Equal(-0.378, correlations.ElementAt(1).Coef, 4);
            Assert.Equal(-0.7206, correlations.ElementAt(2).Coef, 4);
            Assert.Equal(-0.378, correlations.ElementAt(3).Coef, 4);
            Assert.Equal(-0.7206, correlations.ElementAt(4).Coef, 4);
        }
    }
}