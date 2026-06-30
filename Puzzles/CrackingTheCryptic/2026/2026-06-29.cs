namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_06_29 : CtcPuzzle
{
    public override string Title => "Just Sum Long Lines";

    public override string? Author => "HalfBakedLunatic";

    public override Uri? Url => new("https://youtu.be/rSMMWRaKq8M");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.New("""
        542│891│376
        937│264│581
        186│375│294
        ───┼───┼───
        654│932│817
        823│417│965
        791│586│432
        ───┼───┼───
        469│128│753
        218│753│649
        375│649│128
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        CCC│D.D│EE.
        .Cc│bDb│aaE
        C.c│.b.│..E
        ───┼───┼───
        B..│d3.│..F
        .Be│d.h│iF.
        Bee│ffh│i.F
        ───┼───┼───
        .AA│Hgg│jGG
        2.I│.Hg│jG.
        .I.│..H│GG.
        A=B=C=D=E=F=G=H=I
        a=b=c=d=e=f=g=h=i=j
        """);
}
