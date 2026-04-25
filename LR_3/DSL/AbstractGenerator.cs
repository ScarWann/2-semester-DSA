namespace LR_3.DSL;

public abstract class AbstractGenerator<TDataStructure, TTypes>(TTypes type, int from, int to, int step = 1)
where TDataStructure : class
where TTypes : Enum
{
    protected readonly int from = from;
    protected readonly int to = to;
    protected readonly int step = step;

    public TTypes Type { get; } = type;

    public IEnumerable<int> GetRange()
        => Helpers.Range(this.from, this.to, this.step);

    public TDataStructure[] GenerateDataStructureRange()
        => Helpers.Range(this.from, this.to, this.step)
                  .Select(i => this.Generate(this.Type, i))
                  .ToArray();

    public TDataStructure[][] GenerateDataStructureRange(int amount)
        => Helpers.Range(this.from, this.to, this.step)
                  .Select(i => this.Generate(this.Type, i, amount))
                  .ToArray();

    public abstract TDataStructure Generate(TTypes type, int size);

    public TDataStructure[] Generate(TTypes type, int size, int amount)
        => Enumerable.Range(0, amount)
                     .Select(_ => this.Generate(type, size))
                     .ToArray();
}
