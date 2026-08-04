using SMath.Grid2D;
using Xunit;

namespace SMath;

public class GridPredicateTests
{
    private readonly struct EvenColumn : IGridPredicate
    {
        public bool Test(int x, int y)
            => x % 2 == 0;
    }

    [Fact]
    public void GridPredicate_EvaluatesTheDelegate()
    {
        var predicate = new GridPredicate((x, y) => x == y);

        Assert.True(predicate.Test(2, 2));
        Assert.False(predicate.Test(2, 3));
    }

    [Fact]
    public void GridNegatedPredicate_IsTheOppositeOfThePredicate()
    {
        var negated = new GridNegatedPredicate<EvenColumn>(default);

        Assert.False(negated.Test(0, 0));
        Assert.True(negated.Test(1, 0));
    }

    [Fact]
    public void GridNegatedPredicate_TwiceNegated_IsTheVeryPredicate()
    {
        var predicate = new GridNegatedPredicate<GridNegatedPredicate<EvenColumn>>(
            new GridNegatedPredicate<EvenColumn>(default));

        Assert.True(predicate.Test(0, 0));
        Assert.False(predicate.Test(1, 0));
    }

    [Fact]
    public void GridMaskPredicate_MatchesMarkedCells()
    {
        var mask = new byte[] { 0, 1, 0, 0 };
        var predicate = new GridMaskPredicate(mask, 2, 2);

        Assert.False(predicate.Test(0, 0));
        Assert.True(predicate.Test(1, 0));
    }

    [Fact]
    public void GridMaskPredicate_OutsideOfGrid_Matches()
        => Assert.True(new GridMaskPredicate(new byte[4], 2, 2).Test(9, 9));
}
