using SudokuSolver;

namespace Solves;

public class With_technique
{
    [Test]
    public void hidden_singles() => Solve(@"
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
        387|691|452");

    [Test]
    public void Naked_pairs() => Solve(@"
        251|348|796
        ...|917|2.4
        ..7|256|...
        ---+---+---
        ...|.6.|837
        ...|...|.7.
        ..8|...|9..
        ---+---+---
        ...|62.|..8
        8..|7..|...
        ..2|5.1|64.",
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
        387|691|452");

    /// <remarks>See: https://youtu.be/4GVyBiFUNws</remarks>
    [Test]
    public void Hard_easy() => Solve(@"
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
        835|274|196");

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

    private void Solve(string input, string expected)
    {
        _ = Regions.Default;
        var cells = Cells.Parse(input);
        var solution = Cells.Parse(expected);

        var sw = Stopwatch.StartNew();
        var reductions = Solver.Solve(cells).ToArray();
        sw.Stop();

        var last = new Reduction(cells, typeof(object));
        foreach(var r in reductions)
        {
            var delta = r.Reduced.Delta(last.Reduced).ToArray();
            last = r;
            Console.WriteLine(r.Technique.Name);
            Console.WriteLine(string.Join(", ", delta.OrderByDescending(c => c.Values.SingleValue())));
            Console.WriteLine();
        }

        Console.WriteLine("Elapsed: {0:#,##0.#####} ms", sw.Elapsed.TotalMilliseconds);

        Console.WriteLine(last.Reduced);

        reductions.Should().NotBeEmpty();
        reductions.Last().Reduced.Solved().Should().BeTrue();
        reductions.Last().Reduced.Should().BeEquivalentTo(solution);
    }
}