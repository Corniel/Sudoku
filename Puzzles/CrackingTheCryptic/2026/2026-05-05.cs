namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_05 : CtcPuzzle
{
    public override string Title => "Wrapped";

    public override string? Author => "Sotehr";

    public override Uri? Url => new("https://youtu.be/HmYktrkXLCc");

    public override O Duration => O.μs100;

    public override Cells Solution { get; } = Cells.New("""
        735│941│826
        469│382│751
        812│567│493
        ───┼───┼───
        694│215│378
        523│478│169
        187│693│542
        ───┼───┼───
        978│124│635
        346│859│217
        251│736│984
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Lines.GermanWhisper("""
        ...│...│...
        ..E│FGH│...
        .D.│...│.g.
        ───┼───┼───
        .C.│...│f..
        .B.│..e│...
        KA.│.cd│...
        ───┼───┼───
        L..│b..│...
        .Ma│...│...
        ...│...│...
        """)
        + Lines.Renban("""
        ...│...│...
        ...│...│...
        ..A│AA.│B..
        ───┼───┼───
        ..A│..B│.C.
        ..A│.B.│.C.
        ...│B..│.C.
        ───┼───┼───
        ..B│...│.C.
        ...│CCC│C..
        ...│...│...
        """)
        + Couples.WhiteDots("""
        ...│...│...
        ...│...│...
        ...│.AA│B..
        ───┼───┼───
        ...│...│B..
        ...│...│...
        ..C│...│...
        ───┼───┼───
        ..C│DD.│...
        ...│...│...
        ...│...│...
        """)
        + Couples.BlackDots("""
        ...│...│...
        ...│...│...
        .AA│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│BB.
        ...│...│...
        ...│...│...
        """)
        + Groups.Cages("""
        ...│...│...
        ...│...│JA.
        ...│..I│..A
        ───┼───┼───
        ...│HH.│..B
        ...│H..│E..
        ..G│...│E..
        ───┼───┼───
        .F.│.CC│D..
        ...│...│...
        ...│...│...
        A=B C=D=E F=G=H=I=J
        """);
}
