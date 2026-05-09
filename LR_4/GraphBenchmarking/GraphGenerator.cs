using DSL;

namespace LR_4.GraphBenchmarking;

public class GraphGenerator(int to = 1, int from = 1, int step = 1, GraphType type = default)
    : AbstractGenerator<Graph, GraphType>(from, to, step, type)
{
    public override Graph Generate(int size, GraphType type)
        => type switch
        {
            GraphType.Random => new Graph(GenerateRandom(size, density: 0.5)),
            GraphType.Dense => new Graph(GenerateRandom(size, density: 0.9)),
            GraphType.Sparse => new Graph(GenerateRandom(size, density: 0.1)),
            GraphType.Complete => new Graph(GenerateComplete(size)),
            _ => throw new NotImplementedException($"Generation for {type} not implemented"),
        };

    private static int[,] GenerateRandom(int size, double density)
    {
        var rng = new Random();
        var weights = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = i + 1; j < size; j++)
            {
                if (rng.NextDouble() < density)
                    weights[i, j] = weights[j, i] = rng.Next(1, 100);
            }
        }

        EnsureNoStrayVertices(weights, rng);
        return weights;
    }

    private static int[,] GenerateComplete(int size)
    {
        var rng = new Random();
        var weights = new int[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = i + 1; j < size; j++)
                weights[i, j] = weights[j, i] = rng.Next(1, 100);
        }

        EnsureNoStrayVertices(weights, rng);
        return weights;
    }

    private static void EnsureNoStrayVertices(int[,] weights, Random rng)
    {
        int size = weights.GetLength(0);
        for (int i = 0; i < size; i++)
        {
            bool hasEdge = false;
            for (int j = 0; j < size; j++)
            {
                if (weights[i, j] != 0)
                {
                    hasEdge = true;
                    break;
                }
            }

            if (!hasEdge)
            {
                if (size < 2) continue; // can't add an edge to a single-vertex graph
                int j = rng.Next(0, size - 1);
                if (j >= i) j++;
                weights[i, j] = weights[j, i] = rng.Next(1, 100);
            }
        }
    }
}
