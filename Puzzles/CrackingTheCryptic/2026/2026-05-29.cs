namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_29 : CtcPuzzle
{
    public override string Title => "Coterminous";

    public override string? Author => "Nicolas Duhail";

    public override Uri? Url => new("https://youtu.be/2W_fjFG3hs4");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        286│374│591
        453│189│726
        917│256│348
        ───┼───┼───
        562│431│879
        741│598│632
        398│627│154
        ───┼───┼───
        139│845│267
        824│763│915
        675│912│483
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        ADD│EEE│FFa
        AGG│HHI│JJa
        AGK│LL.│QQa
        ───┼───┼───
        BXX│YYY│ZRb
        BOO│PPo│ppb
        BOq│rro│ppb
        ───┼───┼───
        C.k│lgg│hhc
        Ckk│lii│jhc
        Cdd│eee│ffc
        A=B=C=a=b=c=15 D=E=F d=e=f G=H g=h I=J i=j K=L k=l O=P o=p
        Q=R q=r X=Y=Z
        """)
        + KillerCages.Extend;
}
