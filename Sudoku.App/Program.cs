namespace Sudoku.App;

public static class Program
{
    public static async Task Main(string[] args)
    {
        if (args is { Length: > 0 } && args[0] == "nyt")
            Console.WriteLine($"Downloaded NYT {await NewYorkTimesCollector.Load():yyy-MM-dd}");
        else
            Console.WriteLine("Unknown command");
    }
}
