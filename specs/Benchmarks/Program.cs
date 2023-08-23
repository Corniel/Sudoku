namespace Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Solving.Naked_Singles>();
    }
}