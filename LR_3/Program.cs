using LR_3.DSL;
using LR_3.SortingBenchmarking;
using ScottPlot;
using SortingPlot = LR_3.DSL.AlgorithmPlot<int[], LR_3.SortingBenchmarking.ArrayGenerator, LR_3.SortingBenchmarking.ArrayType, LR_3.SortingBenchmarking.SortingBenchmarks>;

namespace LR_3;

public class Program
{
    public static void Main()
    {
        var plot = new SortingPlot();
        plot.AddComplexities(
            new CompositeComplexity(x => (int)(x * Math.Log(x)), x => (int)(x * Math.Sqrt(x)), x => x * 10),
            new ArrayGenerator(ArrayType.Random, 1, 100));
        plot.SavePng("aaaaa.png", 800, 600);
    }
}
