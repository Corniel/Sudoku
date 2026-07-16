namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_07_15 : CtcPuzzle
{
    public override string Title => "Cataract";

    public override string? Author => "Nicolas Duhail";

    public override Uri? Url => new("https://youtu.be/HHE3g108bj0");

    public override O Duration => O.s;

    public override Cells Solution { get; } = Cells.New("""
        542│781│369
        917│563│428
        863│294│751
        ───┼───┼───
        394│672│185
        786│915│234
        125│348│697
        ───┼───┼───
        631│459│872
        258│137│946
        479│826│513
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        AAB│BCC│DDE
        OOO│OOO│OOF
        aab│bbc│cPF
        ───┼───┼───
        QQQ│QRR│cPG
        KKL│LMR│cPG
        xxx│yMR│dPH
        ───┼───┼───
        XXY│yMS│dPH
        ooY│yMS│ePI
        .pZ│zMS│ePI
        A=B=C=D=E=F=G=H=I
        a=b=c=d=e
        K=L=M Q=R=S
        X=Y=Z x=y=z
        O=P o=p
        """);
}
