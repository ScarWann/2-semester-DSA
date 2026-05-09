namespace LR_4.GraphBenchmarking;

public class Graph(int[,] weights)
{
    public int[,] Weights { get; } = weights;

    public int VertexCount => this.Weights.GetLength(0);

    public int TotalWeight
    {
        get
        {
            int total = 0;
            for (int i = 0; i < this.VertexCount; i++)
            {
                for (int j = i + 1; j < this.VertexCount; j++)
                    total += this.Weights[i, j];
            }

            return total;
        }
    }

    public int this[int i, int j] => this.Weights[i, j];

    public bool HasEdge(int i, int j) => this.Weights[i, j] != 0;

    public override string ToString()
    => string.Join("\n", Enumerable.Range(0, this.VertexCount)
        .Select(i => string.Join(" ", Enumerable.Range(0, this.VertexCount)
            .Select(j => this.Weights[i, j].ToString().PadLeft(4)))));
}
