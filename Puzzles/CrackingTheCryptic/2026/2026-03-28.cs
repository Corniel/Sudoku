namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_03_28 : CtcPuzzle
{
    public override string Title => "Just A Killer";

    public override string? Author => "Adem Jaziri";

    public override Uri? Url => new("https://youtu.be/4NyCrykTZU8");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        942│576│831
        517│834│296
        683│219│547
        ───┼───┼───
        795│683│124
        864│127│953
        231│495│768
        ───┼───┼───
        479│358│612
        156│742│389
        328│961│475
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        A..│..F│F.a
        A.B│Cbb│..a
        ..B│C..│...
        ───┼───┼───
        QQQ│...│.c.
        D..│EEE│.c.
        D..│...│qqq
        ───┼───┼───
        ..T│T.G│G..
        .dI│...│..H
        .dI│.ee│..H
        a=b=c=d=e=7
        B=C=D=E=10
        T=12
        A=F=G=H=I=14
        Q=q=21
        """);
}
