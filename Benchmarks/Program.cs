namespace Benchmarks;

public static class Program
{
    public static void Main()
    {
        Cracking_the_Cryptic.Run(p => p.Title == "Arrow Thermo 2");
    }

    public static void Other()
    {
        Cracking_the_Cryptic.Run();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<DigitIterator>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Position.Iterate>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Solving>();
    }
}
