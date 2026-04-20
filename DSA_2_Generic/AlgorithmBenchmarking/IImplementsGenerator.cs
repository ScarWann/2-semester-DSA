namespace DSA_2_Generic.AlgorithmBenchmarking;

public interface IImplementsGenerator<TContainer>
{
    static abstract TContainer Generate<TGenerationData>(TGenerationData generationData)
    where TGenerationData : struct;
}
