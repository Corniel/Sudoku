namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_14 : CtcPuzzle
{
    public override string Title => "Counterpoint";

    public override string? Author => "SevenNateNine";

    public override Uri? Url => new("https://youtu.be/nLu0P3VYz4c");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        479│132│586
        186│459│723
        523│768│149
        ───┼───┼───
        957│681│234
        638│524│917
        241│973│658
        ───┼───┼───
        762│845│391
        895│317│462
        314│296│875
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + pos(8, 4).Clue(9)
        + Lines.Arrow("""
        ..A│.bb│B..
        aa.│...│...
        cc.│...│...
        ───┼───┼───
        D.C│...│...
        dd.│.ee│.ff
        ...│...│E.F
        ───┼───┼───
        ...│...│...
        ...│.hh│...
        ggG│...│H..
        """)
        + Lines.Arrow("""
        ..A│...│B..
        ...│aa.│.bb
        ...│...│...
        ───┼───┼───
        ..C│...│...
        ...│cc.│...
        ...│...│Dd.
        ───┼───┼───
        ...│...│..d
        ...│ee.│.ff
        ..E│...│F..
        """);
}
