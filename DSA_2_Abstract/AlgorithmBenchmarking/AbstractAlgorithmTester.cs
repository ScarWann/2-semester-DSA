namespace DSA_2_Abstract.AlgorithmBenchmarking;

public class AbstractAlgorithmTester<TContainer, TBenchmarks>
    where TContainer : new()
    where TBenchmarks : struct
{
    private readonly Algorithm<TContainer, TBenchmarks>? algorithm;
    private readonly Algorithm<TContainer, TBenchmarks>[]? algorithms;

    public AbstractAlgorithmTester(TContainer data, Algorithm<TContainer, TBenchmarks> algorithm)
    {
        this.Container = data;
        this.algorithm = algorithm;
    }

    public AbstractAlgorithmTester(TContainer[] data, Algorithm<TContainer, TBenchmarks> algorithm)
    {
        this.Containers = data;
        this.algorithm = algorithm;
    }

    public AbstractAlgorithmTester(TContainer data, Algorithm<TContainer, TBenchmarks>[] algorithms)
    {
        this.Container = data;
        this.algorithms = algorithms;
    }

    public AbstractAlgorithmTester(Algorithm<TContainer, TBenchmarks>[] algorithms)
    {
        this.algorithms = algorithms;
    }

    public AbstractAlgorithmTester(Algorithm<TContainer, TBenchmarks> algorithm)
    {
        this.algorithm = algorithm;
    }

    public TContainer[]? Containers { get; }

    public TContainer? Container { get; }

    public void Plot()

    public virtual TContainer Generate<TGenerationData>(TGenerationData generationData)
        where TGenerationData : class
    => new TContainer();

    public static AbstractAlgorithmTester<TContainer, TBenchmarks> Create<TContainer, TBenchmarks>(Algorithm<TContainer, TBenchmarks> algorithm, TContainer[] data)
        where TContainer : new()
        where TBenchmarks : struct
    => new(data, algorithm);
}
