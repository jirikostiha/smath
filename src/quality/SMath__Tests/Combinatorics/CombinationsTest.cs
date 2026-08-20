using Xunit;

namespace SMath.Combinatorics;

public class CombinationsTest
{
    [Theory]
    [MemberData(nameof(CountForN))]
    public void Count(int n, int expected)
    {
        Assert.Equal(expected, Combinations.Count(n));
    }

    [Theory]
    [MemberData(nameof(CountForTuple1FromN))]
    [MemberData(nameof(CountForTuple2FromN))]
    [MemberData(nameof(CountForTuple3FromN))]
    [MemberData(nameof(CountForTuple4FromN))]
    public void CountTuples(int n, int k, int expected)
    {
        Assert.Equal(expected, Combinations.Count(n, k));
    }

    [Theory]
    [MemberData(nameof(CountForTuple2FromN))]
    public void Tuple2(int n, int expected)
    {
        var tuples = Combinations.Tuple2(n).ToArray();
        Assert.Equal(expected, tuples.Distinct().Count());
    }

    [Theory]
    [MemberData(nameof(CountForTuple3FromN))]
    public void Tuple3(int n, int expected)
    {
        var tuples = Combinations.Tuple3(n).ToArray();
        Assert.Equal(expected, tuples.Distinct().Count());
    }

    [Theory]
    [MemberData(nameof(CountForTuple4FromN))]
    public void Tuple4(int n, int expected)
    {
        var tuples = Combinations.Tuple4(n).ToArray();
        Assert.Equal(expected, tuples.Distinct().Count());
    }

    [Theory]
    [MemberData(nameof(CountForTuple1FromN))]
    [MemberData(nameof(CountForTuple2FromN))]
    [MemberData(nameof(CountForTuple3FromN))]
    [MemberData(nameof(CountForTuple4FromN))]
    public void Tuples_CountMatchesCount(int n, int k, int expected)
    {
        Assert.Equal(expected, Combinations.Tuples(n, k).Count());
    }

    [Fact]
    public void Tuples_AreInLexicographicOrder()
    {
        Assert.Equal(
            new[] { new[] { 0, 1 }, new[] { 0, 2 }, new[] { 0, 3 }, new[] { 1, 2 }, new[] { 1, 3 }, new[] { 2, 3 } },
            Combinations.Tuples(4, 2).ToArray());
    }

    [Fact]
    public void Tuples_MatchTheFixedSizeOverloads()
    {
        Assert.Equal(
            Combinations.Tuple2(5).Select(tuple => new[] { tuple.Index1, tuple.Index2 }).ToArray(),
            Combinations.Tuples(5, 2).ToArray());

        Assert.Equal(
            Combinations.Tuple3(5).Select(tuple => new[] { tuple.Index1, tuple.Index2, tuple.Index3 }).ToArray(),
            Combinations.Tuples(5, 3).ToArray());
    }

    #region data
    public static IEnumerable<object[]> CountForN()
    {
        // n; expected
        yield return new object[] { -1, 0 };
        yield return new object[] { 0, 0 };
        yield return new object[] { 1, 1 };
        yield return new object[] { 2, 3 };
        yield return new object[] { 3, 7 };
        yield return new object[] { 4, 15 };
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
        yield return new object[] { 2, 2, 1 };
        yield return new object[] { 3, 2, 3 };
        yield return new object[] { 4, 2, 6 };
        yield return new object[] { 5, 2, 10 };
    }

    public static IEnumerable<object[]> CountForTuple3FromN()
    {
        // n; k; expected
        yield return new object[] { -1, 3, 0 };
        yield return new object[] { 0, 3, 0 };
        yield return new object[] { 1, 3, 0 };
        yield return new object[] { 2, 3, 0 };
        yield return new object[] { 3, 3, 1 };
        yield return new object[] { 4, 3, 4 };
        yield return new object[] { 5, 3, 10 };
        yield return new object[] { 6, 3, 20 };
    }

    public static IEnumerable<object[]> CountForTuple4FromN()
    {
        // n; k; expected
        yield return new object[] { -1, 4, 0 };
        yield return new object[] { 0, 4, 0 };
        yield return new object[] { 1, 4, 0 };
        yield return new object[] { 2, 4, 0 };
        yield return new object[] { 3, 4, 0 };
        yield return new object[] { 4, 4, 1 };
        yield return new object[] { 5, 4, 5 };
        yield return new object[] { 6, 4, 15 };
    }
    #endregion
}
