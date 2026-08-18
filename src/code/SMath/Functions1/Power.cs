namespace Wayout.Mathematics.Functions
{
    using System;

    /// <summary>
    /// General power function.
    /// X is base.
    /// </summary>
    // //spliva s sxp funkci, v math nedava smysl definovat oboji
    //public static class Power // power x1?, power pro 2 dim existuje?
    //{
    //    public static double f(double x1, int exponent) => Math.Pow(x1, exponent);
    //    public static double f(double x1, double exponent) => Math.Pow(x1, exponent);
    //}


    public static class Power4
    {
        public static double f(double x1) => x1 * x1 * x1 * x1;
        public static int f(int x1) => x1 * x1 * x1 * x1;
        public static long f(long x1) => x1 * x1 * x1 * x1;

        public const string Formula = "x1^4";
    }

    public static class Power5
    {
        public static double f(double x1) => x1 * x1 * x1 * x1 * x1;
        public static int f(int x1) => x1 * x1 * x1 * x1 * x1;
        public static long f(long x1) => x1 * x1 * x1 * x1 * x1;

        public const string Formula = "x1^5";
    }

    public static class Power6
    {
        public static double f(double x1) => x1 * x1 * x1 * x1 * x1 * x1;
        public static int f(int x1) => x1 * x1 * x1 * x1 * x1 * x1;
        public static long f(long x1) => x1 * x1 * x1 * x1 * x1 * x1;

        public const string Formula = "x1^6";
    }

    public static class Power7
    {
        public static double f(double x1) => x1 * x1 * x1 * x1 * x1 * x1 * x1;
        public static int f(int x1) => x1 * x1 * x1 * x1 * x1 * x1 * x1;
        public static long f(long x1) => x1 * x1 * x1 * x1 * x1 * x1 * x1;

        public const string Formula = "x1^7";
    }

    public static class Power8
    {
        public static double f(double x1) => x1 * x1 * x1 * x1 * x1 * x1 * x1 * x1;
        public static int f(int x1) => x1 * x1 * x1 * x1 * x1 * x1 * x1 * x1;
        public static long f(long x1) => x1 * x1 * x1 * x1 * x1 * x1 * x1 * x1;

        public const string Formula = "x1^8";
    }
}
