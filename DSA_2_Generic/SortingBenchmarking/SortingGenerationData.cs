using DSA_2_Generic.AlgorithmBenchmarking;

namespace DSA_2_Generic.SortingBenchmarking;

public class SortingGenerationData(int rangeMin, int rangeMax, int rangeStep, ArrayType arrayType) : IGenerationData<int[]>
{
    public int RangeMin { get; } = rangeMin;

    public int RangeMax { get; } = rangeMax;

    public int RangeStep { get; } = rangeStep;

    public ArrayType? ArrayType { get; } = arrayType;

    public int[] Generate()
    {
        Random random = new();
        
    }
}
