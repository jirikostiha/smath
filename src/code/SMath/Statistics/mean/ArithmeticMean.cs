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

        ////https://math.stackexchange.com/questions/106700/incremental-averageing
        //public static double f2(double newValue, double previousMean, double count)
        //{
        //    return previousMean + (newValue - previousMean) / count;
        //}

        //public static double f2(IEnumerable<double> numbers)
        //{
        //    long count = 0;
        //    double mean = 0;
        //    foreach (var value in numbers)
        //    {
        //        count++;
        //        mean = f2(value, mean, count);
        //    }

        //    return mean;
        //}

        //public static double f2(IList<double> sequence)
        //{
        //    double mean = 0;
        //    for (int i = 0; i < sequence.Count; i++)
        //        mean = f2(sequence[i], mean, i);

        //    return mean;
        //}
    }
}
