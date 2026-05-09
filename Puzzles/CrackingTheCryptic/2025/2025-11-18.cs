namespace Puzzles.CrackingTheCryptic;

public sealed class _2025_11_18 : CtcPuzzle
{
    public override string Title => "Equivalence";

    public override string? Author => "Michael Lefkowitz";

    public override Uri? Url => new("https://youtu.be/vx2taaxQ2YI");

    public override O Duration => O.ms10;

    public override Cells Solution { get; } = Cells.New("""
        192│845│376
        648│379│152
        735│621│498
        ───┼───┼───
        983│564│721
        526│917│843
        471│283│965
        ───┼───┼───
        367│458│219
        854│192│637
        219│736│584
        """);

    protected override RuleSet GetConstraints()
        => RuleSet.Standard
        + Groups.Cages("""
        ABB│CD.│...
        AA.│CDE│E.G
        .HI│I.E│.FG
        ───┼───┼───
        .H.│JJK│.FG
        ...│L.K│...
        NN.│LMM│OPP
        ───┼───┼───
        QR.│...│O..
        QR.│STT│.VW
        ...│SSU│UVW
        A=B=C=D=E=F=G=H=I=J=K=L=M=N=O=P=Q=R=S=T=U=V=W
        """);
}
