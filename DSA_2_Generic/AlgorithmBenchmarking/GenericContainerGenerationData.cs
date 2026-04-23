namespace DSA_2_Generic.AlgorithmBenchmarking;

public class GenericContainerGenerationData<TContainer, TTypes>(TTypes type, uint size)
where TContainer : class
where TTypes : Enum
{
    public TTypes Type { get; } = type;

    public uint Size { get; } = size;

    public virtual TContainer Generate()
    {
        throw new NotImplementedException("Generator not implemented");
    }

    public TContainer[] Generate(int amount)
        => Enumerable.Range(0, amount)
                     .Select(_ => this.Generate())
                     .ToArray();
}
