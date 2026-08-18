namespace SMath.Statistics;

using Xunit;

public class CramerCorrelationData
{
    // knowing one category determines the other, Cramér's V is 1
    public static TheoryData<int[], int[]> FullyAssociatedSequences => new()
    {
        { new[] { 0, 0, 1, 1 }, new[] { 5, 5, 9, 9 } },
        { new[] { 0, 1, 2 }, new[] { 7, 8, 9 } },
        // association is unsigned, a reversed mapping is still a perfect association
        { new[] { 0, 0, 1, 1 }, new[] { 9, 9, 5, 5 } },
    };

    public static TheoryData<int[], int[], double> KnownAssociationData => new()
    {
        // balanced 2x2 table, the categories are independent
        { new[] { 0, 0, 1, 1 }, new[] { 0, 1, 0, 1 }, 0d },
        // one off-diagonal observation per cell, chi-squared 2/3 over n = 6
        { new[] { 0, 0, 0, 1, 1, 1 }, new[] { 0, 0, 1, 0, 1, 1 }, 0.333333 },
    };

    // a single distinct category on either axis leaves nothing to associate
    public static TheoryData<int[], int[]> SingleCategoryData => new()
    {
        { new[] { 0, 0, 0 }, new[] { 1, 2, 3 } },
        { new[] { 5, 5, 5 }, new[] { 5, 5, 5 } },
        { new[] { 1, 2, 3 }, new[] { 7, 7, 7 } },
    };
}
