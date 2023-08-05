namespace SMath.Statistics
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Geometric mean
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Geometric_mean">wikipedia</a>
    /// </remarks>
    public static class GeometricMean
    {
        public static double f(IEnumerable<double> sequence)
        {
            var product = Product.Eval(sequence, out long count);

            return Math.Pow(product, 1 / (double)count);
        }
    }
}
