namespace LR_3.DSL;

public class Algorithm<TDataStructure, TBenchmarks>(
    Func<TDataStructure, (TDataStructure DataStructure, TBenchmarks Benchmarks)> algorithm,
    CompositeComplexity? complexity = default,
    string name = "")
where TBenchmarks : struct
{
    private readonly string name = name;

    public Func<TDataStructure, (TDataStructure DataStructure, TBenchmarks Benchmarks)> AlgorithmFunc { get; } = algorithm;

    public CompositeComplexity? Complexity { get; } = complexity;

    public TDataStructure GetDataStructureResults(TDataStructure inputStructure) => this.AlgorithmFunc(inputStructure).DataStructure;

    public TBenchmarks GetBenchmarks(TDataStructure inputStructure) => this.AlgorithmFunc(inputStructure).Benchmarks;

    public (TDataStructure DataStructure, TBenchmarks Benchmarks) GetCompositeResults(TDataStructure inputStructure) => this.AlgorithmFunc(inputStructure);

    public int? Omega(int n) => this.Complexity?.Omega(n);

    public int? Omicron(int n) => this.Complexity?.Omicron(n);

    public int? Theta(int n) => this.Complexity?.Theta(n);

    public override string ToString()
    {
        return this.name;
    }
}

public static class Algorithm
{
    public static Algorithm<TDataStructure, TBenchmarks>
    Create<TDataStructure, TBenchmarks>(
        Func<TDataStructure, (TDataStructure DataStructure, TBenchmarks Benchmarks)> algorithm,
        CompositeComplexity? complexity = default)
    where TBenchmarks : struct
    => new Algorithm<TDataStructure, TBenchmarks>(algorithm, complexity);

    public static Algorithm<TDataStructure, TBenchmarks>
    Create<TDataStructure, TBenchmarks>(
        Func<TDataStructure, (TDataStructure DataStructure, TBenchmarks Benchmarks)> algorithm,
        Func<int, int> omega,
        Func<int, int> omicron,
        Func<int, int> theta)
    where TBenchmarks : struct
    => Create(algorithm, new CompositeComplexity(omega, omicron, theta));

    public static Algorithm<TDataStructure, TBenchmarks>
    Create<TDataStructure, TBenchmarks>(
        Func<TDataStructure, (TDataStructure DataStructure, TBenchmarks Benchmarks)> algorithm,
        string name,
        CompositeComplexity? complexity = default)
    where TBenchmarks : struct
    => new Algorithm<TDataStructure, TBenchmarks>(algorithm, complexity, name);

    public static Algorithm<TDataStructure, TBenchmarks>
    Create<TDataStructure, TBenchmarks>(
        Func<TDataStructure, (TDataStructure DataStructure, TBenchmarks Benchmarks)> algorithm,
        Func<int, int> omega,
        Func<int, int> omicron,
        Func<int, int> theta,
        string name)
    where TBenchmarks : struct
    => Create(algorithm, name, new CompositeComplexity(omega, omicron, theta));
}
