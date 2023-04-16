namespace SMath.FunctionsN
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using static System.Math;

    /// <summary>
    /// A RastriginFunction function is a non-convex non-linear multimodal function.
    /// See <a href="http://en.wikipedia.org/wiki/Rastrigin_function">this</a> for more info.
    /// </summary>
    /// <remarks>
    /// In mathematical optimization, the RastriginFunction function is a non-convex function used as 
    /// a performance test problem for optimization algorithms. It is a typical example of non-linear 
    /// multimodal function. This function is a fairly difficult problem due to its large search space 
    /// and its large number of local minima.
    /// It is defined by:
    /// f(x) = A n + \sum_{i=1}^n x_i^2 - A\cos(2 \pi x_i)
    /// where A = 10, n is number of dimmensions 
    /// It has a global minimum at x=0 where f(x)=0.
    /// http://tracer.lcc.uma.es/problems/rastrigin/rastrigin.html
    /// http://www.ft.utb.cz/people/zelinka/soma/   
    /// </remarks>
    public static class RastriginFunction
    {
        public static double f(IList<double> xs, double a = 10) => a * xs.Count + xs.Sum(x => x*x -a * Cos(2 * PI * x));

        public static double f(double x1, double x2, double a = 10)
           => 2*a + x1*x1 + x2*x2 - a * (Cos(2 * PI * x1) + Cos(2 * PI * x2));

        public const double GlobalMinX1 = 0;

        public const string Formula2 = "";

        public const string FormulaN = "n*A + sum([x² - A*cos(2*π*x)])";
    }
}
