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
            if (aSequence.Count() != bSequence.Count())
                throw new ArgumentException("Inconsistent length of sequences.");

            return Evaluate(aSequence, bSequence);
        }

        public static double Eval<N>(ICollection<N> aSequence, ICollection<N> bSequence)
            where N : INumberBase<N>
        {
            if (aSequence.Count != bSequence.Count)
                throw new ArgumentException("Inconsistent length of sequences.");

            return Evaluate(aSequence, bSequence);
        }

        internal static double Evaluate<N>(IEnumerable<N> aSequence, IEnumerable<N> bSequence)
            where N : INumberBase<N>
        {
            return Covariance.Evaluate(aSequence, bSequence, out _)
                / (StandardDeviation.FromVariance(Variance.Sample.Eval(aSequence))
                * StandardDeviation.FromVariance(Variance.Sample.Eval(bSequence)));
        }

        public static double Eval<N>(ReadOnlySpan<N> aSequence, ReadOnlySpan<N> bSequence)
            where N : INumberBase<N>
        {
            if (aSequence.Length != bSequence.Length)
                throw new ArgumentException("Inconsistent length of sequences.");

            return Covariance.Eval(aSequence, bSequence)
                / (StandardDeviation.FromVariance(Variance.Sample.Eval(aSequence))
                * StandardDeviation.FromVariance(Variance.Sample.Eval(bSequence)));
        }

        /// <summary>
        /// Pearson product-moment correlation coefficient.
        /// </summary>
        public static double EvalPerf<N, NInt>(IList<N> aSequence, IList<N> bSequence, NInt lag)
             where N : INumberBase<N>
             where NInt : IBinaryInteger<NInt>
        {
            if (aSequence.Count != bSequence.Count)
                throw new ArgumentException("Inconsistent length of lists.");

            SumXYX2Y2XY(aSequence, bSequence, out N sumX, out N sumY, out N sumX2, out N sumY2, out N sumXY, lag);
            var n = double.CreateChecked(aSequence.Count);

            // replace by call to std dev?
            var aStDev = double.Sqrt(double.CreateChecked(sumX2) / n - double.CreateChecked(sumX * sumX) / n / n);
            var bStDev = double.Sqrt(double.CreateChecked(sumY2) / n - double.CreateChecked(sumY * sumY) / n / n);
            var covariance = (double.CreateChecked(sumXY) / n - double.CreateChecked(sumX * sumY) / n / n);
            if (covariance == 0)
                return 0;

            var denominator = aStDev * bStDev;
            return denominator != 0 ? covariance / denominator : 1;
        }

        /// <summary>
        /// Pearson cross-correlation.
        /// </summary>
        public static class Cross
        {
            public static IEnumerable<(NInt Lag, double Coef)> Eval<N, NInt>(IList<N> aSequence, IList<N> bSequence, IEnumerable<NInt> lags)
                where N : INumberBase<N>
                where NInt : IBinaryInteger<NInt>
            {
                foreach (var lag in lags)
                {
                    if (lag >= NInt.Zero)
                        yield return (
                            lag,
                            PearsonCorrelation.Eval(
                                aSequence.Take(aSequence.Count - int.CreateChecked(lag)),
                                bSequence.Skip(int.CreateChecked(lag))));
                    else
                        yield return (
                            lag,
                            PearsonCorrelation.Eval(
                                aSequence.Skip(int.Abs(int.CreateChecked(lag))),
                                bSequence.Take(bSequence.Count - int.Abs(int.CreateChecked(lag)))));
                }
            }

            public static IEnumerable<(NInt Lag, double Coef)> EvalPerf<N, NInt>(IList<N> aSequence, IList<N> bSequence, IEnumerable<NInt> lags)
                where N : INumberBase<N>
                where NInt : IBinaryInteger<NInt>
            {
                foreach (var lag in lags)
                    yield return (lag, PearsonCorrelation.EvalPerf(aSequence, bSequence, lag));
            }
        }

        /// <summary>
        /// Pearson auto-correlation.
        /// </summary>
        public static class Auto
        {
            public static IEnumerable<(NInt Lag, double Coef)> Eval<N, NInt>(IList<N> sequence, IEnumerable<NInt> lags)
                where N : INumberBase<N>
                where NInt : IBinaryInteger<NInt>
            {
                foreach (var lag in lags)
                {
                    if (lag >= NInt.Zero)
                        yield return (
                            lag,
                            PearsonCorrelation.Eval(
                                sequence.Take(sequence.Count - int.CreateChecked(lag)),
                                sequence.Skip(int.CreateChecked(lag))));
                    else
                        yield return (
                            lag,
                            PearsonCorrelation.Eval(
                                sequence.Skip(int.Abs(int.CreateChecked(lag))),
                                sequence.Take(sequence.Count - int.Abs(int.CreateChecked(lag)))));
                }
            }

            public static IEnumerable<(NInt Lag, double Coef)> EvalPerf<N, NInt>(IList<N> sequence, IEnumerable<NInt> lags)
                where N : INumberBase<N>
                where NInt : IBinaryInteger<NInt>
            {
                foreach (var lag in lags)
                    yield return (lag, PearsonCorrelation.EvalPerf(sequence, sequence, lag));
            }
        }

        /// <summary>
        /// Weighted pearson correlation coefficient.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Pearson_correlation_coefficient">wikipedia</a>
        /// </remarks>
        public static class Weighted
        {
        }

        //http://www.statisticshowto.com/probability-and-statistics/correlation-coefficient-formula/

        internal static void SumXYX2Y2XY<N, NInt>(IList<N> numbers1, IList<N> numbers2, out N sumX, out N sumY, out N sumX2, out N sumY2, out N sumXY, NInt lag)
            where N : INumberBase<N>
            where NInt : IBinaryInteger<NInt>
        {
            sumX = N.Zero;
            sumX2 = N.Zero;
            sumY = N.Zero;
            sumY2 = N.Zero;
            sumXY = N.Zero;

            for (int i = 0; i < numbers1.Count; ++i)
            {
                N x = numbers1[i];
                var iy = i + int.CreateChecked(lag);
                var y = N.Zero;
                if (lag < NInt.Zero)
                    y = iy < 0
                        ? numbers2[0]
                        : iy < numbers2.Count - iy
                            ? numbers2[iy]
                            : numbers2[numbers2.Count - 1];
                else
                    y = iy < numbers2.Count
                        ? numbers2[iy]
                        : numbers2[numbers2.Count - 1];

                sumX += x;
                sumX2 += x * x;
                sumY += y;
                sumY2 += y * y;
                sumXY += x * y;
            }
        }

        /// <summary>
        /// Pearson product-moment correlation coefficient.
        /// </summary>
        public static double EvalPerf<N, NInt>(IList<N> aSequence, IList<N> bSequence, NInt lag)
             where N : INumberBase<N>
             where NInt : IBinaryInteger<NInt>
        {
            if (aSequence.Count != bSequence.Count)
                throw new ArgumentException("Inconsistent length of lists.");

            SumXYX2Y2XY(aSequence, bSequence, out N sumX, out N sumY, out N sumX2, out N sumY2, out N sumXY, lag);
            var n = double.CreateChecked(aSequence.Count);

            // replace by call to std dev?
            var aStDev = double.Sqrt(double.CreateChecked(sumX2) / n - double.CreateChecked(sumX * sumX) / n / n);
            var bStDev = double.Sqrt(double.CreateChecked(sumY2) / n - double.CreateChecked(sumY * sumY) / n / n);
            var covariance = (double.CreateChecked(sumXY) / n - double.CreateChecked(sumX * sumY) / n / n);
            if (covariance == 0)
                return 0;

            var denominator = aStDev * bStDev;
            return denominator != 0 ? covariance / denominator : 1;
        }



        //http://de.mathworks.com/help/signal/ref/xcorr.html
        //http://www.exceluser.com/excel_dashboards/cross-correlations-1.htm


        //http://stackoverflow.com/questions/17447817/correlation-of-two-arrays-in-b-sharp
        //http://proinvestory.cz/korelace-a-jeji-vyznam
        //public static double Eval<N>(IEnumerable<N> aSequence, IEnumerable<N> bSequence)
        //    where N : INumberBase<N>
        //{
        //    int i = 0;
        //    double r = 0d;

        //    double meanA = 0;
        //    double meanB = 0;
        //    double varA = 0;
        //    double varB = 0;

        //    //do not optimize

        //    using (IEnumerator<N> aEnumerator = aSequence.GetEnumerator())
        //    using (IEnumerator<N> bEnumerator = bSequence.GetEnumerator())
        //    {
        //        while (aEnumerator.MoveNext())
        //        {
        //            if (!bEnumerator.MoveNext())
        //            {
        //                throw new ArgumentOutOfRangeException(nameof(bEnumerator));
        //            }

        //            var a = aEnumerator.Current;
        //            var b = bEnumerator.Current;

        //            var deltaA = double.CreateChecked(a) - meanA;
        //            var scaleDeltaA = deltaA / ++i;

        //            var deltaB = double.CreateChecked(b) - meanB;
        //            var scaleDeltaB = deltaB / i;

        //            meanA += scaleDeltaA;
        //            meanB += scaleDeltaB;

        //            varA += scaleDeltaA * deltaA * (i - 1);
        //            varB += scaleDeltaB * deltaB * (i - 1);
        //            r += (deltaA * deltaB * (i - 1)) / i;
        //        }

        //        //if (ieB.MoveNext())
        //        //{
        //        //    throw new ArgumentOutOfRangeException(nameof(dataA), Resources.ArgumentArraysSameLength);
        //        //}
        //    }

        //    var aux = double.Sqrt(varA * varB);
        //    return aux != 0
        //        ? r / aux
        //        : 0;
        //}


        //vyborny zdroj:
        //https://github.com/mathnet/mathnet-numerics/blob/4292d6c653aab8b411c60c9579b9bc93f3b9d8e9/src/Numerics/Statistics/StreamingStatistics.cs
        //https://github.com/mathnet/mathnet-numerics/blob/4292d6c653aab8b411c60c9579b9bc93f3b9d8e9/src/Numerics/Statistics/Correlation.cs



        /// <summary>
        /// Pearson cross-correlation.
        /// </summary>
        public static class Cross
        {
            public static IEnumerable<(NInt Lag, double Coef)> Eval<N, NInt>(IList<N> aSequence, IList<N> bSequence, IEnumerable<NInt> lags)
                where N : INumberBase<N>
                where NInt : IBinaryInteger<NInt>
            {
                foreach (var lag in lags)
                {
                    if (lag >= NInt.Zero)
                        yield return (
                            lag,
                            PearsonCorrelation.Eval(
                                aSequence.Take(aSequence.Count - int.CreateChecked(lag)),
                                bSequence.Skip(int.CreateChecked(lag))));
                    else
                        yield return (
                            lag,
                            PearsonCorrelation.Eval(
                                aSequence.Skip(int.Abs(int.CreateChecked(lag))),
                                bSequence.Take(bSequence.Count - int.Abs(int.CreateChecked(lag)))));
                }
            }

            public static IEnumerable<(NInt Lag, double Coef)> EvalPerf<N, NInt>(IList<N> aSequence, IList<N> bSequence, IEnumerable<NInt> lags)
                where N : INumberBase<N>
                where NInt : IBinaryInteger<NInt>
            {
                foreach (var lag in lags)
                    yield return (lag, PearsonCorrelation.EvalPerf(aSequence, bSequence, lag));
            }
        }

        /// <summary>
        /// Pearson auto-correlation.
        /// </summary>
        public static class Auto
        {
            public static IEnumerable<(NInt Lag, double Coef)> Eval<N, NInt>(IList<N> sequence, IEnumerable<NInt> lags)
                where N : INumberBase<N>
                where NInt : IBinaryInteger<NInt>
            {
                foreach (var lag in lags)
                {
                    if (lag >= NInt.Zero)
                        yield return (
                            lag,
                            PearsonCorrelation.Eval(
                                sequence.Take(sequence.Count - int.CreateChecked(lag)),
                                sequence.Skip(int.CreateChecked(lag))));
                    else
                        yield return (
                            lag,
                            PearsonCorrelation.Eval(
                                sequence.Skip(int.Abs(int.CreateChecked(lag))),
                                sequence.Take(sequence.Count - int.Abs(int.CreateChecked(lag)))));
                }
            }

            public static IEnumerable<(NInt Lag, double Coef)> EvalPerf<N, NInt>(IList<N> sequence, IEnumerable<NInt> lags)
                where N : INumberBase<N>
                where NInt : IBinaryInteger<NInt>
            {
                foreach (var lag in lags)
                    yield return (lag, PearsonCorrelation.EvalPerf(sequence, sequence, lag));
            }
        }

        /// <summary>
        /// Weighted pearson correlation coefficient.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Pearson_correlation_coefficient">wikipedia</a>
        /// </remarks>
        public static class Weighted
        {
        }

        //http://www.statisticshowto.com/probability-and-statistics/correlation-coefficient-formula/

        internal static void SumXYX2Y2XY<N, NInt>(IList<N> numbers1, IList<N> numbers2, out N sumX, out N sumY, out N sumX2, out N sumY2, out N sumXY, NInt lag)
            where N : INumberBase<N>
            where NInt : IBinaryInteger<NInt>
        {
            sumX = N.Zero;
            sumX2 = N.Zero;
            sumY = N.Zero;
            sumY2 = N.Zero;
            sumXY = N.Zero;

            for (int i = 0; i < numbers1.Count; ++i)
            {
                N x = numbers1[i];
                var iy = i + int.CreateChecked(lag);
                var y = N.Zero;
                if (lag < NInt.Zero)
                    y = iy < 0
                        ? numbers2[0]
                        : iy < numbers2.Count - iy
                            ? numbers2[iy]
                            : numbers2[numbers2.Count - 1];
                else
                    y = iy < numbers2.Count
                        ? numbers2[iy]
                        : numbers2[numbers2.Count - 1];

                sumX += x;
                sumX2 += x * x;
                sumY += y;
                sumY2 += y * y;
                sumXY += x * y;
            }
        }
    }


}
