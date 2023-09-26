using System.Numerics;

namespace SMath.Statistics
{
    /// <summary>
    /// Standard deviation.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Standard_deviation">Wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/StandardDeviation.html">Wolfram Mathworld</a>
    /// </remarks>
    public static class StandardDeviation
    {
        public static double FromVariance<N>(N variance)
            where N : INumberBase<N>
            => double.Sqrt(double.CreateChecked(variance));
    }
}
