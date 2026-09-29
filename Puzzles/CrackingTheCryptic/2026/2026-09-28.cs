namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_09_28 : CtcPuzzle
{
    public override string Title => "Border Thermos";

    public override string? Author => "Aad van de Wetering";

    public override Uri? Url => new("https://youtu.be/H5BTHIevdqw");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        461│529│873
        582│743│619
        793│168│542
        ───┼───┼───
        826│497│135
        357│812│964
        149│356│728
        ───┼───┼───
        235│681│497
        914│275│386
        678│934│251
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.AntiKing
        + Lines.Thermometer("""
            A.F│..N│MLK
            B.G│...│...
            C.H│..S│RQP
            ───┼───┼───
            D.I│...│...
            ...│...│...
            ...│...│i.d
            ───┼───┼───
            pqr│s..│h.c
            ...│...│g.b
            klm│m..│f.a
            """)
         + Lines.Thermometer("""
            ...│.E.│...
            ...│.F.│...
            ...│.G.│...
            ───┼───┼───
            ...│...│...
            ABC│...│cba
            ...│...│...
            ───┼───┼───
            ...│.g.│...
            ...│.f.│...
            ...│.e.│...
            """)
        + pos(2, 2).Clue(3)
        + pos(8, 7).Clue(5);
}
