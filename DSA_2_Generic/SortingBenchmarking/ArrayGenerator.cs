using DSA_2_Generic.AlgorithmBenchmarking;

namespace DSA_2_Generic.SortingBenchmarking;

public class ArrayGenerator(ArrayType type, int from, int to, int step = 1) : Generator<int[], ArrayType>(type, from, to, step)
{
    public override int[] Generate(ArrayType type, int size)
    => type switch
    {
        ArrayType.Ascending => Helpers.Range(0, size, 1).ToArray(),
        ArrayType.Descending => Helpers.Range(0, size, 1).Reverse().ToArray(),
        ArrayType.Random => Helpers.Range(0, size, 1).Shuffle().ToArray(),
        _ => throw new NotImplementedException($"Generation for {type} not implemented"),
    };
}
