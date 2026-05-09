namespace LR_3.SortingBenchmarking;

public static class Algorithms
{
    public static (int[] DataStructure, SortingBenchmarks Benchmarks) VanillaQuickSort(int[] data)
    {
        int[] arr = (int[])data.Clone();
        var benchmarks = new SortingBenchmarks();
        VanillaSort(arr, 0, arr.Length - 1, ref benchmarks);
        return (arr, benchmarks);
    }

    private static void VanillaSort(int[] arr, int low, int high, ref SortingBenchmarks b)
    {
        if (low >= high) return;
        int pivot = arr[high];
        int i = low - 1;
        for (int j = low; j < high; j++)
        {
            b.Comparisons++;
            if (arr[j] <= pivot)
                (arr[++i], arr[j]) = (arr[j], arr[i]);
        }

        (arr[i + 1], arr[high]) = (arr[high], arr[i + 1]);
        int p = i + 1;
        VanillaSort(arr, low, p - 1, ref b);
        VanillaSort(arr, p + 1, high, ref b);
    }

#pragma warning disable SA1202 // Elements should be ordered by access
    public static (int[] DataStructure, SortingBenchmarks Benchmarks) TripleMedianQuickSort(int[] data)
#pragma warning restore SA1202 // Elements should be ordered by access
    {
        int[] arr = (int[])data.Clone();
        var benchmarks = new SortingBenchmarks();
        MedianSort(arr, 0, arr.Length - 1, ref benchmarks);
        return (arr, benchmarks);
    }

    private static void MedianSort(int[] arr, int low, int high, ref SortingBenchmarks b)
    {
        int length = high - low + 1;

        if (length <= 1) return;

        if (length == 2)
        {
            b.Comparisons++;
            if (arr[low] > arr[high])
            {
                (arr[low], arr[high]) = (arr[high], arr[low]);
            }

            return;
        }

        if (length == 3)
        {
            b.Comparisons += 3;
            if (arr[low] > arr[low + 1])
                (arr[low], arr[low + 1]) = (arr[low + 1], arr[low]);

            if (arr[low + 1] > arr[high])
                (arr[low + 1], arr[high]) = (arr[high], arr[low + 1]);

            if (arr[low] > arr[low + 1])
                (arr[low], arr[low + 1]) = (arr[low + 1], arr[low]);

            return;
        }

        int mid = low + ((high - low) / 2);

        if (arr[low] > arr[mid]) (arr[low], arr[mid]) = (arr[mid], arr[low]);

        if (arr[mid] > arr[high])
        {
            (arr[mid], arr[high]) = (arr[high], arr[mid]);
            if (arr[low] > arr[mid]) (arr[low], arr[mid]) = (arr[mid], arr[low]);
        }

        (arr[mid], arr[high - 1]) = (arr[high - 1], arr[mid]);
        int pivot = arr[high - 1];

        int i = low;

        for (int j = low + 1; j < high - 1; j++)
        {
            b.Comparisons++;
            if (arr[j] <= pivot)
                (arr[++i], arr[j]) = (arr[j], arr[i]);
        }

        (arr[i + 1], arr[high - 1]) = (arr[high - 1], arr[i + 1]);
        int p = i + 1;

        MedianSort(arr, low, p - 1, ref b);
        MedianSort(arr, p + 1, high, ref b);
    }

#pragma warning disable SA1202 // Elements should be ordered by access
    public static (int[] DataStructure, SortingBenchmarks Benchmarks) TriplePivotQuickSort(int[] data)
#pragma warning restore SA1202 // Elements should be ordered by access
    {
        int[] arr = (int[])data.Clone();
        var benchmarks = new SortingBenchmarks();
        ThreePivotSort(arr, 0, arr.Length - 1, ref benchmarks);
        return (arr, benchmarks);
    }

    private static void ThreePivotSort(int[] arr, int low, int high, ref SortingBenchmarks b)
    {
        int length = high - low + 1;

        if (length <= 1) return;

        if (length == 2)
        {
            b.Comparisons++;
            if (arr[low] > arr[high])
            {
                (arr[low], arr[high]) = (arr[high], arr[low]);
            }

            return;
        }

        if (length == 3)
        {
            b.Comparisons += 2;
            if (arr[low] > arr[low + 1]) (arr[low], arr[low + 1]) = (arr[low + 1], arr[low]);

            if (arr[low + 1] > arr[high])
            {
                (arr[low + 1], arr[high]) = (arr[high], arr[low + 1]);
                b.Comparisons++;
                if (arr[low] > arr[low + 1]) (arr[low], arr[low + 1]) = (arr[low + 1], arr[low]);
            }

            return;
        }

        if (arr[low] > arr[low + 1]) (arr[low], arr[low + 1]) = (arr[low + 1], arr[low]);
        if (arr[low] > arr[high]) (arr[low], arr[high]) = (arr[high], arr[low]);
        if (arr[low + 1] > arr[high]) (arr[low + 1], arr[high]) = (arr[high], arr[low + 1]);

        int pivot1 = arr[low];
        int pivot2 = arr[low + 1];
        int pivot3 = arr[high];

        int i = low + 2;
        int j = low + 2;
        int k = high - 1;
        int m = high - 1;

        while (j <= k)
        {
            while (j <= k)
            {
                b.Comparisons++;
                if (arr[j] < pivot2)
                {
                    b.Comparisons++;
                    if (arr[j] < pivot1)
                    {
                        (arr[i], arr[j]) = (arr[j], arr[i]);
                        i++;
                    }

                    j++;
                }
                else break;
            }

            while (j <= k)
            {
                b.Comparisons++;
                if (arr[k] >= pivot2)
                {
                    b.Comparisons++;
                    if (arr[k] > pivot3)
                    {
                        (arr[k], arr[m]) = (arr[m], arr[k]);
                        m--;
                    }

                    k--;
                }
                else break;
            }

            if (j < k)
            {
                b.Comparisons++;
                if (arr[j] > pivot3)
                {
                    b.Comparisons++;
                    if (arr[k] < pivot1)
                    {
                        (arr[j], arr[i]) = (arr[i], arr[j]);
                        (arr[i], arr[k]) = (arr[k], arr[i]);
                        i++;
                    }
                    else
                    {
                        (arr[j], arr[k]) = (arr[k], arr[j]);
                    }

                    (arr[k], arr[m]) = (arr[m], arr[k]);
                    m--;
                }
                else
                {
                    b.Comparisons++;
                    if (arr[k] < pivot1)
                    {
                        (arr[j], arr[i]) = (arr[i], arr[j]);
                        (arr[i], arr[k]) = (arr[k], arr[i]);
                        i++;
                    }
                    else
                    {
                        (arr[j], arr[k]) = (arr[k], arr[j]);
                    }
                }

                j++;
                k--;
            }
        }

        (arr[low + 1], arr[j - 1]) = (arr[j - 1], arr[low + 1]);
        if (j - 1 > i - 1)
        {
            (arr[low + 1], arr[i - 1]) = (arr[i - 1], arr[low + 1]);
        }

        (arr[low], arr[i - 2]) = (arr[i - 2], arr[low]);

        m++;
        (arr[high], arr[m]) = (arr[m], arr[high]);

        int p1 = i - 2;
        int p2 = j - 1;
        int p3 = m;

        ThreePivotSort(arr, low, p1 - 1, ref b);
        ThreePivotSort(arr, p1 + 1, p2 - 1, ref b);
        ThreePivotSort(arr, p2 + 1, p3 - 1, ref b);
        ThreePivotSort(arr, p3 + 1, high, ref b);
    }
}
