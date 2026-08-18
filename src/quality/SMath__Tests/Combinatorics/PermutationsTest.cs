using Xunit;

namespace SMath.Combinatorics;

public class PermutationsTest
{
    [Theory]
    [MemberData(nameof(CountForN))]
    public void Count(int n, int expected)
    {
        Assert.Equal(expected, Permutations.Count(n));
    }

    [Theory]
    [MemberData(nameof(CountForTuple1FromN))]
    [MemberData(nameof(CountForTuple2FromN))]
    [MemberData(nameof(CountForTuple3FromN))]
    [MemberData(nameof(CountForTuple4FromN))]
    public void CountTuples(int n, int k, int expected)
    {
        Assert.Equal(expected, Permutations.Count(n, k));
    }

    #region data
    public static IEnumerable<object[]> CountForN()
    {
        // n; expected
        yield return new object[] { -1, 0 };
        yield return new object[] { 0, 0 };
        yield return new object[] { 1, 1 };
        yield return new object[] { 2, 4 };
        yield return new object[] { 3, 15 };
        yield return new object[] { 4, 64 };
    }

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
        yield return new object[] { 1, 2, 0 };
        yield return new object[] { 2, 2, 2 };
        yield return new object[] { 3, 2, 6 };
        yield return new object[] { 4, 2, 12 };
        yield return new object[] { 5, 2, 20 };
    }

    public static IEnumerable<object[]> CountForTuple3FromN()
    {
        // n; k; expected
        yield return new object[] { -1, 3, 0 };
        yield return new object[] { 0, 3, 0 };
        yield return new object[] { 1, 3, 0 };
        yield return new object[] { 2, 3, 0 };
        yield return new object[] { 3, 3, 6 };
        yield return new object[] { 4, 3, 24 };
        yield return new object[] { 5, 3, 60 };
        yield return new object[] { 6, 3, 120 };
    }

    public static IEnumerable<object[]> CountForTuple4FromN()
    {
        // n; k; expected
        yield return new object[] { -1, 4, 0 };
        yield return new object[] { 0, 4, 0 };
        yield return new object[] { 1, 4, 0 };
        yield return new object[] { 2, 4, 0 };
        yield return new object[] { 3, 4, 0 };
        yield return new object[] { 4, 4, 24 };
        yield return new object[] { 5, 4, 120 };
        yield return new object[] { 6, 4, 360 };
    }
    #endregion
}
