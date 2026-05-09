namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_07_31 : CtcPuzzle
{
    public override string Title => "Arrow/Group Sum";

    public override string? Author => "ahaupt";

    public override Uri? Url => new("https://youtu.be/73iEwlTO_p0");

    public override O Duration => O.Unknown;

    public override Cells Solution { get; } = Cells.New("""
        529│781│463
        876│423│159
        431│965│827
        ───┼───┼───
        315│842│976
        294│376│581
        687│159│342
        ───┼───┼───
        168│537│294
        952│614│738
        743│298│615
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Lines.Arrow("""
        ...│...│C..
        ...│...│B..
        ...│..A│.bc
        ───┼───┼───
        ...│...│a..
        ...│...│...
        ..E│...│...
        ───┼───┼───
        GF.│e..│...
        ..f│...│...
        ..g│...│...
        """)
        + Groups.Cages(
        """
        ...│...│...
        ...│BB.│...
        AA.│BB.│...
        ───┼───┼───
        AA.│...│...
        ...│...│...
        ...│...│.CC
        ───┼───┼───
        ...│.DD│.CC
        ...│.DD│...
        ...│...│...
        A ≤ 11  B ≤ 21  C ≤ 19  D ≤ 15
        """,
        false)
        + Groups.Cages(
        """
        ..A│A..│...
        ..A│A..│...
        ...│...│...
        ───┼───┼───
        .BB│...│...
        .BB│...│CC.
        ...│...│CC.
        ───┼───┼───
        ...│...│...
        ...│..D│D..
        ...│..D│D..
        A ≥ 26  B ≥ 19  C ≥ 20  D ≥ 25
        """,
        false)
        + pos(0, 3).LT(0, 4)
        + pos(3, 0).GT(4, 0)
        + pos(4, 8).LT(5, 8)
        + pos(8, 4).GT(8, 5);
}
