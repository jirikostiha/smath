namespace SMath.Statistics;

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
         // the length check is done within the single pass of Covariance.Evaluate,
         // enumerating the sequences twice would re-evaluate lazy pipelines
         => Evaluate(aSequence, bSequence);

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

        return FromSums(sumX, sumY, sumX2, sumY2, sumXY, aSequence.Count);
    }

    /// <summary>
    /// Pearson product-moment correlation coefficient.
    /// </summary>
    public static double EvalPerf<N, NInt>(ReadOnlySpan<N> aSequence, ReadOnlySpan<N> bSequence, NInt lag)
         where N : INumberBase<N>
         where NInt : IBinaryInteger<NInt>
    {
        if (aSequence.Length != bSequence.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        SumXYX2Y2XY(aSequence, bSequence, out N sumX, out N sumY, out N sumX2, out N sumY2, out N sumXY, lag);

        return FromSums(sumX, sumY, sumX2, sumY2, sumXY, aSequence.Length);
    }

    private static double FromSums<N>(N sumX, N sumY, N sumX2, N sumY2, N sumXY, int length)
        where N : INumberBase<N>
    {
        var n = double.CreateChecked(length);

        // the sums are widened before multiplying, the product of two N sums can overflow N
        var x = double.CreateChecked(sumX);
        var y = double.CreateChecked(sumY);

        // replace by call to std dev?
        var aStDev = double.Sqrt(double.CreateChecked(sumX2) / n - x * x / n / n);
        var bStDev = double.Sqrt(double.CreateChecked(sumY2) / n - y * y / n / n);
        var covariance = double.CreateChecked(sumXY) / n - x * y / n / n;

        // a constant sequence has no deviation, the coefficient is undefined then
        var denominator = aStDev * bStDev;
        return denominator != 0 ? covariance / denominator : double.NaN;
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

        /// <summary>
        /// Allocation free cross-correlation. Coefficients are written into <paramref name="destination"/>
        /// in the order of <paramref name="lags"/>. Sub-sequences are sliced, not copied.
        /// </summary>
        /// <returns> Count of written coefficients. </returns>
        public static int Eval<N, NInt>(ReadOnlySpan<N> aSequence, ReadOnlySpan<N> bSequence,
            ReadOnlySpan<NInt> lags, Span<double> destination)
            where N : INumberBase<N>
            where NInt : IBinaryInteger<NInt>
        {
            if (aSequence.Length != bSequence.Length)
                throw new ArgumentException("Inconsistent length of sequences.");
            if (destination.Length < lags.Length)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            for (int i = 0; i < lags.Length; i++)
            {
                var lag = int.CreateChecked(lags[i]);
                destination[i] = lag >= 0
                    ? PearsonCorrelation.Eval(aSequence[..(aSequence.Length - lag)], bSequence[lag..])
                    : PearsonCorrelation.Eval(aSequence[int.Abs(lag)..], bSequence[..(bSequence.Length - int.Abs(lag))]);
            }

            return lags.Length;
        }

        /// <summary>
        /// Allocation free cross-correlation using the product-moment formula.
        /// </summary>
        /// <returns> Count of written coefficients. </returns>
        public static int EvalPerf<N, NInt>(ReadOnlySpan<N> aSequence, ReadOnlySpan<N> bSequence,
            ReadOnlySpan<NInt> lags, Span<double> destination)
            where N : INumberBase<N>
            where NInt : IBinaryInteger<NInt>
        {
            if (destination.Length < lags.Length)
                throw new ArgumentException("Destination is too short.", nameof(destination));

            for (int i = 0; i < lags.Length; i++)
                destination[i] = PearsonCorrelation.EvalPerf(aSequence, bSequence, lags[i]);

            return lags.Length;
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

        /// <summary>
        /// Allocation free auto-correlation. Coefficients are written into <paramref name="destination"/>
        /// in the order of <paramref name="lags"/>. Sub-sequences are sliced, not copied.
        /// </summary>
        /// <returns> Count of written coefficients. </returns>
        public static int Eval<N, NInt>(ReadOnlySpan<N> sequence, ReadOnlySpan<NInt> lags, Span<double> destination)
            where N : INumberBase<N>
            where NInt : IBinaryInteger<NInt>
            => Cross.Eval(sequence, sequence, lags, destination);

        /// <summary>
        /// Allocation free auto-correlation using the product-moment formula.
        /// </summary>
        /// <returns> Count of written coefficients. </returns>
        public static int EvalPerf<N, NInt>(ReadOnlySpan<N> sequence, ReadOnlySpan<NInt> lags, Span<double> destination)
            where N : INumberBase<N>
            where NInt : IBinaryInteger<NInt>
            => Cross.EvalPerf(sequence, sequence, lags, destination);
    }

    /// <summary>
    /// Weighted pearson correlation coefficient.
    /// </summary>
    /// <remarks>
    /// Each pair contributes in proportion to its weight, so a pair weighted 2 counts
    /// exactly as the same pair listed twice and a pair weighted 0 does not count at all.
    /// The weights cancel in the ratio, therefore they need not sum to one.
    /// <a href="https://en.wikipedia.org/wiki/Pearson_correlation_coefficient">wikipedia</a>
    /// </remarks>
    public static class Weighted
    {
        /// <summary>
        /// Weighted pearson correlation coefficient of two sequences.
        /// </summary>
        /// <returns>
        /// Coefficient in the range [-1, 1], or <see cref="double.NaN"/> when it cannot be decided,
        /// that is for an empty input, for weights summing to zero or for a constant sequence.
        /// </returns>
        /// <exception cref="ArgumentException"> Sequences are not of the same length. </exception>
        /// <exception cref="ArgumentOutOfRangeException"> A weight is negative. </exception>
        public static double Eval<N>(IEnumerable<N> aSequence, IEnumerable<N> bSequence, IEnumerable<N> weights)
            where N : INumberBase<N>
        {
            using var aEnumerator = aSequence.GetEnumerator();
            using var bEnumerator = bSequence.GetEnumerator();
            using var weightEnumerator = weights.GetEnumerator();

            var accumulator = default(Accumulator);

            while (true)
            {
                var aMoved = aEnumerator.MoveNext();
                var bMoved = bEnumerator.MoveNext();
                var weightMoved = weightEnumerator.MoveNext();

                // one sequence ran out sooner than the others
                if (aMoved != bMoved || aMoved != weightMoved)
                    throw new ArgumentException("Inconsistent length of sequences.");

                if (!aMoved)
                    break;

                var weight = double.CreateChecked(weightEnumerator.Current);
                if (weight < 0)
                    throw new ArgumentOutOfRangeException(nameof(weights), weight, "Weight cannot be negative.");

                accumulator.Add(
                    double.CreateChecked(aEnumerator.Current),
                    double.CreateChecked(bEnumerator.Current),
                    weight);
            }

            return accumulator.Coefficient;
        }

        /// <summary>
        /// Allocation free weighted pearson correlation coefficient of two sequences.
        /// </summary>
        /// <returns>
        /// Coefficient in the range [-1, 1], or <see cref="double.NaN"/> when it cannot be decided,
        /// that is for an empty input, for weights summing to zero or for a constant sequence.
        /// </returns>
        /// <exception cref="ArgumentException"> Sequences are not of the same length. </exception>
        /// <exception cref="ArgumentOutOfRangeException"> A weight is negative. </exception>
        public static double Eval<N>(ReadOnlySpan<N> aSequence, ReadOnlySpan<N> bSequence, ReadOnlySpan<N> weights)
            where N : INumberBase<N>
        {
            if (aSequence.Length != bSequence.Length || aSequence.Length != weights.Length)
                throw new ArgumentException("Inconsistent length of sequences.");

            var accumulator = default(Accumulator);

            for (int i = 0; i < aSequence.Length; i++)
            {
                var weight = double.CreateChecked(weights[i]);
                if (weight < 0)
                    throw new ArgumentOutOfRangeException(nameof(weights), weight, "Weight cannot be negative.");

                accumulator.Add(
                    double.CreateChecked(aSequence[i]),
                    double.CreateChecked(bSequence[i]),
                    weight);
            }

            return accumulator.Coefficient;
        }

        /// <summary>
        /// Running weighted co-moments of a pair of sequences, updated by West's incremental
        /// algorithm. Deviations are taken from the mean of the values seen so far instead of
        /// from raw power sums, which keeps the result accurate for values far from zero.
        /// </summary>
        private struct Accumulator
        {
            private double _weightSum;
            private double _meanA;
            private double _meanB;
            private double _momentA;  // sum of w * (a - meanA)^2
            private double _momentB;  // sum of w * (b - meanB)^2
            private double _coMoment;  // sum of w * (a - meanA) * (b - meanB)

            public void Add(double a, double b, double weight)
            {
                // a zero weighted pair contributes nothing, skipping it also keeps the running
                // means defined while the leading weights are all zero
                if (weight == 0)
                    return;

                _weightSum += weight;
                var ratio = weight / _weightSum;

                var deltaA = a - _meanA;
                var deltaB = b - _meanB;
                _meanA += ratio * deltaA;
                _meanB += ratio * deltaB;

                // the second factor is taken from the already updated mean, that is what makes
                // the update exact rather than an approximation of the two pass formula
                _momentA += weight * deltaA * (a - _meanA);
                _momentB += weight * deltaB * (b - _meanB);
                _coMoment += weight * deltaA * (b - _meanB);
            }

            // the common divisor by the sum of weights cancels out, so the moments are used as they are,
            // and the roots are taken apart to not overflow on their product
            public readonly double Coefficient
                => _coMoment / (double.Sqrt(_momentA) * double.Sqrt(_momentB));
        }
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
                    : iy < numbers2.Count
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

    internal static void SumXYX2Y2XY<N, NInt>(ReadOnlySpan<N> numbers1, ReadOnlySpan<N> numbers2,
        out N sumX, out N sumY, out N sumX2, out N sumY2, out N sumXY, NInt lag)
        where N : INumberBase<N>
        where NInt : IBinaryInteger<NInt>
    {
        sumX = N.Zero;
        sumX2 = N.Zero;
        sumY = N.Zero;
        sumY2 = N.Zero;
        sumXY = N.Zero;

        for (int i = 0; i < numbers1.Length; ++i)
        {
            N x = numbers1[i];
            var iy = i + int.CreateChecked(lag);
            var y = N.Zero;
            if (lag < NInt.Zero)
                y = iy < 0
                    ? numbers2[0]
                    : iy < numbers2.Length
                        ? numbers2[iy]
                        : numbers2[numbers2.Length - 1];
            else
                y = iy < numbers2.Length
                    ? numbers2[iy]
                    : numbers2[numbers2.Length - 1];

            sumX += x;
            sumX2 += x * x;
            sumY += y;
            sumY2 += y * y;
            sumXY += x * y;
        }
    }
}
