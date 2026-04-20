using ScottPlot;

namespace DSA_2_Generic.AlgorithmBenchmarking;

public class GenericAlgorithmTester<TContainer, TBenchmarks>
    where TContainer : class
    where TBenchmarks : struct
{
    private readonly Algorithm<TContainer, TBenchmarks>? algorithm;
    private readonly Algorithm<TContainer, TBenchmarks>[]? algorithms;

    public GenericAlgorithmTester(TContainer data, Algorithm<TContainer, TBenchmarks> algorithm)
    {
        this.Container = data;
        this.algorithm = algorithm;
    }

    public GenericAlgorithmTester(TContainer[] data, Algorithm<TContainer, TBenchmarks> algorithm)
    {
        this.Containers = data;
        this.algorithm = algorithm;
    }

    public GenericAlgorithmTester(TContainer data, Algorithm<TContainer, TBenchmarks>[] algorithms)
    {
        this.Container = data;
        this.algorithms = algorithms;
    }

    public GenericAlgorithmTester(Algorithm<TContainer, TBenchmarks>[] algorithms)
    {
        this.algorithms = algorithms;
    }

    public GenericAlgorithmTester(Algorithm<TContainer, TBenchmarks> algorithm)
    {
        this.algorithm = algorithm;
    }

    public TContainer[]? Containers { get; }

    public TContainer? Container { get; }

    public Plot PlotAlgorithm(
        Algorithm<TContainer, TBenchmarks> algorithm,
        Func<TBenchmarks, int> selector,
        IGenerationData<TContainer> generationData,
        int testCount = 1)
    {
        Plot plot = new();
        plot.Legend.Alignment = Alignment.UpperLeft;
        plot.Add.Scatter(
            generationData.GetRange(),
            generationData.GenerateAllTypes(testCount)
                          .Select(e => e
                          .Select(e => selector(algorithm.GetBenchmarks(e)))
                          .Average())
                          .ToArray());
        return plot;
    }

    public static GenericAlgorithmTester<TContainer, TBenchmarks> Create<TContainer, TBenchmarks>(Algorithm<TContainer, TBenchmarks> algorithm, TContainer[] data)
        where TContainer : class
        where TBenchmarks : struct
    => new(data, algorithm);
}
