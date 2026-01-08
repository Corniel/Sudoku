namespace Puzzles.CrackingTheCryptic;

public sealed class _2019_11_27 : CtcPuzzle
{
    public override string Title => "Non-consecutive Anti-Knight";

    public override string? Author => "Rishi Puri";

    public override Uri? Url => new("https://youtu.be/QNzltTzv0fc");

    public override O Duration => O.μs100;

    public override Clues Clues { get; } = Clues.Parse("""
        ...│...│...
        ...│...│...
        ...│4.7│...
        ───┼───┼───
        ..6│...│5..
        ...│...│...
        ..4│...│3..
        ───┼───┼───
        ...│2.5│...
        ...│...│...
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.Parse("""
        973│518│264
        425│963│718
        861│427│953
        ───┼───┼───
        316│842│597
        758│396│142
        294│751│386
        ───┼───┼───
        649│275│831
        182│639│475
        537│184│629
        """);

    public override Rules Constraints { get; }
        = Rules.AntiKnight
        + NonConsecutives.Orthogonally();
}
