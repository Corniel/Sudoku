namespace Performance_specs;

public class Solves
{
    [TestCase]
    public void with_avarage_below_100ms()
    {
        var runs = 100;
        var regions = Regions.Default;
        var puzzles = Puzzles();
        _ = HiddenPairs.Pairs.ToArray();

        var count = 0;

        var sw = Stopwatch.StartNew();
        foreach(var _ in Enumerable.Range(0, runs))
        {
            foreach(var puzzle in puzzles)
            {
                var solved = Solver.Solve(puzzle, regions).LastOrDefault();
                count += solved is null ? 0 : 1;
            }
        }
        sw.Stop();
        var avg = sw.Elapsed.TotalMilliseconds / (puzzles.Length * runs);
        Console.WriteLine($"avg: {avg:0.000} ms/puzzle");
        avg.Should().BeLessThan(100);
    }

    private static Puzzle[] Puzzles()
        => new[]
        {
            @"
            6..|..4|..3
            ..5|7.6|.1.
            .1.|...|7..
            ---+---+---
            .98|32.|6..
            75.|8..|.21
            ..4|1.7|.9.
            ---+---+---
            4..|5..|..7
            .6.|...|1..
            3..|69.|.52",
            @"
            6.9|..8|..4
            ..7|..4|..9
            ..4|...|38.
            ---+---+---
            ...|.8.|721
            ...|1.2|...
            216|.5.|...
            ---+---+---
            .53|...|6..
            8..|7..|9..
            9..|6..|1.8",
            @"
            6..|..4|..3
            ..5|7.6|.1.
            .1.|...|7..
            ---+---+---
            ...|32.|6..
            75.|8..|.2.
            ..4|1..|.9.
            ---+---+---
            4..|5..|..7
            .6.|...|1..
            3..|69.|..2",
            @"
            ..5|...|1..
            .61|...|2..
            ...|38.|...
            ---+---+---
            .2.|...|..4
            ...|.3.|..9
            .13|5..|..2
            ---+---+---
            9..|..2|.4.
            ...|...|.7.
            4..|.59|..3",
            @"
            .8.|.2.|56.
            ...|1..|..7
            ...|...|...
            ---+---+---
            .5.|.9.|4.8
            ..7|8..|..3
            .9.|.1.|.5.
            ---+---+---
            2.4|...|8..
            .6.|.85|...
            ...|2..|1..",
        }.Select(p => Puzzle.Parse(p)).ToArray();
}
