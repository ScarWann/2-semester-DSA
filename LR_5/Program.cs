namespace LR_5;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Введіть рядок:");
        string input = Console.ReadLine() ?? string.Empty;

        var counts = input.GroupBy(c => c)
                          .Select(g => new { Char = g.Key, Count = g.Count() })
                          .ToList();

        if (!counts.Any()) return;

        int maxCount = counts.Max(x => x.Count);

        Node root = new Node("Root (Level 0)");
        Node[] levels = new Node[maxCount + 1];
        levels[0] = root;

        for (int i = 1; i <= maxCount; i++)
        {
            levels[i] = new Node($"Рівень {i} (Частота {i})");
            levels[i - 1].Children.Add(levels[i]);
        }

        foreach (var item in counts)
        {
            levels[item.Count].Children.Add(new Node($"Символ '{item.Char}'"));
        }

        Console.WriteLine("\nДерево, де глибина вузла = кількості повторень:");
        root.Print();
    }
}
