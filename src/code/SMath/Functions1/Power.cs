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

    /// <summary>
    /// Square function
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Square_(algebra)">wikipedia</a>
    /// <a href="https://en.wikipedia.org/wiki/Exponentiation#Power_functions">wikipedia</a>
    /// </remarks>
    public static class Power2
    {
        public const double DomainMin = double.NegativeInfinity;
        public const double DomainMax = double.PositiveInfinity;

        public const double ImageMin = 0;
        public const double ImageMax = double.PositiveInfinity;

        public const bool IsEven = true;
        public const bool IsOdd = false;

        public const double InterceptsX1At = 0;

        public const double MinimumX1 = 0;
        public const double MinimumX2 = 0;

        public static double f(double x1) => x1 * x1;
        public static int f(int x1) => x1 * x1;
        public static long f(long x1) => x1 * x1;

        //todo Samples(from, to, step) samplovani dat 


        public const string Formula = "x1^2";
    }

    /// <summary>
    /// Cube function
    /// </summary>
    /// <remarks>
    /// <a href="https://en.wikipedia.org/wiki/Cube_(algebra)">wikipedia</a>
    /// </remarks>
    public static class Power3
    {
        public const double DomainMin = double.NegativeInfinity;
        public const double DomainMax = double.PositiveInfinity;

        public const double ImageMin = 0;
        public const double ImageMax = double.PositiveInfinity;

        public const bool IsEven = false;
        public const bool IsOdd = true;

        //public const double InterceptsX1At = 0;

        //public const double MinimumX1 = 0;
        //public const double MinimumX2 = 0;

        public static double f(double x1) => x1 * x1 * x1;
        public static int f(int x1) => x1 * x1 * x1;
        public static long f(long x1) => x1 * x1 * x1;

        public const string Formula = "x1^3";
    }

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
