namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_01_06 : CtcPuzzle
{
    public override string Title => "Non-consecutive Killer";

    public override string? Author => "spytr";

    public override Uri? Url => new("https://youtu.be/wO1G7GkIrWE");

    public override O Duration => O.ms100;

    public override Clues Clues { get; } = Clues.New("""
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│...│...
        ...│...│...
        ...│...│...
        ───┼───┼───
        ...│2..│...
        ...│...│...
        ...│...│...
        """);

    public override Cells Solution { get; } = Cells.New("""
        937│185│246
        581│462│793
        264│739│518
        ───┼───┼───
        849│357│162
        372│691│485
        615│824│937
        ───┼───┼───
        158│246│379
        426│973│851
        793│518│624
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.Standard
        + NonConsecutives.Orthogonally()
        + Jigsaw.New("""
        aaa│ccc│ddd
        a.b│cXc│dSd
        aab│cXd│dSS
        ───┼───┼───
        aab│cXX│d.S
        bbb│ccX│XQS
        b.b│bH.│HQS
        ───┼───┼───
        fff│fHH│HQQ
        f.f│.H.│HQQ
        f.f│HH.│QQQ
        """);
}
