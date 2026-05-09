namespace Puzzles.CrackingTheCryptic;

public sealed class _2026_04_18 : CtcPuzzle
{
    public override string Title => "Shirkflation";

    public override string? Author => "Erin Toler";

    public override Uri? Url => new("https://youtu.be/00bV9KL-kco");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        172│985│643
        395│462│781
        864│713│529
        ───┼───┼───
        541│627│938
        983│154│276
        627│839│415
        ───┼───┼───
        258│346│197
        736│291│854
        419│578│362
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Groups.Cages("""
        ...│...│U..
        ...│B.T│...
        C.A│.TT│..R
        ───┼───┼───
        .DD│S.O│.Q.
        ..D│...│N.Q
        FF.│E.J│NN.
        ───┼───┼───
        ..G│II.│..M
        ...│I.K│L..
        ..H│.K.│...
        A=B C=D=E F=G H=I=J K=L M=N=O Q=R S=T=U
        """)
        + pos(0, 0).LT(1, 0);
}
