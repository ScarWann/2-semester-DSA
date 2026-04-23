namespace DSA_2_Generic;

// THIS IS NOT generic AT ALL!!!!!!!!!!!!!! GRRRRRRRRR!!!
public class Helpers
{
    public static bool IsSorted<TContainer, TElement>(TContainer data, bool descending = false)
        where TContainer : IList<TElement>
        where TElement : IComparable<TElement>
    {
        for (int i = 0; i < data.Count - 1;)
        {
            if ((data[i].CompareTo(data[i++]) < 0 && !descending) ||
                (data[i].CompareTo(data[i++]) > 0 && descending))
            {
                return false;
            }
        }

        return true;
    }

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
