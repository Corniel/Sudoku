namespace Benchmarks;

public static class Program
{
    public static void Main(string[] args)
    {
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Solving>();
    }

    public static void Other()
    {
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Cracking_the_Cryptic>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Position.Iterate>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Solving>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<ValueIterator>();
    }
}
