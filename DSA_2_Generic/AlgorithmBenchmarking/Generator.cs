namespace DSA_2_Generic.AlgorithmBenchmarking;

public class Generator<TContainer, TTypes>(TTypes type, int from, int to, int step = 1)
where TContainer : class
where TTypes : Enum
{
    protected readonly int from = from;
    protected readonly int to = to;
    protected readonly int step = step;

    public TTypes Type { get; } = type;

    public IEnumerable<int> GetRange()
        => Helpers.Range(this.from, this.to, this.step);

    public TContainer[] GenerateContainerRange()
        => Helpers.Range(this.from, this.to, this.step)
                  .Select(i => this.Generate(this.Type, i))
                  .ToArray();

    public TContainer[][] GenerateContainerRange(int amount)
        => Helpers.Range(this.from, this.to, this.step)
                  .Select(i => this.Generate(this.Type, i, amount))
                  .ToArray();

    public virtual TContainer Generate(TTypes type, int size)
    {
        throw new NotImplementedException("Generator not implemented");
    }

    public TContainer[] Generate(TTypes type, int size, int amount)
        => Enumerable.Range(0, amount)
                     .Select(_ => this.Generate(type, size))
                     .ToArray();
}
