namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_03 : CtcPuzzle
{
    public override string Title => "Killer Whispers";

    public override string? Author => "David Storrs";

    public override Uri? Url => new("https://youtu.be/FtR_Qtg62b0");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        287│154│693
        935│762│814
        164│938│257
        ───┼───┼───
        349│681│725
        652│397│148
        718│245│936
        ───┼───┼───
        573│829│461
        826│413│579
        491│576│382
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Groups.Cages(
        """
        AA.│...│...
        AA.│...│...
        ..B│BBB│B..
        ───┼───┼───
        ..B│...│B..
        ..B│CC.│B..
        ..B│CC.│B..
        ───┼───┼───
        ..B│BBB│B..
        DD.│...│...
        DD.│...│...
        A=22 B=88 C=18 D=23
        """,
        isSet: false)
        + Lines.GermanWhisper("""
        AB.│y..│...
        DC.│z..│...
        ..J│KLM│N..
        ───┼───┼───
        ..Y│...│O..
        ..X│...│P..
        FGW│...│Q.h
        ───┼───┼───
        .HV│UTS│Rfg
        ab.│...│...
        dc.│...│...
        """)
        + Lines.GermanWhisper("""
        A..│...│...
        B..│...│...
        ..J│...│...
        ───┼───┼───
        ..K│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        """)
        + Lines.GermanWhisper("""
        ...│...│.BC
        ...│...│.A.
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│EF.│...
        ...│.G.│...
        ───┼───┼───
        ...│...│...
        ...│...│..I
        ...│...│.KJ
        """)
         + Lines.GermanWhisper("""
        ...│...│.A.
        ...│...│..B
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│.F.│...
        ...│E..│...
        ───┼───┼───
        ...│...│...
        ...│...│.J.
        ...│...│..I
        """);
}
