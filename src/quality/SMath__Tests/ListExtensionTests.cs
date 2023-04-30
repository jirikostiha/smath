using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SMath
{
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
        [MemberData(nameof(NumberCollections.SmallestKthElementsData), MemberType = typeof(NumberCollections))]
        public void KthLargestElement(int[] collection, int k, int expElement)
        {
            Assert.Equal(expElement, collection.KthSmallestElement(k));
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
        private static int[] _positiveOrdered = new int[] { 1, 2, 3, 4 };
        private static int[] _negativeOrdered = new int[] { -4, -3, -2, -1 };

        public static IEnumerable<object[]> SmallestKthElementsData()
        {
            yield return new object[] { _positiveOrdered.ToArray(), 1, 1 };
            yield return new object[] { _positiveOrdered.ToArray(), 2, 2 };
            yield return new object[] { _positiveOrdered.ToArray(), 4, 4 };
            yield return new object[] { _positiveOrdered.Reverse().ToArray(), 1, 1 };
            yield return new object[] { _positiveOrdered.Reverse().ToArray(), 2, 2 };
            yield return new object[] { _positiveOrdered.Reverse().ToArray(), 4, 4 };

            yield return new object[] { _negativeOrdered.ToArray(), 1, -4 };
            yield return new object[] { _negativeOrdered.ToArray(), 2, -3 };
            yield return new object[] { _negativeOrdered.ToArray(), 4, -1 };
            yield return new object[] { _negativeOrdered.Reverse().ToArray(), 1, -4 };
            yield return new object[] { _negativeOrdered.Reverse().ToArray(), 2, -3 };
            yield return new object[] { _negativeOrdered.Reverse().ToArray(), 4, -1 };

            yield return new object[] { new int[] { 3, -1, 4, 5, 0 }, 2, 0 };
        }
    }

    internal class TestClass
    {
        public int Num { get; set; }
        public string Title { get; set; }
    }
}
