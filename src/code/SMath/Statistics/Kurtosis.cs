using System.Numerics;

namespace SMath.Statistics
{
    /// <summary>
    /// Kurtosis.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Kurtosis">wikipedia</a>
    /// </remarks>
    public static class Kurtosis
    {
        public static double Eval<N>(IEnumerable<N> numbers)
            where N : IPowerFunctions<N>
        {
             var m2 = Moment.Eval(numbers, 2);
             var m4 = Moment.Eval(numbers, 4);

             return m4 / (m2 * m2);
        }
    }

    /// <summary>
    /// Excess kurtosis
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Kurtosis#Excess_kurtosis">wikipedia</a>
    /// </remarks>
    public static class ExcessKurtosis
    {
        public static double Eval<N>(IEnumerable<N> sequence)
            where N : IPowerFunctions<N>
            => Kurtosis.Eval(sequence) - 3d;
    }
}
