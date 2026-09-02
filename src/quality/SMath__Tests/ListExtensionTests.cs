using System.Collections.Generic;
using Xunit;

namespace SMath;

public class ListExtensionTests
{
    [Theory]
    [MemberData(nameof(NumberCollections.SmallestKthElementsData), MemberType = typeof(NumberCollections))]
    public void KthSmallestElement(int[] collection, int k, int expElement)
    {
        Assert.Equal(expElement, collection.KthSmallestElement(k));
    }

    [Fact]
    public void KthSmallestComplexElement()
    {
        var collection = new TestClass[]
        {
            new TestClass() { Num = 3, Title = "three" },
            new TestClass() { Num = 1, Title = "one" },
            new TestClass() { Num = 4, Title = "four" },
            new TestClass() { Num = 2, Title = "two" },
        };

        var element = collection.KthSmallestElement(2, o => o.Num);

        Assert.Equal("two", element.Title);
    }

    [Theory]
    [MemberData(nameof(NumberCollections.LargestKthElementsData), MemberType = typeof(NumberCollections))]
    public void KthLargestElement(int[] collection, int k, int expElement)
    {
        Assert.Equal(expElement, collection.KthLargestElement(k));
    }

    [Fact]
    public void KthLargestComplexElement()
    {
        var collection = new TestClass[]
        {
            new TestClass() { Num = 3, Title = "three" },
            new TestClass() { Num = 1, Title = "one" },
            new TestClass() { Num = 4, Title = "four" },
            new TestClass() { Num = 2, Title = "two" },
        };

        var element = collection.KthLargestElement(2, o => o.Num);

        Assert.Equal("three", element.Title);
    }

    [Fact]
    public void EmptyList_ThrowsArgumentException()
    {
        var empty = System.Array.Empty<int>();
        Assert.Throws<System.ArgumentException>(() => empty.KthSmallestElement(1));
        Assert.Throws<System.ArgumentException>(() => empty.KthLargestElement(1));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(5)]
    public void InvalidK_ThrowsArgumentOutOfRangeException(int k)
    {
        var list = new[] { 1, 2, 3, 4 };
        Assert.Throws<System.ArgumentOutOfRangeException>(() => list.KthSmallestElement(k));
        Assert.Throws<System.ArgumentOutOfRangeException>(() => list.KthLargestElement(k));
    }

    [Fact]
    public void SingleElementList_ReturnsElement()
    {
        var list = new[] { 42 };
        Assert.Equal(42, list.KthSmallestElement(1));
        Assert.Equal(42, list.KthLargestElement(1));
    }

    [Fact]
    public void NullArguments_ThrowArgumentNullException()
    {
        System.Collections.Generic.IList<int>? nullList = null;
        Assert.Throws<System.ArgumentNullException>(() => nullList!.KthSmallestElement(1));
        Assert.Throws<System.ArgumentNullException>(() => nullList!.KthLargestElement(1));

        var list = new[] { 1, 2, 3 };
        System.Func<int, int>? nullSelector = null;
        Assert.Throws<System.ArgumentNullException>(() => list.KthSmallestElement(1, nullSelector!));
        Assert.Throws<System.ArgumentNullException>(() => list.KthLargestElement(1, nullSelector!));
    }

    [Fact]
    public void RangeOverload_InvalidRange_Throws()
    {
        var list = new[] { 10, 20, 30, 40, 50 };

        Assert.Throws<System.ArgumentOutOfRangeException>(() => list.KthSmallestElement(1, -1, 3, x => x));
        Assert.Throws<System.ArgumentOutOfRangeException>(() => list.KthSmallestElement(1, 0, 5, x => x));
        Assert.Throws<System.ArgumentException>(() => list.KthSmallestElement(1, 3, 2, x => x));
        Assert.Throws<System.ArgumentOutOfRangeException>(() => list.KthSmallestElement(1, 2, 4, x => x));
        Assert.Throws<System.ArgumentOutOfRangeException>(() => list.KthSmallestElement(5, 1, 3, x => x));

        Assert.Throws<System.ArgumentOutOfRangeException>(() => list.KthLargestElement(1, -1, 3, x => x));
        Assert.Throws<System.ArgumentOutOfRangeException>(() => list.KthLargestElement(1, 0, 5, x => x));
        Assert.Throws<System.ArgumentException>(() => list.KthLargestElement(1, 3, 2, x => x));
        Assert.Throws<System.ArgumentOutOfRangeException>(() => list.KthLargestElement(1, 2, 4, x => x));
        Assert.Throws<System.ArgumentOutOfRangeException>(() => list.KthLargestElement(5, 1, 3, x => x));
    }

