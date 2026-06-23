namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_02_20 : CtcPuzzle
{
    public override string Title => "Pinpoint";

    public override string? Author => "RandyDan";

    public override Uri? Url => new("https://youtu.be/L4BKOaUr1GE");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        542│317│698
        379│846│125
        816│295│473
        ───┼───┼───
        158│439│762
        624│781│539
        793│562│814
        ───┼───┼───
        261│958│347
        487│623│951
        935│174│286
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Couples.Consecutive((2, 7), (3, 7))
        + Couples.Consecutive((4, 2), (5, 2))
        + Couples.Ratio1_2((3, 2), (4, 2))
        + Couples.Ratio1_2((4, 1), (4, 2))
        + Lines.GermanWhisper("""
        ..E│...│...
        AD.│FI.│...
        BC.│GH.│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ..e│..j│...
        ad.│fi.│...
        bc.│gh.│...
        """)
        + Lines.DutchWhisper("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│.AB│...
        ...│...│C..
        ...│...│.D.
        ───┼───┼───
        ...│...│..E
        ...│...│..F
        ...│...│...
        """)
        + Lines.Renban("""
        ...│..A│AAA
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│..B│...
        ...│..B│...
        ───┼───┼───
        ...│...│BB.
        ...│...│...
        ...│...│...
        """);
}
