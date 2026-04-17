namespace DSA_2_Abstract;

public class AbstractAlgorithmTester<TContainer, TElement>(TContainer[] data, Algorithm<TContainer, TElement> algorithm)
    where TContainer : IList<TElement>
{
    private readonly Algorithm<TContainer, TElement> algorithm = algorithm;

    public TContainer[] InitialData { get; } = data;

    public TContainer[] ResultData { get; } = data.Select(algorithm.Run).ToArray();

    public static AbstractAlgorithmTester<TContainer, TElement> Create<TContainer, TElement>(Algorithm<TContainer, TElement> algorithm, TContainer[] data)
    where TContainer : IList<TElement>
    => new(data, algorithm);
}