    [Fact]
    public void RangeOverload_ValidRange_ReturnsElement()
    {
        var list = new[] { 50, 40, 10, 30, 20 };
        Assert.Equal(10, list.KthSmallestElement(2, 1, 3, x => x));
        Assert.Equal(30, list.KthSmallestElement(3, 1, 3, x => x));
    }

    [Fact]
    public void Duplicates_ReturnsCorrectElement()
    {
        var list = new[] { 3, 1, 2, 3, 3, 1 };
        Assert.Equal(1, list.KthSmallestElement(1));
        Assert.Equal(1, list.KthSmallestElement(2));
        Assert.Equal(2, list.KthSmallestElement(3));
        Assert.Equal(3, list.KthSmallestElement(4));
        Assert.Equal(3, list.KthSmallestElement(6));

        Assert.Equal(3, list.KthLargestElement(1));
        Assert.Equal(3, list.KthLargestElement(3));
        Assert.Equal(2, list.KthLargestElement(4));
        Assert.Equal(1, list.KthLargestElement(5));
        Assert.Equal(1, list.KthLargestElement(6));
    }
}

public static class NumberCollections
{
    private static int[] PositiveOrdered => new int[] { 1, 2, 3, 4 };
    private static int[] PositiveReversed => new int[] { 4, 3, 2, 1 };
    private static int[] NegativeOrdered => new int[] { -4, -3, -2, -1 };
    private static int[] NegativeReversed => new int[] { -1, -2, -3, -4 };

    public static IEnumerable<object[]> SmallestKthElementsData()
    {
        yield return new object[] { PositiveOrdered, 1, 1 };
        yield return new object[] { PositiveOrdered, 2, 2 };
        yield return new object[] { PositiveOrdered, 4, 4 };
        yield return new object[] { PositiveReversed, 1, 1 };
        yield return new object[] { PositiveReversed, 2, 2 };
        yield return new object[] { PositiveReversed, 4, 4 };

        yield return new object[] { NegativeOrdered, 1, -4 };
        yield return new object[] { NegativeOrdered, 2, -3 };
        yield return new object[] { NegativeOrdered, 4, -1 };
        yield return new object[] { NegativeReversed, 1, -4 };
        yield return new object[] { NegativeReversed, 2, -3 };
        yield return new object[] { NegativeReversed, 4, -1 };

        yield return new object[] { new int[] { 3, -1, 4, 5, 0 }, 2, 0 };
    }

    public static IEnumerable<object[]> LargestKthElementsData()
    {
        yield return new object[] { PositiveOrdered, 1, 4 };
        yield return new object[] { PositiveOrdered, 2, 3 };
        yield return new object[] { PositiveOrdered, 4, 1 };
        yield return new object[] { PositiveReversed, 1, 4 };
        yield return new object[] { PositiveReversed, 2, 3 };
        yield return new object[] { PositiveReversed, 4, 1 };

        yield return new object[] { NegativeOrdered, 1, -1 };
        yield return new object[] { NegativeOrdered, 2, -2 };
        yield return new object[] { NegativeOrdered, 4, -4 };
        yield return new object[] { NegativeReversed, 1, -1 };
        yield return new object[] { NegativeReversed, 2, -2 };
        yield return new object[] { NegativeReversed, 4, -4 };

        yield return new object[] { new int[] { 3, -1, 4, 5, 0 }, 2, 4 };
    }
}

internal sealed class TestClass
{
    public int Num { get; set; }
    public string? Title { get; set; }
}
