namespace DSA_2_Generic.AlgorithmBenchmarking;

public class Algorithm<TContainer, TBenchmarks>(Func<TContainer, (TContainer Container, TBenchmarks Benchmarks)> algorithm, CompositeComplexity complexity, string name = "")
where TBenchmarks : struct
{
    private readonly string name = name;

    public Func<TContainer, (TContainer Container, TBenchmarks Benchmarks)> AlgorithmFunc { get; } = algorithm;

    public CompositeComplexity Complexity { get; } = complexity;

    public TContainer GetContainerResults(TContainer inputStructure) => this.AlgorithmFunc(inputStructure).Container;

    public TBenchmarks GetBenchmarks(TContainer inputStructure) => this.AlgorithmFunc(inputStructure).Benchmarks;

    public (TContainer Container, TBenchmarks Benchmarks) GetCompositeResults(TContainer inputStructure) => this.AlgorithmFunc(inputStructure);

    public int Omega(int n) => this.Complexity.Omega(n);

    public int Omicron(int n) => this.Complexity.Omicron(n);

    public int Theta(int n) => this.Complexity.Theta(n);

    public override string ToString()
    {
        return this.name;
    }
}

public static class Algorithm
{
    public static Algorithm<TContainer, TBenchmarks>
    Create<TContainer, TBenchmarks>(Func<TContainer, (TContainer Container, TBenchmarks Benchmarks)> algorithm, CompositeComplexity complexity)
    where TBenchmarks : struct
    => new Algorithm<TContainer, TBenchmarks>(algorithm, complexity);

    public static Algorithm<TContainer, TBenchmarks>
    Create<TContainer, TBenchmarks>(
        Func<TContainer, (TContainer Container, TBenchmarks Benchmarks)> algorithm,
        Func<int, int> omega,
        Func<int, int> omicron,
        Func<int, int> theta)
    where TBenchmarks : struct
    => Create(algorithm, new CompositeComplexity(omega, omicron, theta));

    public static Algorithm<TContainer, TBenchmarks>
    Create<TContainer, TBenchmarks>(Func<TContainer, (TContainer Container, TBenchmarks Benchmarks)> algorithm, CompositeComplexity complexity, string name)
    where TBenchmarks : struct
    => new Algorithm<TContainer, TBenchmarks>(algorithm, complexity, name);

    public static Algorithm<TContainer, TBenchmarks>
    Create<TContainer, TBenchmarks>(
        Func<TContainer, (TContainer Container, TBenchmarks Benchmarks)> algorithm,
        Func<int, int> omega,
        Func<int, int> omicron,
        Func<int, int> theta,
        string name)
    where TBenchmarks : struct
    => Create(algorithm, new CompositeComplexity(omega, omicron, theta), name);
}
