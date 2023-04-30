using System.Numerics;

namespace SMath
{
    /// <summary>
    /// List extensions.
    /// </summary>
    public static class ListExtension
    {
        public static IEnumerable<T> SmallestElements<T, N>(this IList<T> list, int count, Func<T, N> valueSelector)
            where N : IComparisonOperators<N, N, bool>
            => list.SmallestElements<T, N>(count, 0, list.Count - 1, valueSelector);

        public static IEnumerable<N> SmallestElements<N>(this IList<N> list, int count)
            where N : IComparisonOperators<N, N, bool>
            => list.SmallestElements(count, 0, list.Count - 1, x => x);

        public static IEnumerable<T> SmallestElements<T, N>(this IList<T> list, int count, int start, int end, Func<T, N> valueSelector)
            where N : IComparisonOperators<N, N, bool>
        {
            throw new NotImplementedException("todo");
        }

        public static T KthSmallestElement<T, N>(this IList<T> list, int k, Func<T, N> valueSelector)
            where N : IComparisonOperators<N, N, bool>
            => list.KthSmallestElement<T, N>(k, 0, list.Count - 1, valueSelector);

        public static N KthSmallestElement<N>(this IList<N> list, int k)
            where N : IComparisonOperators<N, N, bool>
            => list.KthSmallestElement(k, 0, list.Count - 1, x => x);

        /// <summary>
        /// Investigate kth smallest element.
        /// </summary>
        /// <typeparam name="T"> Type of element. </typeparam>
        /// <typeparam name="N"> Type of numeric value. </typeparam>
        /// <param name="list"> List of elements. </param>
        /// <param name="k"> K </param>
        /// <param name="start"> Starting index. </param>
        /// <param name="end"> Ending index. </param>
        /// <param name="valueSelector"> Numeric value selector from element. </param>
        /// <returns> Kth smallest element. </returns>
        public static T KthSmallestElement<T, N>(this IList<T> list, int k, int start, int end, Func<T, N> valueSelector)
            where N : IComparisonOperators<N, N, bool>
        {
            if (start >= end)
                return list[k - 1];

            // Index from where array will be splitted
            int q = Partition(list, start, end, valueSelector);
            //If pivot is placed at index k-1 then it is the solution
            if (q == (k - 1))
            {
                return list[k - 1];
            }
            else if (q > (k - 1))
            {
                return KthSmallestElement(list, k, start, q - 1, valueSelector);
            }
            else
            {
                return KthSmallestElement(list, k, q + 1, end, valueSelector);
            }
        }

        public static T KthLargestElement<T, N>(this IList<T> list, int k, Func<T, N> valueSelector)
            where N : IComparisonOperators<N, N, bool>
            => list.KthLargestElement(k, 0, list.Count - 1, valueSelector);

        public static N KthLargestElement<N>(this IList<N> list, int k)
            where N : IComparisonOperators<N, N, bool>
            => list.KthLargestElement<N, N>(k, 0, list.Count - 1, x => x);

        /// <summary>
        /// Investigate kth smallest element.
        /// </summary>
        /// <typeparam name="T"> Type of element. </typeparam>
        /// <typeparam name="N"> Type of numeric value. </typeparam>
        /// <param name="list"> List of elements. </param>
        /// <param name="k"> K </param>
        /// <param name="start"> Starting index. </param>
        /// <param name="end"> Ending index. </param>
        /// <param name="valueSelector"> Numeric value selector from element. </param>
        /// <returns> Kth smallest element. </returns>
        public static T KthLargestElement<T, N>(this IList<T> list, int k, int start, int end, Func<T, N> valueSelector)
            where N : IComparisonOperators<N, N, bool>
        {
            if (start >= end)
                return list[k - 1];

            // Index from where array will be splitted
            int q = Partition2(list, start, end, valueSelector);
            //If pivot is placed at index k-1 then it is the solution
            if (q == (k - 1))
            {
                return list[k - 1];
            }
            else if (q > (k - 1))
            {
                return KthLargestElement(list, k, start, q - 1, valueSelector);
            }
            else
            {
                return KthLargestElement(list, k, q + 1, end, valueSelector);
            }
        }

        private static int Partition<T, N>(IList<T> list, int p, int r, Func<T, N> valueSelector)
            where N : IComparisonOperators<N, N, bool>
        {
            var pivot = valueSelector(list[r]);
            int i = p - 1;

            for (int j = p; j < r; j++)
            {
                if (valueSelector(list[j]) <= pivot)
                {
                    i++;
                    Swap(list, i, j);
                }
            }
            i++;
            Swap(list, i, r);

            return i;
        }

        private static int Partition2<T, N>(IList<T> list, int p, int r, Func<T, N> valueSelector)
            where N : IComparisonOperators<N, N, bool>
        {
            var pivot = valueSelector(list[r]);
            int i = p - 1;

            for (int j = p; j < r; j++)
            {
                if (valueSelector(list[j]) >= pivot)
                {
                    i++;
                    Swap(list, i, j);
                }
            }
            i++;
            Swap(list, i, r);

            return i;
        }

        private static void Swap<T>(IList<T> list, int i, int j)
        {
            var temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
    }
}