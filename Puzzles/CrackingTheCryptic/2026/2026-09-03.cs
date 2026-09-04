namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_09_03 : CtcPuzzle
{
    public override string Title => "Algae";

    public override string? Author => "Marty Sears";

    public override Uri? Url => new("https://youtu.be/kPt2l1U4pt0");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.New("""
        839│456│172
        172│839│456
        456│172│839
        ───┼───┼───
        728│394│561
        561│728│394
        394│561│728
        ───┼───┼───
        617│283│945
        945│617│283
        283│945│617
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + pos(2, 3).Clue(1)
        + Lines.GermanWhisper("""
            ...│...│...
            ..A│BCD│E..
            ...│.GH│IJ.
            ───┼───┼───
            ..L│MNO│...
            .ab│cde│f..
            ...│.hi│jkl
            ───┼───┼───
            ..n│opq│rs.
            ...│uvw│xy.
            ...│...│...
            """);
}
