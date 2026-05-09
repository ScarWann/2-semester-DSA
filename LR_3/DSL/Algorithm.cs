namespace DSL;

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
