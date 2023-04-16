namespace SMath.FunctionsN
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using static System.Math;

    /// <summary>
    /// Diagonal distance in matrix.
    /// </summary>
    /// <remarks>
    /// </remarks>
    public static class DiagonalDistance
    {
        public static int f(IEnumerable<int> xs) => xs.Max();
        public static int f(int x1, int x2) => Max(Abs(x1), Abs(x2));
        public static int f(int x1, int x2, int x3) => Max(Max(Abs(x1), Abs(x2)), Abs(x3));

        public static long f(IEnumerable<long> xs) => xs.Max();
        public static long f(long x1, long x2) => Max(Abs(x1), Abs(x2));
        public static long f(long x1, long x2, long x3) => Max(Max(Abs(x1), Abs(x2)), Abs(x3));

        //public const string Formula = "";
    }
}
