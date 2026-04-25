namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_04_08 : CtcPuzzle
{
    public override string Title => "Colorful Whispers";

    public override string? Author => "Sneppix";

    public override Uri? Url => new("https://youtu.be/vC0McY40Cjk");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.Parse("""
        482│593│176
        937│618│254
        561│274│839
        ───┼───┼───
        376│842│591
        849│351│627
        215│769│483
        ───┼───┼───
        754│926│318
        198│435│762
        623│187│945
        """);

    protected override Rules GetConstraints()
        => Rules.Standard
        + GermanWhispers.Parse("""
        .HK│...│...
        .IL│.PQ│R.T
        ..M│...│..U
        ───┼───┼───
        A.N│...│..V
        BXY│Z..│.bc
        C..│...│..x
        ───┼───┼───
        D..│i..│..y
        E..│j..│..z
        F..│...│...
        """)
        + DutchWhispers.Parse("""
        A..│...│...
        B..│...│...
        C..│...│...
        ───┼───┼───
        ...│...│...
        ...│.kl│m..
        ...│..K│LM.
        ───┼───┼───
        .xX│...│...
        .yY│...│...
        .zZ│...│...
        """)
        + pos(6, 7).LT((6, 6));
}
