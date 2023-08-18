using Specs.Data;

namespace Performance_specs;

public class Solves
{
    [TestCase]
    public void with_avarage_below_100ms()
    {
        var runs = 100;
        var regions = Regions.Default;
        var puzzles = Puzzles.Solvable;
        _ = Values.Pairs.ToArray();

        var count = 0;

        var sw = Stopwatch.StartNew();
        foreach (var _ in Enumerable.Range(0, runs))
        {
            foreach (var puzzle in puzzles)
            {
                var solved = Solver.Solve(puzzle, regions).LastOrDefault();
                count += solved is null ? 0 : 1;
            }
        }
        sw.Stop();
        var avg = sw.Elapsed.TotalMilliseconds / (puzzles.Count * runs);
        Console.WriteLine($"avg: {avg:0.000} ms/puzzle");
        avg.Should().BeLessThan(100);
    }
}