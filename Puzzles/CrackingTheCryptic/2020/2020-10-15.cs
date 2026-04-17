namespace Puzzles.CrackingTheCryptic;

public sealed class _2020_10_15 : CtcPuzzle
{
    public override string Title => "Non-consecutive";

    public override string? Author => "Richard Stolk";

    public override Uri? Url => new("https://youtu.be/yEfmuTFq_L0");

    public override O Duration => O.μs10;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        3.9│.4.│1.6
        ───┼───┼───
        .9.│4.5│.3.
        8.7│.6.│5.4
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        573│824│691
        248│196│375
        961│573│842
        ───┼───┼───
        186│359│427
        724│681│953
        359│247│186
        ───┼───┼───
        692│415│738
        837│962│514
        415│738│269
        """);

    protected override Rules GetConstraints()
        => Rules.Standard
        + NonConsecutives.Orthogonally();
}
