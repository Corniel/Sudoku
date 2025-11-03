namespace Benchmarks;

public static class Program
{
    public static void Main()
    {
        TestSets.SolveAll();
    }

    public static void Other()
    {
        Cracking_the_Cryptic.Run();
        TestSets.SolveAll();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<DigitIterator>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Position.Iterate>();
    }
}
