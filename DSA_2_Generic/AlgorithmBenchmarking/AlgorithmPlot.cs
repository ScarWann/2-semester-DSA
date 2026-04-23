using ScottPlot;

namespace DSA_2_Generic.AlgorithmBenchmarking;

public class AlgorithmPlot<TContainer, TContainerRangeGenerationData, TContainerGenerationData, TTypes, TBenchmarks>(Plot? plot)
where TContainer : class
where TContainerRangeGenerationData : GenericContainerRangeGenerationData<TContainer, TContainerGenerationData, TTypes>
where TContainerGenerationData : GenericContainerGenerationData<TContainer, TTypes>
where TTypes : Enum
where TBenchmarks : struct
{
    public Plot Plot { get; } = plot ??= new Plot();

    public void AddComplexities(
        CompositeComplexity complexity,
        TContainerRangeGenerationData generationData,
        string name = "")
    {
        int[] generationRange = generationData.GetRange().ToArray();
        var omega = this.Plot.Add.Scatter(
            generationRange,
            generationRange.Select(e => complexity.Omega(e))
                           .ToArray());
        omega.LegendText = (name == string.Empty) ? "Omega" : name + " Omega";

        var omicron = this.Plot.Add.Scatter(
            generationRange,
            generationRange.Select(e => complexity.Omicron(e))
                           .ToArray());
        omicron.LegendText = (name == string.Empty) ? "Omicron" : name + " Omicron";

        var theta = this.Plot.Add.Scatter(
            generationRange,
            generationRange.Select(e => complexity.Theta(e))
                           .ToArray());
        theta.LegendText = (name == string.Empty) ? "Theta" : name + " Theta";
    }

    public void AddAlgorithm(
        Algorithm<TContainer, TBenchmarks> algorithm,
        Func<TBenchmarks, int> selector,
        TContainerRangeGenerationData generationData,
        int testCount = 1,
        bool addComplexities = false)
    {
        int[] generationRange = generationData.GetRange().ToArray();
        if (generationData.Type != null)
        {
            this.Plot.Add.Scatter(
            generationRange,
            generationData.GenerateContainerRange(testCount)
                          .Select(e => e
                          .Average(e => selector(algorithm.GetBenchmarks(e))))
                          .ToArray());
        }
        else
        {
            foreach (var val in Enum.GetValues(typeof(TTypes)).Cast<TTypes>())
            {
                var graph = this.Plot.Add.Scatter(
                generationRange,
                generationRange.Select(i => generationData.CreateGenerationData(val, i).Generate(testCount))
                               .ToArray()
                               .Select(e => e
                               .Average(e => selector(algorithm.GetBenchmarks(e))))
                               .ToArray());
                graph.LegendText = (algorithm.ToString() == string.Empty) ? val.ToString() : $"{algorithm} {val}";
            }
        }

        if (addComplexities) this.AddComplexities(algorithm.Complexity, generationData, algorithm.ToString());
    }

    /// <summary>
    /// Be aware that using addComplexities = true will add a complexity for every algorithm, not once.
    /// Apply AddComplexities separately if different algorithms have the same expected complexities.
    /// </summary>
    public void AddAlgorithms(
        Algorithm<TContainer, TBenchmarks>[] algorithms,
        Func<TBenchmarks, int> selector,
        TContainerRangeGenerationData generationData,
        int testCount = 1,
        bool addComplexities = false)
    {
        foreach (var algorithm in algorithms)
        {
            this.AddAlgorithm(algorithm, selector, generationData, testCount, addComplexities);
        }
    }
}
