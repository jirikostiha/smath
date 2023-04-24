using System.Numerics;

namespace SMath
{
    /// <summary>
    /// 
    /// </summary>
    public static class ListExtension
    {


        //https://afteracademy.com/blog/kth-smallest-element-in-an-array/
        //https://www.dotnetlovers.com/article/10252/find-kth-smallest-and-largest-element-in-an-unsorted-array
        //https://www.geeksforgeeks.org/kth-smallest-largest-element-in-unsorted-array/

        // Standard partition process of QuickSort.
        // It considers the last element as pivot
        // and moves all smaller element to left of
        // it and greater elements to right
        public static int partition(int[] arr, int l, int r)
        {
            int x = arr[r], i = l;
            int temp = 0;
            for (int j = l; j <= r - 1; j++)
            {

                if (arr[j] <= x)
                {
                    // Swapping arr[i] and arr[j]
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;

                    i++;
                }
            }

            // Swapping arr[i] and arr[r]
            temp = arr[i];
            arr[i] = arr[r];
            arr[r] = temp;

            return i;
        }

        // This function returns k'th smallest
        // element in arr[l..r] using QuickSort
        // based method. ASSUMPTION: ALL ELEMENTS
        // IN ARR[] ARE DISTINCT
        public static int kthSmallest(int[] arr, int l, int r,
                                      int K)
        {
            // If k is smaller than number
            // of elements in array
            if (K > 0 && K <= r - l + 1)
            {
                // Partition the array around last
                // element and get position of pivot
                // element in sorted array
                int pos = partition(arr, l, r);

                // If position is same as k
                if (pos - l == K - 1)
                    return arr[pos];

                // If position is more, recur for
                // left subarray
                if (pos - l > K - 1)
                    return kthSmallest(arr, l, pos - 1, K);

                // Else recur for right subarray
                return kthSmallest(arr, pos + 1, r,
                                   K - pos + l - 1);
            }

            // If k is more than number
            // of elements in array
            return int.MaxValue;
        }

        //priority queue
        //binary search
    }
}
