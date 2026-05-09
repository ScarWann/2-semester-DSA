namespace DSL;

public static class Helpers
{
    public static IEnumerable<int> Range(int from, int to, int step)
    {
        for (int i = from; i <= to; i = checked(i + step)) yield return i;
    }
}
