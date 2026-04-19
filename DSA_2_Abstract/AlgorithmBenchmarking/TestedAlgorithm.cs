namespace DSA_2_Abstract.AlgorithmBenchmarking;

public class TestedAlgorithm<TContainer, TBenchmarks>(TContainer data, Algorithm<TContainer, TBenchmarks> algorithm, bool saveData = false)
    where TContainer : IAutoGenerateable<TContainer>
    where TBenchmarks : struct
{
    private readonly Algorithm<TContainer, TBenchmarks> algorithm = algorithm;

    public (TContainer Container, TBenchmarks Benchmarks) CompositeResult { get; set; } = saveData ? algorithm.GetCompositeResults(data) : default;

    public static TestedAlgorithm<TContainer, TBenchmarks> Create<TContainer, TBenchmarks>(Algorithm<TContainer, TBenchmarks> algorithm, TContainer data)
        where TContainer : IAutoGenerateable<TContainer>
        where TBenchmarks : struct
        => new(data, algorithm);

    public override bool Equals(object? obj)
    {
        if (obj == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        if (obj.GetType() == typeof((TContainer Container, TBenchmarks Benchmarks)))
        {
            return this.CompositeResult.Equals(obj);
        }

        if (obj.GetType() == typeof(TContainer))
        {
            return this.CompositeResult.Container.Equals(obj);
        }

        if (obj.GetType() == typeof(TBenchmarks))
        {
            return this.CompositeResult.Benchmarks.Equals(obj);
        }

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
