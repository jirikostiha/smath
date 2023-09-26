namespace SMath.Statistics
{
    using System.Collections.Generic;
    using System.Numerics;

    /// <summary>
    /// Arithmetic mean.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Arithmetic_mean">wikipedia</a>
    /// </remarks>
    public static class ArithmeticMean
    {
        public static double Eval<N>(IEnumerable<N> sequence)
            where N : INumberBase<N>
        {
            var sum = Summation.Eval(sequence, out long count);
            return double.CreateChecked(sum) / double.CreateChecked(count);
        }

        public static double Eval<N>(ReadOnlySpan<N> sequence)
            where N : INumberBase<N>
        {
            var sum = Summation.Eval(sequence, out long count);
            return double.CreateChecked(sum) / double.CreateChecked(count);
        }
    }
}
