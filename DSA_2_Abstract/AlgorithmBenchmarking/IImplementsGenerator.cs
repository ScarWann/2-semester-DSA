namespace DSA_2_Abstract.AlgorithmBenchmarking;

public interface IImplementsGenerator<TContainer>
{
    static abstract TContainer Generate<TGenerationData>(TGenerationData generationData)
    where TGenerationData : struct;
}
