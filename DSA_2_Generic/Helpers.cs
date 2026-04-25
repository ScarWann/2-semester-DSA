namespace DSA_2_Generic;

public class Helpers
{
    public static IEnumerable<int> Range(int from, int to, int step)
    {
        for (int i = from; i <= to; i = checked(i + step)) yield return i;
    }
}

public static class ExtensionHelpers
{
    public static IEnumerable<IEnumerable<T>> Transpose<T>(this IEnumerable<IEnumerable<T>> matrix)
    {
        return matrix.Aggregate(
            matrix.First().Select(x => new List<T> { x } as IEnumerable<T>),
            (acc, row) => acc.Zip(row, (a, b) => a.Append(b)));
    }
}
