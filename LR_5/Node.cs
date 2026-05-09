namespace LR_5;

public class Node(string name)
{
    public string Name { get; set; } = name;

    public List<Node> Children { get; set; } = [];

    public void Print(int depth = 0)
    {
        Console.WriteLine(new string(' ', depth * 4) + "|-- " + this.Name);
        foreach (var child in this.Children) child.Print(depth + 1);
    }
}
