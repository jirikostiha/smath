namespace SMath.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Numerics;

    /// <summary>
    /// moment ?? ma oznaceni?
    /// </summary>
    public static class Moment
    {
        public static double Eval<N>(IEnumerable<N> sequence, int degree)
            where N : INumberBase<N>
        {
            //https://code.msdn.microsoft.com/Basic-C-Statistics-Library-26ac5403/sourcecode?fileId=145263&pathId=196059644

            var mean = ArithmeticMean.Eval(sequence);
            var sum = Summation.Eval(sequence.Select(n => double.Pow(double.CreateChecked(n) - mean, double.CreateChecked(degree))), out int count);

            return sum / double.CreateChecked(count);
        }
    }

    /// <summary>
    /// Standardized moment.
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Standardized_moment">wikipedia</a>
    /// </remarks>
    public static class StandardizedMoment
    {
        public static N Eval<N>(IEnumerable<N> sequence, int degree)
            where N : INumberBase<N>
        {
            switch (degree)
            {
                case 1:
                    return N.Zero;
                case 2:
                    return N.One;
                //case 3:
                    //return Skewness.f(sequence); //?
                //case 4:
                    //return Kurtosis.f(sequence); //?
                    //case 5:
                    //  return Hyperskewness.f(sequence);
                    //case 6:
                    //  return Hypertailedness.f(sequence);
            }

            throw new ArgumentOutOfRangeException(nameof(degree));
        }
    }
}
