using System.Numerics;
using System.Runtime.CompilerServices;

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double PreEvaluate<N>(IEnumerable<N> sequence, out int count)
            where N : INumberBase<N>
        {
            var mean = ArithmeticMean.Eval(sequence);
            var sum = Summation.Eval(
                sequence.Select(n => (double.CreateChecked(n) - mean) * (double.CreateChecked(n) - mean)),
                out count);

            return sum;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static double PreEvaluate<N>(ReadOnlySpan<N> sequence)
            where N : INumberBase<N>
        {
            var mean = ArithmeticMean.Eval(sequence);
            double sum = 0;
            for (int i = 0; i < sequence.Length; i++)
                sum += (double.CreateChecked(sequence[i]) - mean) * (double.CreateChecked(sequence[i]) - mean);

            return sum;
        }

        /// <summary>
        /// Sample variance.
        /// </summary>
        public static class Sample
        {
            public static double Eval<N>(IEnumerable<N> sequence)
                where N : INumberBase<N>
                =>
                PreEvaluate(sequence, out int count) / (double.CreateChecked(count) - 1d);

            public static double Eval<N>(ReadOnlySpan<N> sequence)
                where N : INumberBase<N>
                =>
                PreEvaluate(sequence) / (double.CreateChecked(sequence.Length) - 1d);
        }

        /// <summary>
        /// Population variance.
        /// </summary>
        public static class Population
        {
            public static double Eval<N>(IEnumerable<N> sequence)
                where N : INumberBase<N>
                =>
                PreEvaluate(sequence, out int count) / double.CreateChecked(count);

            public static double Eval<N>(ReadOnlySpan<N> sequence)
                where N : INumberBase<N>
                =>
                PreEvaluate(sequence) / double.CreateChecked(sequence.Length);
        }
    }
}
