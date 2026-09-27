namespace SMath.Statistics;

using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;

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
         // the length check is done within the single pass of Evaluate,
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
        var accumulator = default(CoMomentAccumulator);

        using var aEnumerator = aSequence.GetEnumerator();
        using var bEnumerator = bSequence.GetEnumerator();

        while (true)
        {
            var aMoved = aEnumerator.MoveNext();
            var bMoved = bEnumerator.MoveNext();

            if (aMoved != bMoved)
                throw new ArgumentException("Inconsistent length of sequences.");

            if (!aMoved)
                break;

            accumulator.Add(
                double.CreateChecked(aEnumerator.Current),
                double.CreateChecked(bEnumerator.Current));
        }

        return accumulator.PearsonCoefficient;
    }

    public static double Eval<N>(ReadOnlySpan<N> aSequence, ReadOnlySpan<N> bSequence)
        where N : INumberBase<N>
    {
        if (aSequence.Length != bSequence.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        var accumulator = default(CoMomentAccumulator);
        for (int i = 0; i < aSequence.Length; i++)
            accumulator.Add(double.CreateChecked(aSequence[i]), double.CreateChecked(bSequence[i]));

        return accumulator.PearsonCoefficient;
    }

    /// <summary>
    /// Pearson product-moment correlation coefficient with edge clamping on the shifted sequence.
    /// </summary>
    public static double EvalPerf<N, NInt>(IList<N> aSequence, IList<N> bSequence, NInt lag)
         where N : INumberBase<N>
         where NInt : IBinaryInteger<NInt>
    {
        if (aSequence.Count != bSequence.Count)
            throw new ArgumentException("Inconsistent length of lists.");

        var shift = int.CreateChecked(lag);
        var accumulator = default(CoMomentAccumulator);
        for (int i = 0; i < aSequence.Count; i++)
            accumulator.Add(
                double.CreateChecked(aSequence[i]),
                double.CreateChecked(bSequence[ClampedIndex(i, shift, bSequence.Count)]));

        return accumulator.PearsonCoefficient;
    }

    /// <summary>
    /// Pearson product-moment correlation coefficient with edge clamping on the shifted sequence.
    /// </summary>
    public static double EvalPerf<N, NInt>(ReadOnlySpan<N> aSequence, ReadOnlySpan<N> bSequence, NInt lag)
         where N : INumberBase<N>
         where NInt : IBinaryInteger<NInt>
    {
        if (aSequence.Length != bSequence.Length)
            throw new ArgumentException("Inconsistent length of sequences.");

        var shift = int.CreateChecked(lag);
        var accumulator = default(CoMomentAccumulator);
        for (int i = 0; i < aSequence.Length; i++)
            accumulator.Add(
                double.CreateChecked(aSequence[i]),
                double.CreateChecked(bSequence[ClampedIndex(i, shift, bSequence.Length)]));

        return accumulator.PearsonCoefficient;
    }

    // the shifted index runs past the sequence at one end, the nearest edge value stands in for it
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static int ClampedIndex(int index, int shift, int length)
        => int.Clamp(index + shift, 0, length - 1);

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

        /// <summary>
        /// Cross-correlation using the product-moment formula with edge clamping.
        /// </summary>
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
        /// Allocation free cross-correlation using the product-moment formula with edge clamping.
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

        /// <summary>
        /// Auto-correlation using the product-moment formula with edge clamping.
        /// </summary>
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
        /// Allocation free auto-correlation using the product-moment formula with edge clamping.
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
}
