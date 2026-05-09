using LR_4.GraphBenchmarking;
using ScottPlot.Plottables;

namespace LR_4.GraphBenchmarking;

public static class Algorithms
{
    public static (Graph DataStructure, SpanningBenchmarks Benchmarks) BoruvkaAlgorithm(Graph graph)
    {
        int n = graph.VertexCount;
        var benchmarks = new SpanningBenchmarks();
        int[,] mst = new int[n, n];

        int[] component = new int[n];
        for (int i = 0; i < n; i++) component[i] = i;

        int componentCount = n;

        while (componentCount > 1)
        {
            var cheapest = new (int V, int Weight)[n];
            for (int i = 0; i < n; i++) cheapest[i] = (-1, int.MaxValue);

            for (int u = 0; u < n; u++)
            {
                for (int v = 0; v < n; v++)
                {
                    if (!graph.HasEdge(u, v)) continue;
                    int cu = component[u], cv = component[v];
                    if (cu == cv) continue;

                    benchmarks.Comparisons++;
                    if (graph[u, v] < cheapest[cu].Weight)
                        cheapest[cu] = (v, graph[u, v]);

                    benchmarks.Comparisons++;
                    if (graph[u, v] < cheapest[cv].Weight)
                        cheapest[cv] = (u, graph[u, v]);
                }
            }

            // Merging
            bool anyMerged = false;
            for (int c = 0; c < n; c++)
            {
                var (v, w) = cheapest[c];
                if (v == -1) continue;
                if (component[c] == component[v]) continue;

                mst[c, v] = mst[v, c] = w;

                int cc = component[c], cv = component[v];
                for (int i = 0; i < n; i++)
                    if (component[i] == cv) component[i] = cc;

                componentCount--;
                anyMerged = true;
            }

            if (!anyMerged) break;
        }

        return (new Graph(mst), benchmarks);
    }
}
