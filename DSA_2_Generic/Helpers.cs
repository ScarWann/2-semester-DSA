namespace DSA_2_Generic;

// THIS IS NOT ABSTRACT AT ALL!!!!!!!!!!!!!! GRRRRRRRRR!!!
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
}
