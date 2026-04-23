namespace DSA_2_Generic.AlgorithmBenchmarking;

public class GenericContainerRangeGenerationData<TContainer, TContainerGenerationData, TTypes>(int from, int to, int step = 1, TTypes? type = default)
where TContainer : class
where TContainerGenerationData : GenericContainerGenerationData<TContainer, TTypes>
where TTypes : Enum
{
    private readonly int from = from;
    private readonly int to = to;
    private readonly int step = step;

    public TTypes? Type { get; } = type;

    public IEnumerable<int> GetRange()
        => Helpers.Range(this.from, this.to, this.step);

    public TContainer[] GenerateContainerRange()
        => Helpers.Range(this.from, this.to, this.step)
                  .Select(i => this.CreateGenerationData(this.Type, i).Generate())
                  .ToArray();

    public TContainer[][] GenerateContainerRange(int amount)
        => Helpers.Range(this.from, this.to, this.step)
                  .Select(i => this.CreateGenerationData(this.Type, i).Generate(amount))
                  .ToArray();

    public virtual TContainerGenerationData CreateGenerationData(TTypes? type, int size)
        => throw new NotImplementedException("Container generation not implemented.");
}
