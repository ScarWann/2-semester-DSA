namespace DSA_2;

public class TestCase
{
    private int[] array;
    private int inversions = -1;

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

    public TestCase(int[] array, IDictionary<int, int> indexMask, int id)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(indexMask);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(id);

        this.array = array;
        this.ApplyMask(indexMask);
        this.Id = id;
    }

    public int Length => this.array.Length;

    public int Id
    {
        get;
    }

    public int Inversions
    {
        get
        {
            return this.inversions != -1 ? this.inversions : this.GetInversions();
        }
    }

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

    public void ApplyMask(IDictionary<int, int> indexMask)
    {
        this.array = this.array.Select(val => indexMask[val]).ToArray();
    }

    public override string ToString()
    {
        return $"{this.Id} {string.Join(" ", this.ToArray())}";
    }

    private int GetInversions()
    {
        if (this.Length == 1)
        {
            return 0;
        }
        else
        {
            this.SortAndCountInversions(this.ToArray());
            return this.inversions;
        }
    }

    private int[] SortAndCountInversions(int[] array)
    {
        var leftArray = this.SortAndCountInversions(array[1..(array.Length / 2)]);
        var rigthArray = this.SortAndCountInversions(array[(array.Length / 2)..]);
        return this.MergeAndCountSplitInversions(array, leftArray, rigthArray);
    }

    private int[] MergeAndCountSplitInversions(int[] array, int[] leftArray, int[] rightArray)
    {
        int i = 0, j = 0;
        for (int k = 0; k < array.Length; k++)
        {
            if (leftArray[i] <= rightArray[j] || j == rightArray.Length)
            {
                array[k] = leftArray[i];
                i++;
            }
            else
            {
                array[k] = rightArray[j];
                j++;
                this.inversions += rightArray.Length - i;
            }
        }

        return array;
    }
}
