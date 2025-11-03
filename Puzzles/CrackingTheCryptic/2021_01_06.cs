namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_01_06 : CtcPuzzle
{
    public override string Title => "Non-consecutive Killer";

    public override string? Author => "spytr";

    public override Uri? Url => new("https://youtu.be/wO1G7GkIrWE");

    public override O Duration => O.s;

    public override Clues Clues { get; } = Clues.Parse("""
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

    public override Cells Solution { get; } = Cells.Parse("""
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

    public override Rules Constraints { get; } =
        Rules.Standard
        + NonConsecutives()
        + Jigsaw.Parse("""
        aaa│ccc│ddd
        a.b│c6c│dSd
        aab│c6d│dSS
        ───┼───┼───
        aab│c66│d.S
        bbb│cc6│6QS
        b.b│bH.│HQS
        ───┼───┼───
        fff│fHH│HQQ
        f.f│.H.│HQQ
        f.f│HH.│QQQ
        """);

    private static IEnumerable<NonConsecutive> NonConsecutives()
    {
        foreach (var pos in Pos.All)
        {
            if (pos.N() is { } n) yield return new NonConsecutive(pos, n);
            if (pos.W() is { } w) yield return new NonConsecutive(pos, w);
        }
    }
}
