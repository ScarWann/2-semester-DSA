namespace DSL;

public struct CompositeComplexity(Func<int, int> omega, Func<int, int> omicron, Func<int, int> theta)
{
    private readonly Func<int, int> omega = omega;
    private readonly Func<int, int> omicron = omicron;
    private readonly Func<int, int> theta = theta;

    public int Omega(int n) => this.omega(n);

    public int Omicron(int n) => this.omicron(n);

    public int Theta(int n) => this.theta(n);
}
