namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_02_09 : CtcPuzzle
{
    public override string Title => "Thermo Sudoku";

    public override string? Author => "Sam Cappleman-Lynes";

    public override Uri? Url => new("https://youtu.be/lgJYOuVk910");

    public override O Duration => O.μs100;

    public override Clues Clues { get; } = Clues.Parse("""
        .4.│...│.1.
        2..│...│..6
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        9..│...│..2
        .1.│...│.9.
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        847│632│519
        295│471│386
        631│598│247
        ───┼───┼───
        129│743│865
        486│259│173
        753│816│924
        ───┼───┼───
        368│924│751
        974│185│632
        512│367│498
        """);

    protected override Rules GetConstraints()
        => Rules.Standard
        + Thermometers.Parse("""
        ...│D.H│...
        ..C│.E.│I..
        .B.│..F│.J.
        ───┼───┼───
        A.m│...│..K
        .l.│...│.L.
        k..│...│M.a
        ───┼───┼───
        .j.│f..│.b.
        ..i│.e.│c..
        ...│h.d│...
        """);
}
