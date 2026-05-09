using DSL;
using LR_3.SortingBenchmarking;

namespace LR_3;

public class Program
{
    public static void Main(string[] args)
    {
        // DrawAlgorithms();
        // PollTest(args[0]);
        RunTests();
    }

    public static void PollTest(string filename)
    {
        string[] file = File.ReadAllLines(filename);
        var vanillaQuicksort = new SortingAlgorithm(Algorithms.VanillaQuickSort);
        var tripleMedianQuicksort = new SortingAlgorithm(Algorithms.TripleMedianQuickSort);
        var triplePivotQuicksort = new SortingAlgorithm(Algorithms.TriplePivotQuickSort);
        int[] arr = file.Skip(1).Select(int.Parse).ToArray();
        string newFilename = filename.Replace("input", "output");
        File.WriteAllText(newFilename, vanillaQuicksort.GetBenchmarks(arr).Comparisons.ToString() + ' ');
        File.AppendAllText(newFilename, tripleMedianQuicksort.GetBenchmarks(arr).Comparisons.ToString() + ' ');
        File.AppendAllText(newFilename, triplePivotQuicksort.GetBenchmarks(arr).Comparisons.ToString());
    }

    public static void DrawAlgorithms()
    {
        var quickSortComplexity = new CompositeComplexity(i => (int)(i * Math.Log(i)), i => i * i, i => (int)(i * Math.Log(i)));
        var vanillaQuicksort = new SortingAlgorithm(Algorithms.VanillaQuickSort, quickSortComplexity, "Vanilla QuickSort");
        var tripleMedianQuicksort = new SortingAlgorithm(Algorithms.TripleMedianQuickSort, quickSortComplexity, "3Median QuickSort");
        var triplePivotQuicksort = new SortingAlgorithm(Algorithms.TriplePivotQuickSort, quickSortComplexity, "3Pivot QuickSort");
        SortingAlgorithm[] algorithms = [vanillaQuicksort, tripleMedianQuicksort, triplePivotQuicksort];

        // Solo algorithm graphs. Complexities included.
        var generator = new ArrayGenerator(1000, step: 10);
        var soloPlot = new SortingPlot();
        soloPlot.AddAlgorithm(vanillaQuicksort, e => e.Comparisons, generator, addComplexities: true);
        soloPlot.SavePng("graphs/standalone/vanillaComplexities.png", 400, 300);
        soloPlot.Clear();

        soloPlot.AddAlgorithm(tripleMedianQuicksort, e => e.Comparisons, generator, addComplexities: true);
        soloPlot.SavePng("graphs/standalone/3MedianComplexities.png", 400, 300);
        soloPlot.Clear();

        soloPlot.AddAlgorithm(triplePivotQuicksort, e => e.Comparisons, generator, addComplexities: true);
        soloPlot.SavePng("graphs/standalone/3PivotComplexities.png", 400, 300);
        soloPlot.Clear();

        // Algorithm comparisons.
        PlotComparison(10, 1, 100);
        PlotComparison(100, 5, 100);
        PlotComparison(1000, 10, 50);
    }

    public static void PlotComparison(int size, int step = 1, int testCount = 1)
    {
        var vanillaQuicksort = new SortingAlgorithm(Algorithms.VanillaQuickSort, name: "Vanilla QuickSort");
        var tripleMedianQuicksort = new SortingAlgorithm(Algorithms.TripleMedianQuickSort, name: "3Median QuickSort");
        var triplePivotQuicksort = new SortingAlgorithm(Algorithms.TriplePivotQuickSort, name: "3Pivot QuickSort");
        SortingAlgorithm[] algorithms = [vanillaQuicksort, tripleMedianQuicksort, triplePivotQuicksort];

        var plot = new SortingPlot();
        var generator = new ArrayGenerator(size, step, type: ArrayType.Random);
        plot.AddAlgorithms(algorithms, e => e.Comparisons, generator, testCount);
        plot.SavePng($"graphs/comparisons/{size}size.png", 400, 300);
        plot.Clear();
    }

    public static void RunTests()
    {
        int[] tests = [10, 100, 1000, 5000, 10000, 20000, 50000];
        foreach (var item in Enum.GetValues(typeof(ArrayType)).Cast<ArrayType>().Skip(3))
        {
            Console.WriteLine(item + ":");
            PrintBenchMarks(tests, item);
        }
    }

    public static void PrintBenchMarks(int[] tests, ArrayType type)
    {
        var gen = new ArrayGenerator();
        var vanillaQuicksort = new SortingAlgorithm(Algorithms.VanillaQuickSort);
        var tripleMedianQuicksort = new SortingAlgorithm(Algorithms.TripleMedianQuickSort);
        var triplePivotQuicksort = new SortingAlgorithm(Algorithms.TriplePivotQuickSort);
        SortingAlgorithm[] algorithms = [vanillaQuicksort, tripleMedianQuicksort, triplePivotQuicksort];
        foreach (var i in tests)
        {
            var arr = gen.Generate(i, type);
            Console.Write(vanillaQuicksort.GetBenchmarks(arr).Comparisons + " ");
            Console.Write(tripleMedianQuicksort.GetBenchmarks(arr).Comparisons + " ");
            Console.WriteLine(triplePivotQuicksort.GetBenchmarks(arr).Comparisons);
        }
    }
}
