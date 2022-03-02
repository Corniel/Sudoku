using SudokuSolver;
using SudokuSolver.Techniques;

namespace Solves;

public class With_technique
{
    [Test]
    public void Naked_singles() => Solve(@"
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
        672|914|583
        845|736|219
        913|258|746
        ---+---+---
        198|325|674
        756|849|321
        234|167|895
        ---+---+---
        421|583|967
        569|472|138
        387|691|452",
        new NakedSingles());

    [Test]
    public void Hidden_singles() => Solve(@"
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
        672|914|583
        845|736|219
        913|258|746
        ---+---+---
        198|325|674
        756|849|321
        234|167|895
        ---+---+---
        421|583|967
        569|472|138
        387|691|452",
        new HiddenSingles(),
        new NakedSingles());

    [Test]
    public void Naked_pairs() => Solve(@"
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
        639|278|514
        587|314|269
        124|569|387
        ---+---+---
        345|986|721
        798|132|456
        216|457|893
        ---+---+---
        453|891|672
        861|723|945
        972|645|138",
        new NakedPairs(),
        new NakedSingles(),
        new HiddenSingles());

    /// <remarks>See: https://youtu.be/4GVyBiFUNws</remarks>
    [Test]
    public void Hidden_pairs() => Solve(@"
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
   @"
        483|729|561
        529|146|387
        716|538|249
        ---+---+---
        152|693|478
        647|852|913
        398|417|652
        ---+---+---
        274|961|835
        961|385|724
        835|274|196",
        new HiddenPairs(),

        new NakedSingles(),
        new HiddenSingles(),
        new NakedPairs());

    /// <remarks>See: https://youtu.be/Ui1hrp7rovw</remarks>
    [Test]
    public void Steering_wheel() => Solve(@"...|1.2|...
        .6.|...|.7.
        ..8|...|9..
        ---+---+---
        4..|...|..3
        .5.|..7|...
        2..|.8.|..1
        ---+---|--- 
        ..9|...|8.5
        .7.|...|.6.
        ...|3.4|...",
        @"
        ...|1.2|...
        .6.|...|.7.
        ..8|...|9..
        ---+---+---
        4..|...|..3
        .5.|..7|...
        2..|.8.|..1
        ---+---|---
        ..9|...|8.5
        .7.|...|.6.
        ...|3.4|...");


    [Test]
    public void Unsolved() => Solve(@"
        ..1|3..|...
        ...|.9.|..4
        92.|..8|...
        ---+---+---
        1.9|6..|.72
        5..|...|..3
        68.|..2|4.9
        ---+---+---
        ...|1..|.28
        2..|.4.|...
        ...|..7|3..",
    @"
        639|278|514
        587|314|269
        124|569|387
        ---+---+---
        345|986|721
        798|132|456
        216|457|893
        ---+---+---
        453|891|672
        861|723|945
        972|645|138");

    private void Solve(string input, string expected, params Technique[] techniques)
    {
        techniques = techniques.Any() ? techniques : null;
        _ = Regions.Default;
        var cells = Cells.Parse(input);
        var solution = Cells.Parse(expected);

        var sw = Stopwatch.StartNew();
        var reductions = Solver.Solve(cells, techniques).ToArray();
        sw.Stop();

        var last = new Reduction(cells, typeof(object));
        foreach(var r in reductions)
        {
            var delta = r.Reduced.Delta(last.Reduced).ToArray();
            last = r;
            Console.WriteLine(r.Technique.Name);
            Console.WriteLine(string.Join(", ", delta.Where(c => c.Values.SingleValue())));
            Console.WriteLine();
        }

        Console.WriteLine("Elapsed: {0:#,##0.#####} ms", sw.Elapsed.TotalMilliseconds);

        Console.WriteLine(last.Reduced);

        Console.WriteLine(string.Join(", ", last.Reduced.Where(c => c.Values.IsUndecided())));

        reductions.Should().NotBeEmpty();
        reductions.Last().Reduced.Solved().Should().BeTrue();
        reductions.Last().Reduced.Should().BeEquivalentTo(solution);
    }
}