using System;
using System.Collections.Generic;
using System.Numerics;

namespace SMath.Combinatorics
{
    /// <summary>
    /// Permutations with repetition
    /// Order does matter
    /// {a,b}: a, b, aa, ab, ba, bb
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Permutation#Permutations_with_repetition">wikipedia</a>
    /// </remarks>
    public static class PermutationsWithRepetition
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
            where NInt : IPowerFunctions<NInt>, IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
            => n > NInt.Zero && k > NInt.Zero
                ? NInt.Pow(n, k)
                : NInt.Zero; //or exc

        public static IEnumerable<(NInt Index1, NInt Index2)> Tuple2<NInt>(NInt n)
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n <= NInt.Zero)
                yield break;

            throw new NotImplementedException("todo");
        }

        public static IEnumerable<(NInt Index1, NInt Index2, NInt Index3)> Tuple3<NInt>(NInt n)
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n <= NInt.Zero)
                yield break;

            throw new NotImplementedException("todo");
        }

        public static IEnumerable<(NInt Index1, NInt Index2, NInt Index3, NInt Index4)> Tuple4<NInt>(NInt n)
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n <= NInt.Zero)
                yield break;

            throw new NotImplementedException("todo");
        }

        //DES: NE - nema smysl pze muze byt k > n
        //public static IEnumerable<int[]> Tuples(int n) //all tuples
        //{
        //    if (n <= 0)
        //        yield break;

        //    for (int k = 1; k <= n; k++)
        //        foreach (var ktuple in Tuples(n, k))
        //            yield return ktuple;
        //}

        //https://www.baeldung.com/cs/permutations-with-repetition
        //https://www.geeksforgeeks.org/print-all-permutations-with-repetition-of-characters/
        //https://rosettacode.org/wiki/Permutations_with_repetitions
        public static IEnumerable<NInt[]> Tuples<NInt>(NInt n, NInt k)
            where NInt : IUnsignedNumber<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n < k || k <= NInt.Zero)
                yield break;

            throw new NotImplementedException("todo");
        }
    }
}
