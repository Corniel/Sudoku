namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_05_21 : CtcPuzzle
{
    public override string Title => "Stepped Themos";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/AdSOJQ3huN0");

    public override O Duration => O.μs10;

    public override Clues Clues { get; } = Clues.New("""
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        ...|...|...
        ...|...|...
        ---+---+---
        ...|...|...
        7..|...|...
        ..9|...|...
        """);

    public override Cells Solution { get; } = Cells.New("""
        541|627|893
        982|531|674
        376|984|521
        ---+---+---
        625|493|718
        137|865|942
        498|172|356
        ---+---+---
        813|259|467
        754|316|289
        269|748|135
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.Standard
        + Triples().SelectMany(NonConsecutive.New)
        + Lines.Thermometer("""
         ...|...|.P.
         FE.|...|NO.
         .DC|..L|M..
         ---+---+---
         ..B|A.K|...
         ...|...|...
         ...|a.k|l..
         ---+---+---
         ..c|b..|mn.
         .ed|...|.op
         .f.|...|...
         """);

    private static IEnumerable<PosSet> Triples()
    {
        for (var f = 0; f < _9; f++)
        {
            for (var s = 0; s < 9; s += 3)
            {
                yield return PosSet.New((f, s), (f, s + 1), (f, s + 2));
                yield return PosSet.New((s, f), (s + 1, f), (s + 2, f));
            }
        }
    }
}
