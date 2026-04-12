namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_03_20 : CtcPuzzle
{
    public override string Title => "Catacomb";

    public override string? Author => "Nicolas Dubai";

    public override Uri? Url => new("https://youtu.be/n6eLrr5WNQU");

    public override O Duration => O.ms;

    public override Cells Solution { get; } = Cells.Parse("""
        751│486│293
        948│325│167
        236│791│485
        ───┼───┼───
        417│839│652
        625│174│839
        893│562│714
        ───┼───┼───
        382│657│941
        574│918│326
        169│243│578
        """);

    public override Rules Constraints { get; }
        = Rules.Standard
        + SameSums.Parse("""
        ...│.II│JJJ
        ...│jjI│JE.
        ..k│b.j│JE.
        ───┼───┼───
        .lc│.aZ│iF.
        Cl.│dWa│YgF
        ACl│.dW│.Yg
        ───┼───┼───
        BDG│mHe│Xf.
        BDG│H.H│f..
        GGG│...│...
        A=B C=D E=F G=H I=J
        a=b=c=d=e=f=g
        i=j=k=l=m
        W=X=Y=Z
        """);
}
