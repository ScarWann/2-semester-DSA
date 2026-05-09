using DSL;

namespace LR_3.SortingBenchmarking;

public class ArrayGenerator(int to = 1, int from = 1, int step = 1, ArrayType type = default) : AbstractGenerator<int[], ArrayType>(from, to, step, type)
{
    public override int[] Generate(int size, ArrayType type)
    => type switch
    {
        ArrayType.Ascending => Helpers.Range(0, size, 1).ToArray(),
        ArrayType.Descending => Helpers.Range(0, size, 1).Reverse().ToArray(),
        ArrayType.Random => Helpers.Range(0, size, 1).Shuffle().ToArray(),
        _ => throw new NotImplementedException($"Generation for {type} not implemented"),
    };
}
