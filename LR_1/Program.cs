using System.Drawing;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using ScottPlot;
namespace DSA_1;

public class Program
{
    static void Main() 
    {
        //DumpResults();

        //PlotGenericBenchmarks("10_Generic.png", 10, testCount: 20);
        //PlotGenericBenchmarks("100_Generic.png", 100);
        //PlotGenericBenchmarks("1000_Generic.png", 1000, stepSize: 10);

        //PlotAlghorithmBenchmarks("1000_NaiveBubble.png", 10000, NaiveBubbleSort, stepSize:  100);
        //PlotAlghorithmBenchmarks("1000_ModifiedBubble.png", 1000, ModifiedBubbleSort, stepSize: 10);
        //PlotAlghorithmBenchmarks("20000_SedgewickShell.png", 20000, SedgewickShellSort, stepSize: 200);
    }

    /// <summary>
    /// Defaults to InsertionSort if no sequence is provided
    /// </summary>
    public static (int Swaps, int Comparisons) GenericShellSort(ref int[] arr, int[]? comparisonSequence)
    {   
        int swaps = 0;
        int comparisons = 0;

        if (comparisonSequence == null) {
            for (int i = 1; i < arr.Length; ++i)
            {
                for (int j = i; (j >= 1) && (arr[j - 1] > arr[j]); j -= 1)
                {
                    swaps++;
                    comparisons++;
                    (arr[j], arr[j - 1]) = (arr[j - 1], arr[j]);
                }
                comparisons++;
            }
        } else
        {
            foreach (int step in comparisonSequence)
            {
                for (int i = step; i < arr.Length; ++i)
                {
                    for (int j = i; (j >= step) && (arr[j - step] > arr[j]); j -= step)
                    {
                        swaps++;
                        comparisons++;
                        (arr[j], arr[j - step]) = (arr[j - step], arr[j]);
                    }
                    comparisons++;
                }
            }
        }
        return (swaps, comparisons);
    }
    public static (int Swaps, int Comparisons) SedgewickShellSort(ref int[] arr)
    {
        return GenericShellSort(ref arr, GenerateSequence(arr.Length, GetSedgewickElement));
    }

    public static (int Swaps, int Comparisons) NaiveBubbleSort(ref int[] arr)
    {
        int swaps = 0;
        int comparisons = 0;

        for (int i = 0; i < arr.Length; i++)
        {
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    swaps++;
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
                comparisons++;
            }
            comparisons++;
        }
        
