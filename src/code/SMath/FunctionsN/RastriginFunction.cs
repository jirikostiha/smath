using System.Numerics;
using SMath.Functions1;

namespace SMath.FunctionsN;

/// <summary>
/// Rastrigin function in n dimensions.
/// </summary>
/// <remarks>
/// A non convex non linear multimodal function used as a performance test problem for
/// optimization algorithms. The large search space and the large number of regularly
/// distributed local minima make it a hard problem.
/// <a href="https://en.wikipedia.org/wiki/Rastrigin_function">Wikipedia</a>
/// <a href="https://www.sfu.ca/~ssurjano/rastr.html">Simon Fraser University</a>
/// </remarks>
public static class RastriginFunction
{
    /// <summary>
    /// String representation of the function.
    /// </summary>
    public static string PlainTextFormula
        => "a*n + sum(xi^2 - a*cos(2*pi*xi))";

    /// <summary>
    /// Commonly used value of the amplitude parameter.
    /// </summary>
    public static N DefaultAmplitude<N>()
        where N : INumberBase<N>
        => N.CreateChecked(10);

    /// <summary>
    /// Value of the global minimum. It is attained at the origin for any amplitude.
    /// </summary>
    public static N GlobalMinimum<N>()
        where N : INumberBase<N>
        => N.Zero;

    /// <summary>
    /// Evaluates the function value.
    /// </summary>
    /// <param name="point"> Coordinates of the point. </param>
    /// <param name="a"> Amplitude of the modulation. </param>
    public static N Eval<N>(IEnumerable<N> point, N a)
        where N : ITrigonometricFunctions<N>
    {
        var doublePi = N.CreateChecked(2) * N.Pi;
        var sum = N.Zero;
        var dimensions = N.Zero;

        foreach (var coordinate in point)
        {
            sum += Power2.Eval(coordinate) - (a * N.Cos(doublePi * coordinate));
            dimensions++;
        }

        return (a * dimensions) + sum;
    }

    /// <summary>
    /// Evaluates the function value with the default amplitude.
    /// </summary>
    /// <param name="point"> Coordinates of the point. </param>
    public static N Eval<N>(IEnumerable<N> point)
        where N : ITrigonometricFunctions<N>
        => Eval(point, DefaultAmplitude<N>());

    /// <summary>
    /// Evaluates the function value.
    /// </summary>
    /// <param name="point"> Coordinates of the point. </param>
    /// <param name="a"> Amplitude of the modulation. </param>
    public static N Eval<N>(ReadOnlySpan<N> point, N a)
        where N : ITrigonometricFunctions<N>
    {
        var doublePi = N.CreateChecked(2) * N.Pi;
        var sum = N.Zero;

        for (int i = 0; i < point.Length; i++)
            sum += Power2.Eval(point[i]) - (a * N.Cos(doublePi * point[i]));

        return (a * N.CreateChecked(point.Length)) + sum;
    }

    /// <summary>
    /// Evaluates the function value with the default amplitude.
    /// </summary>
    /// <param name="point"> Coordinates of the point. </param>
    public static N Eval<N>(ReadOnlySpan<N> point)
        where N : ITrigonometricFunctions<N>
        => Eval(point, DefaultAmplitude<N>());
}
