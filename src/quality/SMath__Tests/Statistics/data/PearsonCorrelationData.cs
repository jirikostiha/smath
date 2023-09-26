namespace SMath.Statistics;

using Xunit;

public class PearsonCorrelationData
{
    public static TheoryData<int[], int[]> FullyCorrelatedSequences => new()
    {
        { new[] { 0, 1, 2 }, new[] { 1, 2, 3 } },
        { new[] { 2, 1, 0 }, new[] { 2, 1, 0 } },
    };

    public static TheoryData<int[], int[]> FullyNegativellyCorrelatedSequences => new()
    {
        { new[] { 0, 1, 2 }, new [] { 3, 2, 1 } },
        { new[] { 1, 2, 1 }, new [] { 3, 2, 3 } }
    };

    public static TheoryData<int[], int[], double> NotCorrelatedData => new()
    {
        { new [] { 1, 2, 1 }, new [] { 2, 1, 2 }, -1 },
        { new [] { 1, 2, 1 }, new [] { 1, 2, 3 }, 0 },
    };

    public static TheoryData<int[], int[], double> SameValues => new()
    {
        { new [] { 0, 0, 0 }, new[] { 0, 0, 0 }, double.NaN },  // cannot decide
        { new [] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, double.NaN },  // cannot decide
        { new [] { 1, 1, 1 }, new [] { 1, 2, 3 }, double.NaN },  // cannot decide
        { new [] { 1, 1, 1 }, new [] { 3, 2, 1 }, double.NaN },  // cannot decide
    };
}