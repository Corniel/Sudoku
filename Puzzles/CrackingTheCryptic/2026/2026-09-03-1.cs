namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_09_03_1 : CtcPuzzle
{
    public override string Title => "Blue Waves";

    public override string? Author => "Antiknight";

    public override Uri? Url => new("https://youtu.be/rb531dEkOEg");

    public override O Duration => O.Unknown;

    public override Cells Solution { get; } = Cells.New("""
        679│451│823
        123│786│459
        485│239│671
        ───┼───┼───
        896│345│712
        712│698│345
        354│127│986
        ───┼───┼───
        241│863│597
        537│914│268
        968│572│134
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
            ...│...│...
            ...│...│BB.
            ...│..A│...
            ───┼───┼───
            ...│...│...
            ...│...│DD.
            ...│..C│...
            ───┼───┼───
            ...│...│...
            ...│..E│...
            ...│...│FF.
            A=B C=D E=F
            """)
        + Lines.Thermometer("""
            ...│DE.│...
            ABC│...│...
            ...│...│...
            ───┼───┼───
            ...│IJK│...
            .GH│...│...
            ...│...│...
            ───┼───┼───
            .M.│R..│W.Y
            N.Q│U..│.XZ
            .PT│...│...
            """);
}
