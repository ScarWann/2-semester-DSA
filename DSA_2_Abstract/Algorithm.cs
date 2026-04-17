namespace DSA_2_Abstract;

public class Algorithm<TContainer, TElement, TResultSet>(Func<TContainer, (TContainer Container, TResultSet ResultSet)> algorithm, CompositeComplexity complexity)
where TContainer : IList<TElement>
where TResultSet : ISet<int>
{
    private readonly CompositeComplexity complexity = complexity;

    public Func<TContainer, (TContainer Container, TResultSet ResultSet)> AlgorithmFunc { get; } = algorithm;

    public TContainer GetContainerResults(TContainer inputStructure) => this.AlgorithmFunc(inputStructure).Container;

    public TResultSet GetResultSet(TContainer inputStructure) => this.AlgorithmFunc(inputStructure).ResultSet;

    public (TContainer Container, TResultSet ResultSet) GetCompositeResults(TContainer inputStructure) => this.AlgorithmFunc(inputStructure);

    public int Omega(int n) => this.complexity.Omega(n);

    public int Omicron(int n) => this.complexity.Omicron(n);

    public int Theta(int n) => this.complexity.Theta(n);
}

public static class Algorithm
{
    public static Algorithm<TContainer, TElement, TResultSet>
    Create<TContainer, TElement, TResultSet>(Func<TContainer, (TContainer Container, TResultSet ResultSet)> algorithm, CompositeComplexity complexity)
    where TContainer : IList<TElement>
    where TResultSet : ISet<int>
    => new Algorithm<TContainer, TElement, TResultSet>(algorithm, complexity);

    public static Algorithm<TContainer, TElement, TResultSet> Create<TContainer, TElement, TResultSet>(
        Func<TContainer, TContainer> algorithm,
        Func<int, int> omega,
        Func<int, int> omicron,
        Func<int, int> theta)
    where TContainer : IList<TElement>
    where TResultSet : ISet<int>
    => Algorithm.Create(algorithm, new CompositeComplexity(omega, omicron, theta));
}