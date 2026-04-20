namespace DSA_2_Generic.AlgorithmBenchmarking;

public interface IGenerationData<TContainer>
where TContainer : class
{
    TContainer Generate();

    TContainer[] GenerateAllTypes();

    TContainer[][] GenerateAllTypes(int amount);

    int[] GetRange();
}
