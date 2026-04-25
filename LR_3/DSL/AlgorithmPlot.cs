using ScottPlot;

namespace LR_3.DSL;

public class AlgorithmPlot<TDataStructure, TGenerator, TTypes, TBenchmarks> : Plot
where TDataStructure : class
where TGenerator : AbstractGenerator<TDataStructure, TTypes>
where TTypes : Enum
where TBenchmarks : struct
{
    public AlgorithmPlot()
    : base()
    {
        this.Legend.Alignment = Alignment.UpperLeft;
    }

    public void AddComplexities(
        CompositeComplexity? complexity,
        TGenerator generationData,
        string name = "")
    {
        ArgumentNullException.ThrowIfNull(complexity);
        var safeComplexity = (CompositeComplexity)complexity;

        int[] generationRange = generationData.GetRange().ToArray();
        var omega = this.Add.Scatter(
            generationRange,
            generationRange.Select(e => safeComplexity.Omega(e))
                           .ToArray());
        omega.LegendText = (name == string.Empty) ? "Omega" : name + " Omega";

        var omicron = this.Add.Scatter(
            generationRange,
            generationRange.Select(e => safeComplexity.Omicron(e))
                           .ToArray());
        omicron.LegendText = (name == string.Empty) ? "Omicron" : name + " Omicron";

        var theta = this.Add.Scatter(
            generationRange,
            generationRange.Select(e => safeComplexity.Theta(e))
                           .ToArray());
        theta.LegendText = (name == string.Empty) ? "Theta" : name + " Theta";
    }

    public void AddAlgorithm(
        Algorithm<TDataStructure, TBenchmarks> algorithm,
        Func<TBenchmarks, int> selector,
        TGenerator generationData,
        int testCount = 1,
        bool addComplexities = false)
    {
        int[] generationRange = generationData.GetRange().ToArray();
        if (generationData.Type != null)
        {
            this.Add.Scatter(
            generationRange,
            generationData.GenerateDataStructureRange(testCount)
                          .Select(e => e
                          .Average(e => selector(algorithm.GetBenchmarks(e))))
                          .ToArray());
        }
        else
        {
            foreach (var val in Enum.GetValues(typeof(TTypes)).Cast<TTypes>())
            {
                var graph = this.Add.Scatter(
                generationRange,
                generationRange.Select(i => generationData.Generate(val, i, testCount))
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
    /// Apply AddComplexities separately if different algorithms have the same expected complexities to avoid cluttering the graph.
    /// </summary>
    public void AddAlgorithms(
        Algorithm<TDataStructure, TBenchmarks>[] algorithms,
        Func<TBenchmarks, int> selector,
        TGenerator generationData,
        int testCount = 1,
        bool addComplexities = false)
    {
        foreach (var algorithm in algorithms)
        {
            this.AddAlgorithm(algorithm, selector, generationData, testCount, addComplexities);
        }
    }
}
