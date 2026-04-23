namespace DSA_2;

public class TestCase
{
    private int[] array;
    private int inversions;
    private bool inversionsCalculated;

    public TestCase(int[] array)
    {
        ArgumentNullException.ThrowIfNull(array);

        this.array = array;
    }

    public TestCase(int[] array, int id)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

        this.array = array;
        this.Id = id;
    }

    public int Length => this.array.Length;

    public int Id
    {
        get;
    }

    public int Inversions => this.inversionsCalculated ? this.inversions : this.GetInversions();

    private IReadOnlyCollection<int> Data => this.array;

    public int this[int i]
    {
        get
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(i, this.array.Length);
            return this.array[i];
        }

        set
        {
            ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(i, this.array.Length);
            this.array[i] = value;
        }
    }

    public int[] ToArray()
    {
        return this.Data.ToArray();
    }

    public void ApplyMask(int[] xMovieAtRank)
    {
        this.array = xMovieAtRank.Select(filmIdx => this.array[filmIdx]).ToArray();
    }

    public override string ToString()
    {
        return $"{this.Id} {string.Join(" ", this.ToArray())}";
    }

    private int GetInversions()
    {
        this.inversionsCalculated = true;
        if (this.Length == 1) return 0;
        this.SortAndCountInversions(this.ToArray());
        return this.inversions;
    }

    private int[] MergeAndCountSplitInversions(int[] leftArray, int[] rightArray)
    {
        int[] result = new int[leftArray.Length + rightArray.Length];
        int i = 0, j = 0;
        for (int k = 0; k < result.Length; k++)
        {
            if (j == rightArray.Length) result[k] = leftArray[i++];
            else if (i == leftArray.Length) result[k] = rightArray[j++];
            else if (leftArray[i] <= rightArray[j]) result[k] = leftArray[i++];
            else
            {
                result[k] = rightArray[j++];
                this.inversions += leftArray.Length - i;
            }
        }

        return result;
    }

    private int[] SortAndCountInversions(int[] array)
    {
        if (array.Length == 1) return array;
        var left = this.SortAndCountInversions(array[..(array.Length / 2)]);
        var right = this.SortAndCountInversions(array[(array.Length / 2)..]);
        var result = this.MergeAndCountSplitInversions(left, right);
        return result;
    }
}
