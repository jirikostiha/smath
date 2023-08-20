using System.Numerics;

namespace SMath.Statistics
{
    /// <summary>
    /// Variance.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Variance">Wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/Variance.html">Wolfram Mathworld</a>
    /// </remarks>
    public static class Variance
    {
        /// <summary>
        /// Sample variance.
        /// </summary>
        public static class Sample
        {
            public static double Eval<N>(IEnumerable<N> sequence)
                where N : INumberBase<N>
            {
                var mean = ArithmeticMean.Eval(sequence);
                var sum = Summation.Eval(
                    sequence.Select(n => (double.CreateChecked(n) - mean) * (double.CreateChecked(n) - mean)),
                    out int count);

                return sum / (double.CreateChecked(count) - 1d);
            }
        }

        /// <summary>
        /// Population variance.
        /// </summary>
        public static class Population
        {
            public static double Eval<N>(IEnumerable<N> sequence)
                where N : INumberBase<N>
            {
                var mean = ArithmeticMean.Eval(sequence);
                var sum = Summation.Eval(
                    sequence.Select(n => (double.CreateChecked(n) - mean) * (double.CreateChecked(n) - mean)),
                    out int count);

                return sum / double.CreateChecked(count);
            }
        }
    }
}