        return (swaps, comparisons);
    }

    public static (int Swaps, int Comparisons) ModifiedBubbleSort(ref int[] arr)
    {
        int swaps = 0;
        int comparisons = 0;

        bool finished = false;

        for (int i = 0; i < arr.Length && !finished; i++)
        {
            finished = true;
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    swaps++;
                    finished = false;
                    (arr[j], arr[j + 1]) = (arr[j + 1], arr[j]);
                }
                comparisons++;
            }
            comparisons++;
        }
        
        return (swaps, comparisons);
    }

    public static double[] RunBenchmarks(int[] benchmarks, SortingMethod sortingMethod, TestOrder testOrder, 
                                         int testCount = 1, TestType testType = TestType.Comparisons)
    {
        var intermediaryResults = new int[testCount, benchmarks.Length];
        for (int i = 0; i < testCount; i++)
        {
            switch (testOrder)
            {
                case TestOrder.Ascending:
                    for (int j = 0; j < benchmarks.Length; j++)
                    {
                        int[] arr = [.. Range(1, benchmarks[j], 1)];
                        if (testType == TestType.Swaps)
                        {
                            intermediaryResults[i,j] = sortingMethod(ref arr).Swaps;
                        } else
                        {
                            intermediaryResults[i,j] = sortingMethod(ref arr).Comparisons;
                        }
                    }
                    break;
                case TestOrder.Descending:
                    for (int j = 0; j < benchmarks.Length; j++)
                    {
                        int[] arr = [.. Range(1, benchmarks[j], -1)];
                        if (testType == TestType.Swaps)
                        {
                            intermediaryResults[i,j] = sortingMethod(ref arr).Swaps;
                        } else
                        {
                            intermediaryResults[i,j] = sortingMethod(ref arr).Comparisons;
                        }
                    }
                    break;
                case TestOrder.Random:
                    var rng = new Random();
                    for (int j = 0; j < benchmarks.Length; j++)
                    {
                        int[] arr = [.. Range(1, benchmarks[j], 1)];
                        rng.Shuffle(arr);
                        if (testType == TestType.Swaps)
                        {
                            intermediaryResults[i,j] = sortingMethod(ref arr).Swaps;
                        } else
                        {
                            intermediaryResults[i,j] = sortingMethod(ref arr).Comparisons;
                        }
                    }
                    break;
            }
        }
        var results = Enumerable.Range(0, benchmarks.Length)
            .Select(j => Enumerable.Range(0, testCount).Average(i => intermediaryResults[i, j]))
            .ToArray();
        return results;
    }

    public static void PlotGenericBenchmarks(string filename, int maxSize, int stepSize = 1, TestOrder testOrder = TestOrder.Random, 
                                                int testCount = 5, TestType testType = TestType.Comparisons)
    {
        int[] testRange = [.. Range(1, maxSize, stepSize)];
        Plot myPlot = new();
        myPlot.Legend.Alignment = Alignment.UpperLeft;

        double[] naiveBubblesortResult = RunBenchmarks(benchmarks: testRange, 
                                                    sortingMethod: NaiveBubbleSort, 
                                                    testOrder: testOrder,
                                                    testCount: testCount,
                                                    testType: testType);
        var nbp = myPlot.Add.Scatter(testRange, naiveBubblesortResult);
        nbp.ConnectStyle = ConnectStyle.Straight;
        nbp.LegendText = "Naive Bubblesort";

        double[] modifiedBubblesortResult = RunBenchmarks(benchmarks: testRange, 
                                                          sortingMethod: ModifiedBubbleSort, 
                                                          testOrder: testOrder,
                                                          testCount: testCount,
                                                          testType: testType);
        var mbp = myPlot.Add.Scatter(testRange, modifiedBubblesortResult);
        mbp.ConnectStyle = ConnectStyle.Straight;
        mbp.LegendText = "Modified Bubblesort";

        double[] sedgewickShellsortResult = RunBenchmarks(benchmarks: testRange, 
                                                          sortingMethod: SedgewickShellSort, 
                                                          testOrder: testOrder,
                                                          testCount: testCount,
                                                          testType: testType);
        var ssp = myPlot.Add.Scatter(testRange, sedgewickShellsortResult);
        ssp.ConnectStyle = ConnectStyle.Straight;
        ssp.LegendText = "Sedgewick Shellsort";

        myPlot.SavePng(filename, 400, 300);
    }

    public static void PlotAlghorithmBenchmarks(string filename, int maxSize, SortingMethod sortingMethod, int stepSize = 1, int testCount = 5, TestType testType = TestType.Comparisons)
    {
        int[] testRange = [.. Range(1, maxSize, stepSize)];
        Plot myPlot = new();
        myPlot.Legend.Alignment = Alignment.UpperLeft;

        double[] ascendingResults = RunBenchmarks(benchmarks: testRange, 
                                                 sortingMethod: sortingMethod, 
                                                 testOrder: TestOrder.Ascending,
                                                 testCount: 1,
                                                 testType: testType);
        var asp = myPlot.Add.Scatter(testRange, ascendingResults);
        asp.ConnectStyle = ConnectStyle.Straight;
        asp.LegendText = "Ascending";

        double[] descendingResults = RunBenchmarks(benchmarks: testRange, 
                                                   sortingMethod: sortingMethod, 
                                                   testOrder: TestOrder.Descending,
                                                   testCount: 1,
                                                   testType: testType);
        var dsp = myPlot.Add.Scatter(testRange, descendingResults);
        dsp.ConnectStyle = ConnectStyle.Straight;
        dsp.LegendText = "Descending";

        double[] averageResults = RunBenchmarks(benchmarks: testRange, 
                                                sortingMethod: sortingMethod, 
                                                testOrder: TestOrder.Random,
                                                testCount: testCount,
                                                testType: testType);
        var avp = myPlot.Add.Scatter(testRange, averageResults);
        avp.ConnectStyle = ConnectStyle.Straight;
        avp.LegendText = "Average";

        var worstFuncs = new Dictionary<string, Func<int, double>>
        {
            ["NaiveBubbleSort"]    = i => i * i,
            ["ModifiedBubbleSort"] = i => i * i,
            ["SedgewickShellSort"] = i => Math.Pow(i, 4.0/3.0)
        };
        var worstResults = testRange.Select(worstFuncs[sortingMethod.Method.Name]).ToArray();
        var wcp = myPlot.Add.Scatter(testRange, worstResults);
        wcp.ConnectStyle = ConnectStyle.Straight;
        wcp.LegendText = "Worst (Asymptotic)";

        var bestFuncs = new Dictionary<string, Func<int, double>>
        {
            ["NaiveBubbleSort"]    = i => i * i,
            ["ModifiedBubbleSort"] = i => i,
            ["SedgewickShellSort"] = i => i * Math.Log(i)
        };
        var bestResults = testRange.Select(bestFuncs[sortingMethod.Method.Name]).ToArray();
        var bcp = myPlot.Add.Scatter(testRange, bestResults);
        bcp.ConnectStyle = ConnectStyle.Straight;
        bcp.LegendText = "Best (Asymptotic)";

        myPlot.SavePng(filename, 800, 600);
    }

    public static void DumpResults()
    {
        SortingMethod[] sortingMethods = {NaiveBubbleSort, ModifiedBubbleSort, SedgewickShellSort};
        int[] sizes = [10, 100, 1000, 5000, 10000, 20000, 50000];

        Console.WriteLine("Ascending");
        foreach (var sortingMethod in sortingMethods)
        {
            Console.WriteLine(sortingMethod.Method.Name);
            Console.WriteLine(string.Join(", ", RunBenchmarks(sizes, sortingMethod, TestOrder.Ascending, testType: TestType.Comparisons)));
            Console.WriteLine(string.Join(", ", RunBenchmarks(sizes, sortingMethod, TestOrder.Ascending, testType: TestType.Swaps)));
        }

        Console.WriteLine("Descending");
        foreach (var sortingMethod in sortingMethods)
        {
            Console.WriteLine(sortingMethod.Method.Name);
            Console.WriteLine(string.Join(", ", RunBenchmarks(sizes, sortingMethod, TestOrder.Descending, testType: TestType.Comparisons)));
            Console.WriteLine(string.Join(", ", RunBenchmarks(sizes, sortingMethod, TestOrder.Descending, testType: TestType.Swaps)));
        }

        Console.WriteLine("Random");
        foreach (var sortingMethod in sortingMethods)
        {
            Console.WriteLine(sortingMethod.Method.Name);
            Console.WriteLine(string.Join(", ", RunBenchmarks(sizes, sortingMethod, TestOrder.Random, testCount: 5, testType: TestType.Comparisons)));
            Console.WriteLine(string.Join(", ", RunBenchmarks(sizes, sortingMethod, TestOrder.Random, testCount: 5,  testType: TestType.Swaps)));
        }
    }

    public static int GetSedgewickElement(int n)
    {
        if (n % 2 == 0)
        {
            return (int)(9 * (Math.Pow(2, n) - Math.Pow(2, n/2)) + 1);
        } else
        {
            return (int)(8 * Math.Pow(2, n) - 6 * Math.Pow(2, (n + 1) / 2) + 1);
        }
    }

    public static int[] GenerateSequence(int upto, Func<int, int> func)
    {
        List<int> sequence = [];
        for (int i = 0; func(i) < upto; i++)
        {
            sequence.Add(func(i));
        }
        sequence.Reverse();
        return [.. sequence];
    }

    public static bool IsSorted(int[] arr)
    {
        for (int i = 1; i < arr.Length; i++)
        {
            if (arr[i - 1] > arr[i])
            {
                return false;
            }
        }
        return true;
    }

    public static IEnumerable<int> Range(int min, int max, int step)
    {
        if (step > 0)
            for (int i = min; i <= max; i += step) yield return i;
        else if (step < 0)
            for (int i = max; i >= min; i += step) yield return i;
    }


    public delegate (int Swaps, int Comparisons) SortingMethod(ref int[] arr);
}

public enum TestOrder
{
    Ascending,
    Descending,
    Random
}

public enum TestType
{
    Swaps,
    Comparisons
}
