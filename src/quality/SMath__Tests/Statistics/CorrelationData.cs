namespace SMath.Statistics
{
    using Xunit;

    public class CorrelationData
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
            { new [] { 1, 1, 1 }, new [] { 1, 2, 3 }, 0 },
            { new [] { 1, 1, 1 }, new [] { 3, 2, 1 }, 0 },
            { new [] { 0, 0, 0 }, new[] { 0, 0, 0 }, 0 },  // cannot decide -> 0
            { new [] { 1, 1, 1, 1 }, new[] { 1, 1, 1, 1 }, 0 }  // cannot decide -> 0
        };
    }
}