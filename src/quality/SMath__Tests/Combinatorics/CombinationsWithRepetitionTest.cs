using Xunit;

namespace SMath.Combinatorics;

public class CombinationsWithRepetitionTest
{
    [Theory]
    [MemberData(nameof(CountForTuple1FromN))]
    [MemberData(nameof(CountForTuple2FromN))]
    [MemberData(nameof(CountForTuple3FromN))]
    [MemberData(nameof(CountForTuple4FromN))]
    public void CountTuples(int n, int k, int expected)
    {
        Assert.Equal(expected, CombinationsWithRepetition.Count(n, k));
    }

    [Theory]
    [MemberData(nameof(CountForTuple1FromN))]
    [MemberData(nameof(CountForTuple2FromN))]
    [MemberData(nameof(CountForTuple3FromN))]
    [MemberData(nameof(CountForTuple4FromN))]
    public void Tuples_CountMatchesCount(int n, int k, int expected)
    {
        Assert.Equal(expected, CombinationsWithRepetition.Tuples(n, k).Count());
    }

    [Fact]
    public void Tuples_AreInLexicographicOrder()
    {
        Assert.Equal(
            new[] { new[] { 0, 0 }, new[] { 0, 1 }, new[] { 0, 2 }, new[] { 1, 1 }, new[] { 1, 2 }, new[] { 2, 2 } },
            CombinationsWithRepetition.Tuples(3, 2).ToArray());
    }

    #region data
    public static IEnumerable<object[]> CountForTuple1FromN()
    {
        // n; k; expected
        yield return new object[] { -1, 1, 0 };
        yield return new object[] { 0, 1, 0 };
        yield return new object[] { 1, 1, 1 };
        yield return new object[] { 2, 1, 2 };
        yield return new object[] { 3, 1, 3 };
    }

    public static IEnumerable<object[]> CountForTuple2FromN()
    {
        // n; k; expected
        yield return new object[] { -1, 2, 0 };
        yield return new object[] { 0, 2, 0 };
        yield return new object[] { 1, 2, 1 };
        yield return new object[] { 2, 2, 3 };
        yield return new object[] { 3, 2, 6 };
        yield return new object[] { 4, 2, 10 };
        yield return new object[] { 5, 2, 15 };
    }

    public static IEnumerable<object[]> CountForTuple3FromN()
    {
        // n; k; expected
        yield return new object[] { -1, 3, 0 };
        yield return new object[] { 0, 3, 0 };
        yield return new object[] { 1, 3, 1 };
        yield return new object[] { 2, 3, 4 };
        yield return new object[] { 3, 3, 10 };
        yield return new object[] { 4, 3, 20 };
        yield return new object[] { 5, 3, 35 };
        yield return new object[] { 6, 3, 56 };
    }

    public static IEnumerable<object[]> CountForTuple4FromN()
    {
        // n; k; expected
        yield return new object[] { -1, 4, 0 };
        yield return new object[] { 0, 4, 0 };
        yield return new object[] { 1, 4, 1 };
        yield return new object[] { 2, 4, 5 };
        yield return new object[] { 3, 4, 15 };
        yield return new object[] { 4, 4, 35 };
        yield return new object[] { 5, 4, 70 };
        yield return new object[] { 6, 4, 126 };
    }
    #endregion
}
