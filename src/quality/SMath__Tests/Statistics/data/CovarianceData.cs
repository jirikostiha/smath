namespace SMath.Statistics;

using Xunit;

public class CovarianceData
{
    public static TheoryData<int[], int[], double> Data => new()
    {
        { new[] { 0, 0 }, new[] { 0, 0 }, 0 },
        { new[] { 0, 0 }, new[] { 1, 1 }, 0 },
        { new[] { 1, 1 }, new[] { 1, 1 }, 0 },
        { new[] { 0, 1, 2 }, new[] { 0, 1, 2 }, 1 },
        { new[] { 0, 1, 2 }, new[] { 0, -1, -2 }, -1 },
    };
}