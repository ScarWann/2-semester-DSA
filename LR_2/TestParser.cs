namespace DSA_2;

public class TestParser
{
    private bool calculatedInversions;
    private (int Id, int Inversions)[]? indexedInversions;
    private bool appliedMask;
    private int appliedMaskId = -1;
    private TestCase[]? data;

#pragma warning disable CS8618
    public TestParser()
    {
    }
#pragma warning restore CS8618

    public IList<TestCase> Data => this.data;

    public int MaskId { get; set; }

    public IReadOnlyCollection<(int Id, int Inversions)> IndexedIversions => this.calculatedInversions ? this.indexedInversions : this.CalculateInversions();

    public string OutputFile { get; set; }

    public string InputFile { get; set; }

    public static void Main(string[] args)
    {
        var parser = new TestParser();
        for (int i = 0; i < args.Length; i++)
        {
            switch (args[i])
            {
                case "-i": parser.InputFile = args[++i]; break;
                case "-o": parser.OutputFile = args[++i]; break;
                case "-x": parser.MaskId = int.Parse(args[++i], System.Globalization.CultureInfo.InvariantCulture); break;
                default:
                    ExitWithMessage(
                    "Usage: dotnet run -- -i <input> -o <output> [-x: Mask ID]:" +
                    " '-i' Input file path" +
                    " '-o' Output file path" +
                    " '-x' ID of the array relative to which the inversions are counted"); break;
            }
        }

        parser.ReadFile();
        parser.DumpResults();
    }

    public void ReadFile()
    {
        ArgumentNullException.ThrowIfNull(this.InputFile);
        string[] lines = File.ReadAllLines(this.InputFile);

        ThrowExceptionOnInvalidInputFormatting(lines);

        var firstLine = lines[0].Split(' ');
        int testCount = int.Parse(firstLine[0], System.Globalization.CultureInfo.InvariantCulture);
        int[][] rawLineData = new int[testCount][];

        for (int i = 0; i < testCount; i++)
        {
            rawLineData[i] = [.. lines[i + 1].Split(' ').Select(str => int.Parse(str, System.Globalization.CultureInfo.InvariantCulture))];
        }

        this.data = rawLineData.Select(data => new TestCase(array: data[1..], id: data[0])).ToArray();
    }

    public void DumpResults()
    {
        ArgumentNullException.ThrowIfNull(this.OutputFile);
        File.WriteAllText(this.OutputFile, this.ToString());
    }

    private (int Id, int Inversions)[] CalculateInversions()
    {
        if (this.MaskId != 0) this.ApplyMask();
        this.CalculateInversionsWithoutMask();

        foreach (var t in this.Data)
            Console.WriteLine($"TestCase {t.Id} array after mask: [{string.Join(",", t.ToArray())}] inversions: {t.Inversions}");

        return this.indexedInversions;
    }

    private (int Id, int Inversions)[] CalculateInversionsWithoutMask()
    {
        this.calculatedInversions = true;
        return this.indexedInversions = this.Data.Where(e => e.Id != this.appliedMaskId).OrderBy(e => e.Inversions).Select(e => (e.Id, e.Inversions)).ToArray();
    }

    private void ApplyMask()
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(this.MaskId);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(this.MaskId, this.Data.ToArray().Length);
        this.ApplyMask(this.Data[this.MaskId - 1]);
    }

    private void ApplyMask(TestCase testCase)
    {
        this.appliedMaskId = testCase.Id;
        int[] xMovieAtRank = new int[testCase.Length];
        for (int mIdx = 0; mIdx < testCase.Length; mIdx++)
            xMovieAtRank[testCase[mIdx] - 1] = mIdx;
        this.ApplyMask(xMovieAtRank);
    }

    private void ApplyMask(int[] xMovieAtRank)
    {
        if (this.appliedMask)
            throw new ArgumentException("Mask was already applied.");
        foreach (var testCase in this.Data)
            testCase.ApplyMask(xMovieAtRank);
        this.appliedMask = true;
    }

    private new string ToString()
    {
        return this.MaskId + $"\n{string.Join("\n", this.IndexedIversions.Select(e => $"{e.Id} {e.Inversions}"))}";
    }

    private static void ThrowExceptionOnInvalidInputFormatting(string[] lines)
    {
        var firstLine = lines[0].Split(' ');
        if (firstLine.Length != 2) throw new FormatException("First line must contain two values separated by a whitespace.");

        int testCount = int.Parse(firstLine[0], System.Globalization.CultureInfo.InvariantCulture);
        int testLength = int.Parse(firstLine[1], System.Globalization.CultureInfo.InvariantCulture);
        if (lines.Length != testCount + 1) throw new InvalidDataException("First integer of the first row must be equal to the amount of tests. Either adjust the integer or provide correct test amount.");
        int[] expectedTestContents = [.. Enumerable.Range(1, testLength)];
        int[][] rawLineData = new int[testCount][];

        for (int i = 0; i < testCount; i++)
        {
            rawLineData[i] = [.. lines[i + 1].Split(' ').Select(str => int.Parse(str, System.Globalization.CultureInfo.InvariantCulture))];
            if (rawLineData[i].Length != testLength + 1) throw new InvalidDataException("Each line except for the first must contain the amount of integers provided by the second integer of the first row, plus an identifier as the first integer of the row. Either adjust the integer in the first row or provide correct tests of correct size.");
            if (rawLineData[i][0] != i + 1) throw new InvalidDataException("Each test must start with an identifier, in order.");
            if (!rawLineData[i][1..].Order().SequenceEqual(expectedTestContents.Order())) throw new InvalidDataException("Each test must consist of a range of integers from 1 up to the length of the test.");
        }
    }

    private static void ExitWithMessage(string message)
    {
        Console.WriteLine(message);
        Environment.Exit(0);
    }
}
