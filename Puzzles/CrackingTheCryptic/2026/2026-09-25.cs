namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_09_25 : CtcPuzzle
{
    public override string Title => "Beetle, Beetle";

    public override string? Author => "Walter Grönholm";

    public override Uri? Url => new("https://youtu.be/WtF_PfobSVs");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        681│542│397
        749│138│625
        532│769│841
        ───┼───┼───
        318│257│964
        954│386│712
        276│491│583
        ───┼───┼───
        163│874│259
        827│915│436
        495│623│178
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
            AA.│...│.CC
            A..│BB.│..C
            ..2│.Bx│x..
            ───┼───┼───
            .D.│EEx│...
            .DD│E..│FFF
            ..G│G..│H..
            ───┼───┼───
            ..G│.IH│H..
            J..│.I.│..K
            JJ.│.I.│.KK
            A=21 B=10 C=21 D=10 E=10 F=10 G=13 H=11 I=10 J=21 K=21
            x<27
            """)
        + KillerCages.Extend;
}
