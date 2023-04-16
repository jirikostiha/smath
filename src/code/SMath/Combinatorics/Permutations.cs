using System;
using System.Collections.Generic;
using System.Numerics;

namespace SMath.Combinatorics
{
    //https://www.geeksforgeeks.org/c-program-to-print-all-permutations-of-a-given-string-2/
    //https://www.w3resource.com/csharp-exercises/recursion/csharp-recursion-exercise-11.php

    /// <summary>
    /// Permutations without repetition
    /// Order does matter
    /// {a,b}: a, b, ab, ba
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Permutation">wikipedia</a>
    /// <a href="https://mathworld.wolfram.com/Permutation.html">wolfram mathword</a>
    /// </remarks>
    public static class Permutations
    {
        /// <summary>
        /// Counts permutations
        /// </summary>
        /// <param name="n"></param>
        public static NInt Count<NInt>(NInt n)
             where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
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
        /// Counts k-permutations of n.
        /// </summary>
        /// <remarks>
        /// <a href="https://en.wikipedia.org/wiki/Permutation#k-permutations_of_n">wikipedia</a>       
        /// </remarks>
        /// <param name="n"></param>
        /// <param name="k"></param>
        public static NInt Count<NInt>(NInt n, NInt k)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
            => n >= k && k > NInt.Zero
                ? Factorial.Eval(n) / Factorial.Eval(n - k)
                : NInt.Zero; //or exception

        public static IEnumerable<(NInt Index1, NInt Index2)> Tuple2<NInt>(NInt n)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            throw new NotImplementedException("todo");
        }

        public static IEnumerable<(NInt Index1, NInt Index2, NInt Index3)> Tuple3<NInt>(NInt n)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            throw new NotImplementedException("todo");
        }

        public static IEnumerable<(NInt Index1, NInt Index2, NInt Index3, NInt Index4)> Tuple4<NInt>(NInt n)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            throw new NotImplementedException("todo");
        }

        public static IEnumerable<NInt[]> Tuples<NInt>(NInt n)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n <= NInt.Zero)
                yield break;

            for (var k = NInt.One; k <= n; k++)
                foreach (var ktuple in Tuples(n, k))
                    yield return ktuple;
        }

        //https://www.baeldung.com/cs/array-generate-all-permutations#:~:text=Heap's%20Algorithm,of%20these%20elements%20exactly%20once.
        //https://www.geeksforgeeks.org/heaps-algorithm-for-generating-permutations/
        //https://rosettacode.org/wiki/Permutations_with_repetitions
        public static IEnumerable<NInt[]> Tuples<NInt>(NInt n, NInt k)
            where NInt : IBinaryInteger<NInt>, IComparisonOperators<NInt, NInt, bool>
        {
            if (n < k || k <= NInt.Zero)
                yield break;


            throw new NotImplementedException("todo");
        }
    }
}
