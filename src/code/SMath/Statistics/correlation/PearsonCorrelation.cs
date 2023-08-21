namespace SMath.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Numerics;

    /// <summary>
    /// Pearson correlation coefficient.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Pearson_correlation_coefficient">wikipedia</a>
    /// </remarks>
    public static class PearsonCorrelation
    {
        public static double Eval<N>(IEnumerable<N> aSequence, IEnumerable<N> bSequence)
             where N : INumberBase<N>
        {
            return Covariance.Eval(aSequence, bSequence)
                / (StandardDeviation.FromVariance(Variance.Sample.Eval(aSequence))
                * StandardDeviation.FromVariance(Variance.Sample.Eval(bSequence)));
        }
    }
}
