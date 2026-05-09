using System.Data.SqlTypes;

namespace DSL;

public abstract class AbstractGenerator<TDataStructure, TTypes>(int from, int to, int step = 1, TTypes? type = default)
where TDataStructure : class
where TTypes : Enum
{
    protected readonly int from = from;
    protected readonly int to = to;
    protected readonly int step = step;

    public TTypes? Type { get; } = type;

    public IEnumerable<int> GetRange()
        => Helpers.Range(this.from, this.to, this.step);

    public TDataStructure[] GenerateDataStructureRange()
        => Helpers.Range(this.from, this.to, this.step)
                  .Select(i => this.Generate(i, this.Type))
                  .ToArray();

    public TDataStructure[][] GenerateDataStructureRange(int amount)
        => Helpers.Range(this.from, this.to, this.step)
                  .Select(i => this.Generate(i, amount, this.Type))
                  .ToArray();

    public abstract TDataStructure Generate(int size, TTypes? type = default);

    public TDataStructure[] Generate(int size, int amount, TTypes? type = default)
        => Enumerable.Range(0, amount)
                     .Select(_ => this.Generate(size, type))
                     .ToArray();
}
