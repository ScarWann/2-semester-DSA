using System.Drawing;
using DSL;
using LR_4.GraphBenchmarking;

namespace LR_4;

public class Program
{
    public static void Main(string[] args)
    {
        // DrawAlgorithms();
        PollTest(args);
    }

    public static void DrawAlgorithms()
    {
        var boruvkaAlgorithm = new SpanningAlgorithm(Algorithms.BoruvkaAlgorithm, new CompositeComplexity(i => (int)(i * i * Math.Log(i)), i => (int)(i * i * Math.Log(i)), i => (int)(i * i * Math.Log(i))));

        // Solo algorithm graphs. Complexities included.
        var generator = new GraphGenerator(100, step: 5);
        var soloPlot = new SpanningPlot();
        soloPlot.AddAlgorithm(boruvkaAlgorithm, e => e.Comparisons, generator, addComplexities: true, testCount: 5);
        soloPlot.SavePng("complexities.png", 400, 300);
        soloPlot.Clear();
    }

    public static void PollTest(string[] args)
    {
        bool randomMode = args.Contains("--random") || args.Contains("-r");

        int n;
        string? sizeArg = args.FirstOrDefault(a => !a.StartsWith('-'));

        if (sizeArg is not null && int.TryParse(sizeArg, out int argSize))
        {
            n = argSize;
        }
        else
        {
            Console.Write("Enter matrix size: ");
            n = int.Parse(Console.ReadLine()!);
        }

        Graph graph;

        if (randomMode)
        {
            var generator = new GraphGenerator();
            graph = generator.Generate(n, GraphType.Random);
            Console.WriteLine($"Generated random {n}x{n} graph:");
            Console.WriteLine(graph);
        }
        else
        {
            int[,] weights = new int[n, n];
            Console.WriteLine($"Enter the {n}x{n} weight matrix (space-separated rows, 0 = no edge):");
            for (int i = 0; i < n; i++)
            {
                string[] parts = Console.ReadLine()!.Trim().Split(' ', StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != n)
                {
                    Console.Error.WriteLine($"Error: expected {n} values on row {i}, got {parts.Length}.");
                    return;
                }

                for (int j = 0; j < n; j++)
                    weights[i, j] = int.Parse(parts[j]);
            }

            graph = new Graph(weights);
        }

        var (mst, benchmarks) = Algorithms.BoruvkaAlgorithm(graph);

        Console.WriteLine("\nMST weight matrix:");
        Console.WriteLine(mst);

        Console.WriteLine($"\nTotal MST weight : {mst.TotalWeight}");
        Console.WriteLine($"Comparisons      : {benchmarks.Comparisons}");

        return;
    }
}
