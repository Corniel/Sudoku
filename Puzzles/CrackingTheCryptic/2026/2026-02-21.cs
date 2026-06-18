namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_02_21 : CtcPuzzle
{
    public override string Title => "Schubladen";

    public override string? Author => "Myxo";

    public override Uri? Url => new("https://youtu.be/sFUTQioeDaw");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        134│276│859
        792│538│614
        685│194│732
        ───┼───┼───
        869│415│327
        573│962│148
        241│387│596
        ───┼───┼───
        918│643│275
        326│759│481
        457│821│963
        """);

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│..5│...
        ...│...│...
        2..│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│8..│...
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.XSudoku
        + Lines.Thermometer("""
        .cd│...│pn.
        B.b│...│o.m
        AC.│a..│.lk
        ───┼───┼───
        ..D│...│...
        ...│...│...
        ...│...│.i.
        ───┼───┼───
        ...│..F│.hg
        K.M│...│G.f
        .LN│...│IH.
        """);
}
