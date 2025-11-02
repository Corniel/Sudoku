using Sudoku.Parsing;

namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_03_25 : CtcPuzzle
{
    public override string Title => "Rapuzzle";
    public override string? Author => "Scojo";
    public override Uri? Url => new("https://youtu.be/-L9qq8cyQ5M");
    public override O Duration => O.ms;

    public override Clues Clues { get; } = Clues.Parse("""
        ...|.2.|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        471|928|536
        836|571|924
        925|436|187
        ---+---+---
        194|382|675
        387|695|241
        652|147|398
        ---+---+---
        549|813|762
        218|769|453
        763|254|819
        """);

    public override Rules Constraints { get; } =
        Rules.AntiKnight
        + Tower()
        + EntropicLines.Parse("""
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        .AB|C..|...
        ..D|E..|...
        ..F|...|...
        ---+---+---
        ...|G.I|...
        ...|.HJ|K..
        ...|...|.L.
        """);



    private static IEnumerable<Thermometer> Tower()
    {
        var tower = NamedCage.Parse("""
            .T.|...|...
            TTT|...|...
            TTT|...|...
            ---+---+---
            TTT|...|...
            TTT|...|...
            TTT|...|...
            ---+---+---
            TTT|...|...
            TTT|...|...
            TTT|...|...
            """).Single();

        foreach(var t in tower.Cells)
        {
            if (t.N() is { } n && !tower.Cells.Contains(n))
                yield return new Thermometer([n, t]);

            if (t.W() is { } w && !tower.Cells.Contains(w))
                yield return new Thermometer([w, t]);

            if (t.E() is { } e && !tower.Cells.Contains(e))
                yield return new Thermometer([e, t]);
        }
    }
}
