namespace DSA_2_Abstract;

public class TestedAlgorithm<TContainer, TElement>(TContainer data, Algorithm<TContainer, TElement> algorithm, bool saveData)
    where TContainer : IList<TElement>
{
    private readonly Algorithm<TContainer, TElement> algorithm = algorithm;

    public TContainer? DataResult { get; set; } = saveData ? algorithm.Run(data) : default;

    public static TestedAlgorithm<TContainer, TElement> Create<TContainer, TElement>(Algorithm<TContainer, TElement> algorithm, TContainer data)
    where TContainer : IList<TElement>
    => new(data, algorithm);

    public override bool Equals(object? obj)
    {
        if (obj == null || this.Data == null || this.GetType() != obj.GetType())
        {
            return false;
        }

        if (obj.GetType() == typeof(TContainer))
        {
            return this.Data.Equals(obj);
        }

        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
