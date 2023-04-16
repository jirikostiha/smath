using System;
using System.Collections.Generic;
using System.Numerics;

namespace SMath.Combinatorics
{
    /// <summary>
    /// Combinations with repetitiont
    /// Order does not matter
    /// {a,b}: a, b, aa, ab, bb
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Combination#Number_of_combinations_with_repetition">wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/Multiset.html">wolfram mathword</a>
    /// </remarks>
    public static class CombinationsWithRepetition
    {        
        //DES: NE - nema smysl pze muze byt k > n
        //public static long Count(long n)
        //{
        //    //Q: is there a formula?
        //    if (n > 0)
        //    {
        //        long sum = 0;
        //        for (int k = 1; k <= n; k++)
        //            sum += Count(n, k);

        //        return sum;
        //    }
        //    return 0; //or exception
        //}

        public static NInt Count<NInt>(NInt n, NInt k)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
            => n > NInt.Zero && k > NInt.Zero
                ? Factorial.Eval(n + k - NInt.One) / (Factorial.Eval(k) * Factorial.Eval(n - NInt.One))
                : NInt.Zero;

        public static IEnumerable<(NInt Index1, NInt Index2)> Tuple2<NInt>(NInt n)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n <= NInt.Zero)
                yield break;

            throw new NotImplementedException("todo");
        }

        public static IEnumerable<(NInt Index1, NInt Index2, NInt Index3)> Tuple3<NInt>(NInt n)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n <= NInt.Zero)
                yield break;

            throw new NotImplementedException("todo");
        }

        public static IEnumerable<(NInt Index1, NInt Index2, NInt Index3, NInt Index4)> Tuple4<NInt>(NInt n)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n <= NInt.Zero)
                yield break;

            throw new NotImplementedException("todo");
        }

        //DES: NE - nema smysl pze muze byt k > n
        //public static IEnumerable<int[]> Tuples(int n)
        //{
        //    if (n <= 0)
        //        yield break;

        //    for (int k = 1; k <= n; k++)
        //        foreach (var ktuple in Tuples(n, k))
        //            yield return ktuple;
        //}

        //https://rosettacode.org/wiki/Permutations_with_repetitions
        //https://www.tutorialcup.com/interview/string/print-permutations-repetition.htm
        public static IEnumerable<NInt[]> Tuples<NInt>(NInt n, NInt k)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n <= NInt.Zero || k <= NInt.Zero)
                yield break;            
        }
    }
}
