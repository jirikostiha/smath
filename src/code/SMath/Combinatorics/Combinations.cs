using SMath.Expansions;
using System;
using System.Collections.Generic;
using System.Numerics;

namespace SMath.Combinatorics
{
    /// <summary>
    /// Combinations without repetition
    /// Order does not matter
    /// {a,b}: a, b, ab
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Combination">wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/Combination.html">wolfram mathword</a>
    /// https://www.mathsisfun.com/combinatorics/combinations-permutations-calculator.html
    /// </remarks>
    public static class Combinations
    {
        /// <summary>
        /// Counts combinations (without repetition).
        /// </summary>
        public static NInt Count<NInt>(NInt n)
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            //Q: is there a formula?
            if (n > NInt.Zero)
            {
                var sum = NInt.Zero;
                for (var k = NInt.One; k <= n; k++)
                    sum += Count(n, k);

                return sum;
            }
            return NInt.Zero; //or exception
        }

        /// <summary>
        /// Counts k-combinations of n.
        /// </summary>
        /// <param name="n"></param>
        /// <param name="k"> trida </param>
        public static NInt Count<NInt>(NInt n, NInt k)
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
            => n >= k && k > NInt.Zero
                ? BinomialCoefficient.Eval(n, k)
                : NInt.Zero; //or exception

        public static IEnumerable<(NInt Index1, NInt Index2)> Tuple2<NInt>(NInt n)
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            for (var i = NInt.Zero; i < n - NInt.One; i++)
                for (var j = i + NInt.One; j < n; j++)
                    yield return (i, j);
        }

        public static IEnumerable<(NInt Index1, NInt Index2, NInt Index3)> Tuple3<NInt>(NInt n)
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            for (var i = NInt.Zero; i < n - NInt.One; i++)
                for (var j = i + NInt.One; j < n; j++)
                    for (var k = j + NInt.One; k < n; k++)
                        yield return (i, j, k);
        }

        public static IEnumerable<(NInt Index1, NInt Index2, NInt Index3, NInt Index4)> Tuple4<NInt>(NInt n)
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            for (var i = NInt.Zero; i < n - NInt.One; i++)
                for (var j = i + NInt.One; j < n; j++)
                    for (var k = j + NInt.One; k < n; k++)
                        for (var l = k + NInt.One; l < n; l++)
                            yield return (i, j, k, l);
        }

        public static IEnumerable<NInt[]> Tuples<NInt>(NInt n) //all
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n <= NInt.Zero)
                yield break;

            for (var k = NInt.One; k <= n; k++)
                foreach (var ktuple in Tuples(n, k))
                    yield return ktuple;
        }

        public static IEnumerable<NInt[]> Tuples<NInt>(NInt n, NInt k)
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n < k || k <= NInt.Zero)
                yield break;

            throw new NotImplementedException("todo");
        }
    }
}
