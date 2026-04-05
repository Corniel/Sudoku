using Sudoku.Houses;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2024_09_29 : CtcPuzzle
{
    public override string Title => "3 In the Corner";

    public override string? Author => "James Kopp";

    public override Uri? Url => new("https://youtu.be/x6RrwaOb0Iw");

    public override O Duration => O.oo;

    // TODO: remove the arrow: it has not been specified, but could be deduced
    // by a hint that is not defined in a constraint
    public override Clues Clues { get; } = Clues.Parse("""
        .9.|...|...
        ..1|...|...
        ...|1..|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|1..
        ---+---+---
        ...|...|...
        ...|...|...
        3..|...|...
        """);

    public override Rules Constraints { get; } =
        Rules.Standard
        + AtLeast3s();

    public override Cells Solution { get; } = Cells.Parse("""
        594|738|261
        261|495|837
        837|162|594
        ---+---+---
        159|384|726
        726|951|483
        483|627|159
        ---+---+---
        948|273|615
        615|849|372
        372|516|948
        """);

    private static IEnumerable<Pair> AtLeast3s()
    {
        foreach (var box in Box.All)
        {
            foreach (var c in box)
            {
                if (c.W() is { } w && box.Cells.Contains(w))
                    yield return DeltaMin.New(c, w, 3).One;

                if (c.S() is { } s && box.Cells.Contains(s))
                    yield return DeltaMin.New(c, s, 3).One;
            }
        }
    }
}
