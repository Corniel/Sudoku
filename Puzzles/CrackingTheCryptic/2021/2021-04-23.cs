namespace Puzzles.CrackingTheCryptic;

public sealed class _2021_04_23 : CtcPuzzle
{
    public override string Title => "Wheels Of Arrows";

    public override string? Author => "Aspartagcus";

    public override Uri? Url => new("https://youtu.be/Vc-FYo_nur4");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        491│687│352
        856│123│479
        237│495│681
        ───┼───┼───
        945│716│238
        718│532│946
        623│948│517
        ───┼───┼───
        169│374│825
        584│261│793
        372│859│164
        """);

    protected override RuleSet GetConstraints() =>
        RuleSet.Standard
        + Lines.Arrow("""
        ...│...│...
        ...│LKJ│...
        ..A│...│I..
        ───┼───┼───
        .B.│...│.d.
        .C.│...│.c.
        .D.│...│.b.
        ───┼───┼───
        ..i│...│a..
        ...│jkl│...
        ...│...│...
        """)
        + Lines.Arrow("""
        ...│...│...
        ...│...│...
        ..A│...│d..
        ───┼───┼───
        ...│B.e│...
        ...│...│...
        ...│E.b│...
        ───┼───┼───
        ..D│...│a..
        ...│...│...
        ...│...│...
        """)
        + Diagonal.Sum(55, (0, 0), (8, 8))
        + Diagonal.Sum(08, (0, 2), (2, 0))
        + Diagonal.Sum(53, (0, 8), (8, 0))
        + Diagonal.Sum(31, (3, 0), (8, 5))
        + Diagonal.Sum(15, (8, 6), (6, 8))
        + Diagonal.Sum(26, (5, 8), (0, 3));
}
