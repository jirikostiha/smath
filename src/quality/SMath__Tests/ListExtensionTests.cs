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
    public string Title { get; set; }
}
