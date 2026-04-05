namespace Benchmark;

public static class Program
{
    public static void Main()
        => BenchmarkDotNet.Running.BenchmarkRunner.Run<BinaryConversion>();

    public static void Other()
    {
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<BinaryConversion>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<DigitIterator>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Generation>();
        _ = BenchmarkDotNet.Running.BenchmarkRunner.Run<Position.Iterate>();
    }
}
