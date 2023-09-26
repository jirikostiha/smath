namespace SMath.Statistics;

using Xunit;

public class VarianceData
{
    public static TheoryData<int[], double> Sample => new()
    {
        { new[] { 0, 0 }, 0 },
        { new[] { 1, 1 }, 0 },
        { new[] { 2, 2 }, 0 },
        { new[] { 0, 1, 2 }, 1 },
        { new[] { 0, -1, -2 }, 1 },
        { new[] { 1, 2, 3, 4, 5, 6 }, 3.5 },
    };

    public static TheoryData<int[], double> Population => new()
    {
        { new[] { 0, 0 }, 0 },
        { new[] { 1, 1 }, 0 },
        { new[] { 2, 2 }, 0 },
        { new[] { 0, 1, 2 }, 0.66666667 },
        { new[] { 0, -1, -2 }, 0.66666667 },
        { new[] { 1, 2, 3, 4, 5, 6 }, 2.9166667 },
    };
}