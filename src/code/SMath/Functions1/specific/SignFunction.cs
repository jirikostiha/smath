namespace Wayout.Mathematics.Functions
{
    /// <summary>
	/// Sign or signum step function
	/// </summary>
	/// <remarks>
	/// <a href="https://en.wikipedia.org/wiki/Sign_function">wikipedia</a>
	/// </remarks>
    public static class SignFunction
	{
	 	public static double f(double x1) => (x1 < 0) ? -1 : (x1 > 0) ? 1 : 0;

		public const string Formula = "-1, x1 < 0; 1, x1 > 0; 0, x1 = 0";
	}
}
