using Xunit;

namespace SMath.Combinatorics;

public class PermutationsWithRepetitionTest
{
    [Theory]
    [MemberData(nameof(CountForTuple1FromN))]
    [MemberData(nameof(CountForTuple2FromN))]
    [MemberData(nameof(CountForTuple3FromN))]
    [MemberData(nameof(CountForTuple4FromN))]
    public void CountTuples(int n, int k, int expected)
    {
        Assert.Equal(expected, PermutationsWithRepetition.Count(n, k));
    }

    [Theory]
    [MemberData(nameof(CountForTuple1FromN))]
    [MemberData(nameof(CountForTuple2FromN))]
    [MemberData(nameof(CountForTuple3FromN))]
    [MemberData(nameof(CountForTuple4FromN))]
    public void Tuples_CountMatchesCount(int n, int k, int expected)
    {
        Assert.Equal(expected, PermutationsWithRepetition.Tuples(n, k).Count());
    }

    [Fact]
    public void Tuples_AreInLexicographicOrder()
    {
        Assert.Equal(
            new[] { new[] { 0, 0 }, new[] { 0, 1 }, new[] { 1, 0 }, new[] { 1, 1 } },
            PermutationsWithRepetition.Tuples(2, 2).ToArray());
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
        yield return new object[] { 2, 2, 4 };
        yield return new object[] { 3, 2, 9 };
        yield return new object[] { 4, 2, 16 };
        yield return new object[] { 5, 2, 25 };
    }

    public static IEnumerable<object[]> CountForTuple3FromN()
    {
        // n; k; expected
        yield return new object[] { -1, 3, 0 };
        yield return new object[] { 0, 3, 0 };
        yield return new object[] { 1, 3, 1 };
        yield return new object[] { 2, 3, 8 };
        yield return new object[] { 3, 3, 27 };
        yield return new object[] { 4, 3, 64 };
        yield return new object[] { 5, 3, 125 };
        yield return new object[] { 6, 3, 216 };
    }

    public static IEnumerable<object[]> CountForTuple4FromN()
    {
        // n; k; expected
        yield return new object[] { -1, 4, 0 };
        yield return new object[] { 0, 4, 0 };
        yield return new object[] { 1, 4, 1 };
        yield return new object[] { 2, 4, 16 };
        yield return new object[] { 3, 4, 81 };
        yield return new object[] { 4, 4, 256 };
        yield return new object[] { 5, 4, 625 };
        yield return new object[] { 6, 4, 1296 };
    }
    #endregion
}
