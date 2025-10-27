namespace Benchmarks;

public static class Program
{
    public static void Main()
    {
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Solving>();
    }

    public static void Other()
    {
        Cracking_the_Cryptic.Run();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Position.Iterate>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Solving>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<ValueIterator>();
    }
}
