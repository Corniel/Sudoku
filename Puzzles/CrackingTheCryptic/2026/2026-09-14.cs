namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_09_14 : CtcPuzzle
{
    public override string Title => "Border Arrows";

    public override string? Author => "Aard van de Wetering";

    public override Uri? Url => new("https://youtu.be/UthdnuO0ZEM");

    public override O Duration => O.Unknown;

    public override Cells Solution { get; } = Cells.New("""
        698│275│314
        237│914│658
        451│863│729
        ───┼───┼───
        164│592│837
        972│386│145
        385│147│296
        ───┼───┼───
        743│629│581
        519│738│462
        826│451│973
        """);

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│.1.│...
        .5.│...│.2.
        ───┼───┼───
        ...│.9.│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│7.8│...
        ...│...│...
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Anti.King
        + Lines.Arrow("""
            A.B│.C.│ddD
            a.b│.c.│...
            a.b│.c.│eeE
            ───┼───┼───
            ...│...│...
            Lll│...│ffF
            ...│...│...
            ───┼───┼───
            Kkk│.i.│h.g
            ...│.i.│h.g
            Jjj│.I.│H.G
            """);
}
