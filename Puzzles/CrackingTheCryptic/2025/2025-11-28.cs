namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_11_28 : CtcPuzzle
{
    public override string Title => "Brink";

    public override string? Author => "99%Sneaky";

    public override Uri? Url => new("https://youtu.be/1HJtCeHCWQ8");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        178│932│546
        546│178│923
        293│456│781
        ───┼───┼───
        785│621│439
        914│783│265
        632│549│178
        ───┼───┼───
        451│367│892
        867│295│314
        329│814│657
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        ...│...│...
        ...│...│...
        BB.│...│...
        ───┼───┼───
        ...│...│...
        ...│...│.CC
        A..│...│...
        ───┼───┼───
        AE.│...│.DD
        .E.│...│...
        ...│...│...

        A = 10  B = 11  C = 11  D = 11  E = 11
        """)
        + Lines.Renban("""
        ...│...│AAA
        ...│...│...
        ...│.BB│B..
        ───┼───┼───
        ..D│C..│...
        ..D│C..│...
        .D.│C..│...
        ───┼───┼───
        ...│.EE│E..
        ...│...│...
        ...│...│FFF
        """)
        + Lines.Arrow("""
        ...│.aa│...
        ..A│a..│...
        ...│...│.B.
        ───┼───┼───
        ...│..b│bb.
        ...│..C│c..
        ...│...│c..
        ───┼───┼───
        ...│...│...
        ..D│d..│...
        ...│.dd│...
        """)
        + Anti.King
        + KillerCages.Extend;
}
