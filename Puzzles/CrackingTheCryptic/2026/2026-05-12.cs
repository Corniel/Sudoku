namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_05_12 : CtcPuzzle
{
    public override string Title => "Mushroom Dance";

    public override string? Author => "Florian Wortmann";

    public override Uri? Url => new("https://youtu.be/lzcnEnB5tvo");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        362│781│954
        918│425│376
        547│369│218
        ───┼───┼───
        724│953│861
        689│172│435
        135│648│792
        ───┼───┼───
        891│534│627
        273│816│549
        456│297│183
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Killer("""
        ..B│C.E│F..
        .B.│.EG│HJ.
        .B.│.E.│HJM
        ───┼───┼───
        A..│D..│I.L
        ...│...│LL.
        .OO│Q.K│U..
        ───┼───┼───
        N.P│.R.│.V.
        .P.│.R.│.V.
        ..S│R.W│V..
        A=B=C D=E=F G=H I=J K=L=M N=O=P Q=R=S U=V=W
        """);
}
