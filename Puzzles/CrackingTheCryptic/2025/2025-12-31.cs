namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_12_31 : CtcPuzzle
{
    public override string Title => "Venice";

    public override string? Author => "Kaktuslav";

    public override Uri? Url => new("https://youtu.be/sS2obiEAQFM");

    public override O Duration => O.ms100;

    public override Cells Solution { get; } = Cells.Parse("""
        649│238│715
        237│154│896
        158│769│234
        ───┼───┼───
        975│846│123
        813│572│469
        462│391│578
        ───┼───┼───
        786│423│951
        324│915│687
        591│687│342
        """);

    protected override Rules GetConstraints()
        => Rules.Standard
        + SameSums.Parse("""
        AXX│YYY│ZZZ
        Aaa│bbb│...
        Acc│ddo│ppp
        ───┼───┼───
        Bee│ffq│rrr
        ggg│hhC│EG.
        iii│jjC│EGI
        ───┼───┼───
        ..k│llD│FHJ
        mmm│nss│tHJ
        ...│..u│vv.

        A=B C=D E=F G=H I=J X=Y=Z
        a=b c=d e=f g=h i=j k=l m=n o=p q=r s=t u=v
        """);
}
