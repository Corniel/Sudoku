namespace Benchmarks;

public static class Program
{
    public static void Main()
    {
        Cracking_the_Cryptic.Run(p => p.Duration is Puzzles.O.Unknown);
    }

    public static void Other()
    {
        Cracking_the_Cryptic.Run(p => p.Duration is Puzzles.O.Unknown);
        TestSets.SolveAll();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<DigitIterator>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Position.Iterate>();
    }
}
