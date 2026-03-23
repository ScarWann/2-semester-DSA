namespace DSA_2;

public class TestParser
{
    private readonly int testCount;
    private readonly int testLength;
    private bool calculatedInversions;
    private int[] inversions;
    private bool appliedMask;
    private int appliedMaskId = -1;
    private string filename;

    public TestParser(string filename)
    {
        string[] lines = File.ReadAllLines(filename);
        var firstLine = lines[0].Split(' ');
        if (firstLine.Length != 2) throw new FormatException("First line must contain two values separated by a whitespace.");

        int testCount = int.Parse(firstLine[0], System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
        int testLength = int.Parse(firstLine[1], System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture);
        if (lines.Length != testCount + 1) throw new InvalidDataException("First integer of the first row must be equal to the amount of tests. Either adjust the integer or provide correct test amount.");
        int[] expectedTestContents = [.. Enumerable.Range(1, testLength)];
        int[][] rawLineData = new int[testCount][];

        for (int i = 0; i < testCount; i++)
        {
            rawLineData[i] = [.. lines[i].Split(' ').Select(str => int.Parse(str, System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.InvariantCulture))];
            if (rawLineData[i].Length != testLength + 1) throw new InvalidDataException("Each line except for the first must contain the amount of integers provided by the second integer of the first row, plus an identifier as the first integer of the row. Either adjust the integer in the first row or provide correct tests of correct size.");
            if (rawLineData[i][0] != i + 1) throw new InvalidDataException("Each test must start with an identifier, in order.");
            if (rawLineData[i][1..].ToHashSet() != expectedTestContents.ToHashSet() && rawLineData[i][1..].ToHashSet().Count == rawLineData[i][1..].Length) throw new InvalidDataException("Each test must consist of a range of integers from 1 up to the length of the test.");
        }

        this.Data = rawLineData.Select(data => new TestCase(array: data[1..], id: data[0])).ToArray();
        this.testCount = testCount;
        this.testLength = testLength;
        this.filename = filename;
    }

    public IList<TestCase> Data { get; }

    public int MaskId { get; set; }

    public IReadOnlyCollection<int> Inversions => this.calculatedInversions ? this.inversions : this.CalculateInversions();

    private int[] CalculateInversions(int maskId)
    {
        
    }

    private int[] CalculateInversionsWithoutMask()
    {
        this.calculatedInversions = true;
        return this.inversions = this.Data.Select(testCase => testCase.Inversions).ToArray();
    }

    private void ApplyMask(int id)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(id);
        this.ApplyMask(this.Data[id]);
    }

    private void ApplyMask(TestCase testCase)
    {
        this.appliedMaskId = this.Data.IndexOf(testCase);
        this.ApplyMask(indexMask: testCase.ToArray().Select((val, idx) => (val, idx)).ToDictionary(pair => pair.val, pair => pair.idx));
    }

    private void ApplyMask(IDictionary<int, int> indexMask)
    {
        if (this.appliedMask)
        {
            throw new ArgumentException("Mask was already applied.");
        }
        else
        {
            foreach (var testCase in this.Data) testCase.ApplyMask(indexMask);
            this.appliedMask = true;
        }
    }

    public void DumpResults(string filename)
    {
        
    }

    public override string ToString()
    {
        return this.appliedMaskId != -1 ? $"Unknown mask ID\n{this.}"
    }
}
